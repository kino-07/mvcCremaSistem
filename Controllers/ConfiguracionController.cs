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
    public class ConfiguracionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConfiguracionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Configuracion
        public async Task<IActionResult> Index()
        {
            return View(await _context.Configuracion.ToListAsync());
        }

        // GET: Configuracion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuracion = await _context.Configuracion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configuracion == null)
            {
                return NotFound();
            }

            return View(configuracion);
        }

        // GET: Configuracion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Configuracion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Clave,Valor,Descripcion,FechaActualizacion")] Configuracion configuracion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(configuracion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(configuracion);
        }

        // GET: Configuracion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuracion = await _context.Configuracion.FindAsync(id);
            if (configuracion == null)
            {
                return NotFound();
            }
            return View(configuracion);
        }

        // POST: Configuracion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Clave,Valor,Descripcion,FechaActualizacion")] Configuracion configuracion)
        {
            if (id != configuracion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(configuracion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfiguracionExists(configuracion.Id))
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
            return View(configuracion);
        }

        // GET: Configuracion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuracion = await _context.Configuracion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configuracion == null)
            {
                return NotFound();
            }

            return View(configuracion);
        }

        // POST: Configuracion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var configuracion = await _context.Configuracion.FindAsync(id);
            if (configuracion != null)
            {
                _context.Configuracion.Remove(configuracion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfiguracionExists(int id)
        {
            return _context.Configuracion.Any(e => e.Id == id);
        }
    }
}
