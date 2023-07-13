using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyTarefas.Data;
using MyTarefas.Models;

namespace MyTarefas.Controllers
{
    [Authorize]
    public class TarefasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TarefasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tarefas
        public async Task<IActionResult> Index()
        {
              return _context.Tarefas != null ? 
                          View(await _context.Tarefas
                          .AsNoTracking()
                        .Where(x => x.User == User.Identity.Name)
                          .ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Tarefas'  is null.");
        }

        // GET: Tarefas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tarefas == null)
            {
                return NotFound();
            }

            var tarefas = await _context.Tarefas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarefas == null)
            {
                return NotFound();
            }

            return View(tarefas);
        }

        // GET: Tarefas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tarefas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Done,CreateAt,LastUpdate,User")] Tarefas tarefas)
        {
            if (ModelState.IsValid)
            {
                tarefas.User = User.Identity.Name;
                _context.Add(tarefas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tarefas);
        }

        // GET: Tarefas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tarefas == null)
            {
                return NotFound();
            }

            var tarefas = await _context.Tarefas.FindAsync(id);
            if (tarefas == null)
            {
                return NotFound();
            }
            return View(tarefas);
        }

        // POST: Tarefas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Done,CreateAt,LastUpdate,User")] Tarefas tarefas)
        {
            if (id != tarefas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tarefas.User = User.Identity.Name;
                    _context.Update(tarefas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarefasExists(tarefas.Id))
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
            return View(tarefas);
        }

        // GET: Tarefas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tarefas == null)
            {
                return NotFound();
            }

            var tarefas = await _context.Tarefas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarefas == null)
            {
                return NotFound();
            }

            return View(tarefas);
        }

        // POST: Tarefas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tarefas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tarefas'  is null.");
            }
            var tarefas = await _context.Tarefas.FindAsync(id);
            if (tarefas != null)
            {
                _context.Tarefas.Remove(tarefas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarefasExists(int id)
        {
          return (_context.Tarefas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
