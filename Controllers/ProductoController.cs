using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvcCremaSistem.Data;
using mvcCremaSistem.Models;

namespace mvcCremaSistem.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;

        }


        // GET: Producto
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Producto.Include(p => p.Categoria);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Producto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Producto/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCat");
            return View();
        }

        // POST: Producto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            // Procesar la imagen si se ha subido
            if (producto.ImagenArchivo != null && producto.ImagenArchivo.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await producto.ImagenArchivo.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();
                    producto.Imagen = Convert.ToBase64String(fileBytes);
                }
            }

            if (ModelState.IsValid)
            {
                producto.FechaCreacion = DateTime.Now;
                producto.FechaActualizacion = DateTime.Now;

                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCat", producto.CategoriaId);
            return View(producto);
        }

        // GET: Producto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCat", producto.CategoriaId);
            return View(producto);
        }

        // POST: Producto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            // Obtener el producto existente
            var productoExistente = await _context.Producto.FindAsync(id);
            if (productoExistente == null)
            {
                return NotFound();
            }

            // Procesar la nueva imagen si se ha subido
            if (producto.ImagenArchivo != null && producto.ImagenArchivo.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await producto.ImagenArchivo.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();
                    productoExistente.Imagen = Convert.ToBase64String(fileBytes);
                }
            }

            // Actualizar las propiedades del producto existente
            productoExistente.ProductoNombre = producto.ProductoNombre;
            productoExistente.CategoriaId = producto.CategoriaId;
            productoExistente.Descripcion = producto.Descripcion;
            productoExistente.Estado = producto.Estado;
            productoExistente.Destacado = producto.Destacado;
            productoExistente.FechaActualizacion = DateTime.Now;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productoExistente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCat", producto.CategoriaId);
            return View(productoExistente);
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto != null)
            {
                _context.Producto.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Producto.Any(e => e.Id == id);
        }
    }
}