using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sistema_cft.Models;

namespace sistema_cft.Controllers
{
    public class AsignaturaasignadasController : Controller
    {
        private readonly SistemaCftContext _context;

        public AsignaturaasignadasController(SistemaCftContext context)
        {
            _context = context;
        }

        // GET: Asignaturaasignadas
        public async Task<IActionResult> Index()
        {
            var sistemaCftContext = _context.Asignaturaasignada.Include(a => a.Asignatura).Include(a => a.Estudiante);
            return View(await sistemaCftContext.ToListAsync());
        }

        // GET: Asignaturaasignadas/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null || _context.Asignaturaasignada == null)
            {
                return NotFound();
            }

            var asignaturaasignada = await _context.Asignaturaasignada
                .Include(a => a.Asignatura)
                .Include(a => a.Estudiante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignaturaasignada == null)
            {
                return NotFound();
            }

            return View(asignaturaasignada);
        }

        // GET: Asignaturaasignadas/Create
        public IActionResult Create()
        {
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id");
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id");
            return View();
        }

        // POST: Asignaturaasignadas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstudianteId,AsignaturaId,Id,FechaRegistro")] Asignaturaasignada asignaturaasignada)
        {
            if (asignaturaasignada.EstudianteId != 0 && asignaturaasignada.AsignaturaId != 0 && asignaturaasignada.FechaRegistro != null)
            {
                _context.Add(asignaturaasignada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id", asignaturaasignada.AsignaturaId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", asignaturaasignada.EstudianteId);
            return View(asignaturaasignada);
        }

        // GET: Asignaturaasignadas/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null || _context.Asignaturaasignada == null)
            {
                return NotFound();
            }

            var asignaturaasignada = await _context.Asignaturaasignada.FindAsync(id);
            if (asignaturaasignada == null)
            {
                return NotFound();
            }
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id", asignaturaasignada.AsignaturaId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", asignaturaasignada.EstudianteId);
            return View(asignaturaasignada);
        }

        // POST: Asignaturaasignadas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("EstudianteId,AsignaturaId,Id,FechaRegistro")] Asignaturaasignada asignaturaasignada)
        {
            if (id != asignaturaasignada.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asignaturaasignada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignaturaasignadaExists(asignaturaasignada.Id))
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
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id", asignaturaasignada.AsignaturaId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", asignaturaasignada.EstudianteId);
            return View(asignaturaasignada);
        }

        // GET: Asignaturaasignadas/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null || _context.Asignaturaasignada == null)
            {
                return NotFound();
            }

            var asignaturaasignada = await _context.Asignaturaasignada
                .Include(a => a.Asignatura)
                .Include(a => a.Estudiante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignaturaasignada == null)
            {
                return NotFound();
            }

            return View(asignaturaasignada);
        }

        // POST: Asignaturaasignadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            if (_context.Asignaturaasignada == null)
            {
                return Problem("Entity set 'SistemaCftContext.Asignaturaasignada'  is null.");
            }
            var asignaturaasignada = await _context.Asignaturaasignada.FindAsync(id);
            if (asignaturaasignada != null)
            {
                _context.Asignaturaasignada.Remove(asignaturaasignada);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsignaturaasignadaExists(uint id)
        {
          return (_context.Asignaturaasignada?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
