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
    public class GramajesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GramajesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Gramajes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Gramajes.Include(g => g.Producto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Gramajes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gramajes = await _context.Gramajes
                .Include(g => g.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gramajes == null)
            {
                return NotFound();
            }

            return View(gramajes);
        }

        // GET: Gramajes/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "ProductoId");
            return View();
        }

        // POST: Gramajes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductoId,Gramos,Precio,Activo,FechaCreacion")] Gramajes gramajes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gramajes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "ProductoNombre", gramajes.ProductoId);
            return View(gramajes);
        }

        // GET: Gramajes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gramajes = await _context.Gramajes.FindAsync(id);
            if (gramajes == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "ProductoNombre", gramajes.ProductoId);
            return View(gramajes);
        }

        // POST: Gramajes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductoId,Gramos,Precio,Activo,FechaCreacion")] Gramajes gramajes)
        {
            if (id != gramajes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gramajes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GramajesExists(gramajes.Id))
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
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "ProductoNombre", gramajes.ProductoId);
            return View(gramajes);
        }

        // GET: Gramajes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gramajes = await _context.Gramajes
                .Include(g => g.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gramajes == null)
            {
                return NotFound();
            }

            return View(gramajes);
        }

        // POST: Gramajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gramajes = await _context.Gramajes.FindAsync(id);
            if (gramajes != null)
            {
                _context.Gramajes.Remove(gramajes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GramajesExists(int id)
        {
            return _context.Gramajes.Any(e => e.Id == id);
        }
    }
}
