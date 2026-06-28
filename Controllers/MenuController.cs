using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvcCremaSistem.Data;
using mvcCremaSistem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace mvcCremaSistem.Controllers
{
    public class MenuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Menu
        public async Task<IActionResult> Index()
        {
            // Obtener productos activos con sus relaciones
            var productos = await _context.Producto
                .Include(p => p.Categoria)
                .Include(p => p.Gramajes)
                .Where(p => p.Estado == "Activo")
                .OrderBy(p => p.Categoria != null ? p.Categoria.NombreCat : "Sin Categoría")
                .ToListAsync();

            // === DEBUG: Verificar datos ===
            System.Diagnostics.Debug.WriteLine($"=== TOTAL PRODUCTOS ENCONTRADOS: {productos.Count} ===");
            foreach (var p in productos)
            {
                System.Diagnostics.Debug.WriteLine($"Producto: {p.ProductoNombre} | Estado: {p.Estado} | Categoria: {p.Categoria?.NombreCat}");
                foreach (var g in p.Gramajes)
                {
                    System.Diagnostics.Debug.WriteLine($"  Gramaje: {g.Gramos}g - {g.Precio}Bs");
                }
            }
            // =============================

            // Agrupar por categoría
            var productosAgrupados = productos
                .GroupBy(p => p.Categoria?.NombreCat ?? "Sin Categoría")
                .Select(g => new MenuGrupoViewModel
                {
                    Categoria = g.Key,
                    Color = g.First().Categoria?.Color ?? "#6c757d",
                    Productos = g.Select(p => new MenuProductoViewModel
                    {
                        Id = p.Id,
                        Nombre = p.ProductoNombre,
                        Descripcion = p.Descripcion ?? "Sin descripción",
                        Imagen = p.Imagen,
                        Destacado = p.Destacado,
                        Estado = p.Estado,
                        Gramajes = p.Gramajes != null && p.Gramajes.Any()
                            ? p.Gramajes.Where(g => g.Activo).Select(g => new GramajeViewModel
                            {
                                Id = g.Id,
                                Gramos = g.Gramos,
                                Precio = g.Precio
                            }).ToList()
                            : new List<GramajeViewModel>(),
                        PrecioMin = p.PrecioMin,
                        PrecioMax = p.PrecioMax
                    }).ToList()
                })
                .ToList();

            // Pasar datos a la vista
            ViewBag.ProductosAgrupados = productosAgrupados;
            ViewBag.CarritoItems = ObtenerCarrito();
            ViewBag.TotalProductos = productos.Count;

            return View();
        }

        // POST: Agregar al carrito
        [HttpPost]
        public async Task<IActionResult> AgregarAlCarrito(int productoId, int gramajeId, int cantidad)
        {
            try
            {
                var producto = await _context.Producto
                    .Include(p => p.Gramajes)
                    .FirstOrDefaultAsync(p => p.Id == productoId);

                if (producto == null)
                {
                    return Json(new { success = false, message = "Producto no encontrado" });
                }

                var gramaje = producto.Gramajes?.FirstOrDefault(g => g.Id == gramajeId);
                if (gramaje == null)
                {
                    return Json(new { success = false, message = "Gramaje no disponible" });
                }

                var carrito = ObtenerCarrito();
                var itemExistente = carrito.FirstOrDefault(c => c.ProductoId == productoId && c.GramajeId == gramajeId);

                if (itemExistente != null)
                {
                    itemExistente.Cantidad += cantidad;
                }
                else
                {
                    carrito.Add(new CarritoItem
                    {
                        ProductoId = productoId,
                        ProductoNombre = producto.ProductoNombre,
                        GramajeId = gramajeId,
                        GramajeTexto = $"{gramaje.Gramos}g",
                        PrecioUnitario = gramaje.Precio,
                        Cantidad = cantidad,
                        Imagen = producto.Imagen
                    });
                }

                GuardarCarrito(carrito);

                return Json(new
                {
                    success = true,
                    message = "Producto agregado al carrito",
                    carritoCount = carrito.Sum(c => c.Cantidad)
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        // POST: Actualizar cantidad en carrito
        [HttpPost]
        public IActionResult ActualizarCarrito(int productoId, int gramajeId, int cantidad)
        {
            try
            {
                var carrito = ObtenerCarrito();
                var item = carrito.FirstOrDefault(c => c.ProductoId == productoId && c.GramajeId == gramajeId);

                if (item != null)
                {
                    if (cantidad <= 0)
                    {
                        carrito.Remove(item);
                    }
                    else
                    {
                        item.Cantidad = cantidad;
                    }
                    GuardarCarrito(carrito);
                }

                return Json(new
                {
                    success = true,
                    carritoCount = carrito.Sum(c => c.Cantidad),
                    totalCarrito = carrito.Sum(c => c.Subtotal)
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        // POST: Eliminar del carrito
        [HttpPost]
        public IActionResult EliminarDelCarrito(int productoId, int gramajeId)
        {
            try
            {
                var carrito = ObtenerCarrito();
                var item = carrito.FirstOrDefault(c => c.ProductoId == productoId && c.GramajeId == gramajeId);

                if (item != null)
                {
                    carrito.Remove(item);
                    GuardarCarrito(carrito);
                }

                return Json(new
                {
                    success = true,
                    carritoCount = carrito.Sum(c => c.Cantidad),
                    totalCarrito = carrito.Sum(c => c.Subtotal)
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        // GET: Ver carrito
        public IActionResult VerCarrito()
        {
            var carrito = ObtenerCarrito();
            return PartialView("_CarritoPartial", carrito);
        }

        // Métodos privados para manejar carrito en sesión
        private List<CarritoItem> ObtenerCarrito()
        {
            var carritoJson = HttpContext.Session.GetString("Carrito");
            if (string.IsNullOrEmpty(carritoJson))
            {
                return new List<CarritoItem>();
            }
            try
            {
                return JsonSerializer.Deserialize<List<CarritoItem>>(carritoJson) ?? new List<CarritoItem>();
            }
            catch
            {
                return new List<CarritoItem>();
            }
        }

        private void GuardarCarrito(List<CarritoItem> carrito)
        {
            var carritoJson = JsonSerializer.Serialize(carrito);
            HttpContext.Session.SetString("Carrito", carritoJson);
        }
    }

    // ViewModels para el menú
    public class MenuGrupoViewModel
    {
        public string Categoria { get; set; } = string.Empty;
        public string Color { get; set; } = "#6c757d";
        public List<MenuProductoViewModel> Productos { get; set; } = new();
    }

    public class MenuProductoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? Imagen { get; set; }
        public bool Destacado { get; set; }
        public string Estado { get; set; } = string.Empty;
        public List<GramajeViewModel> Gramajes { get; set; } = new();
        public decimal PrecioMin { get; set; }
        public decimal PrecioMax { get; set; }
        public decimal PrecioSeleccionado { get; set; }
    }

    public class GramajeViewModel
    {
        public int Id { get; set; }
        public int Gramos { get; set; }
        public decimal Precio { get; set; }
    }

    public class CarritoItem
    {
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; } = string.Empty;
        public int GramajeId { get; set; }
        public string GramajeTexto { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public string? Imagen { get; set; }
        public decimal Subtotal => PrecioUnitario * Cantidad;
    }
}