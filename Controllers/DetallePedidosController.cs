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
    public class DetallePedidosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetallePedidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DetallePedidos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DetallePedidos.Include(d => d.Gramajes).Include(d => d.Pedidos).Include(d => d.Producto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DetallePedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallePedidos = await _context.DetallePedidos
                .Include(d => d.Gramajes)
                .Include(d => d.Pedidos)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detallePedidos == null)
            {
                return NotFound();
            }

            return View(detallePedidos);
        }

        // GET: DetallePedidos/Create
        public IActionResult Create()
        {
            ViewData["GramajeId"] = new SelectList(_context.Gramajes, "Id", "Id");
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Codigo");
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Estado");
            return View();
        }

        // POST: DetallePedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PedidoId,ProductoId,ProductoNombre,ProductoDescripcion,GramajeId,GramajeSeleccionado,Cantidad,PrecioUnitario,Subtotal")] DetallePedidos detallePedidos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detallePedidos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GramajeId"] = new SelectList(_context.Gramajes, "Id", "Id", detallePedidos.GramajeId);
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Codigo", detallePedidos.PedidoId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Estado", detallePedidos.ProductoId);
            return View(detallePedidos);
        }

        // GET: DetallePedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallePedidos = await _context.DetallePedidos.FindAsync(id);
            if (detallePedidos == null)
            {
                return NotFound();
            }
            ViewData["GramajeId"] = new SelectList(_context.Gramajes, "Id", "Id", detallePedidos.GramajeId);
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Codigo", detallePedidos.PedidoId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Estado", detallePedidos.ProductoId);
            return View(detallePedidos);
        }

        // POST: DetallePedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PedidoId,ProductoId,ProductoNombre,ProductoDescripcion,GramajeId,GramajeSeleccionado,Cantidad,PrecioUnitario,Subtotal")] DetallePedidos detallePedidos)
        {
            if (id != detallePedidos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detallePedidos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallePedidosExists(detallePedidos.Id))
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
            ViewData["GramajeId"] = new SelectList(_context.Gramajes, "Id", "Id", detallePedidos.GramajeId);
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Codigo", detallePedidos.PedidoId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Estado", detallePedidos.ProductoId);
            return View(detallePedidos);
        }

        // GET: DetallePedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallePedidos = await _context.DetallePedidos
                .Include(d => d.Gramajes)
                .Include(d => d.Pedidos)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detallePedidos == null)
            {
                return NotFound();
            }

            return View(detallePedidos);
        }

        // POST: DetallePedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detallePedidos = await _context.DetallePedidos.FindAsync(id);
            if (detallePedidos != null)
            {
                _context.DetallePedidos.Remove(detallePedidos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetallePedidosExists(int id)
        {
            return _context.DetallePedidos.Any(e => e.Id == id);
        }
    }
}
