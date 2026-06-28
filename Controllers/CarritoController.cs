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
    public class CarritoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarritoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Carrito
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Carrito.Include(c => c.Gramaje).Include(c => c.Producto).Include(c => c.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Carrito/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carrito
                .Include(c => c.Gramaje)
                .Include(c => c.Producto)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrito == null)
            {
                return NotFound();
            }

            return View(carrito);
        }

        // GET: Carrito/Create
        public IActionResult Create()
        {
            ViewData["GramajeId"] = new SelectList(_context.Gramajes, "Id", "Id");
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Estado");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Correo");
            return View();
        }

        // POST: Carrito/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,ProductoId,GramajeId,Cantidad,FechaAgregado,FechaActualizacion")] Carrito carrito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carrito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GramajeId"] = new SelectList(_context.Gramajes, "Id", "Id", carrito.GramajeId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Estado", carrito.ProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Correo", carrito.UsuarioId);
            return View(carrito);
        }

        // GET: Carrito/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carrito.FindAsync(id);
            if (carrito == null)
            {
                return NotFound();
            }
            ViewData["GramajeId"] = new SelectList(_context.Gramajes, "Id", "Id", carrito.GramajeId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Estado", carrito.ProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Correo", carrito.UsuarioId);
            return View(carrito);
        }

        // POST: Carrito/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,ProductoId,GramajeId,Cantidad,FechaAgregado,FechaActualizacion")] Carrito carrito)
        {
            if (id != carrito.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoExists(carrito.Id))
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
            ViewData["GramajeId"] = new SelectList(_context.Gramajes, "Id", "Id", carrito.GramajeId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Estado", carrito.ProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Correo", carrito.UsuarioId);
            return View(carrito);
        }

        // GET: Carrito/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carrito
                .Include(c => c.Gramaje)
                .Include(c => c.Producto)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrito == null)
            {
                return NotFound();
            }

            return View(carrito);
        }

        // POST: Carrito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carrito = await _context.Carrito.FindAsync(id);
            if (carrito != null)
            {
                _context.Carrito.Remove(carrito);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarritoExists(int id)
        {
            return _context.Carrito.Any(e => e.Id == id);
        }
    }
}
