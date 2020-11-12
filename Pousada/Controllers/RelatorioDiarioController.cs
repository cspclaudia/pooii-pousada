using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pousada.Data;
using Pousada.Models;

namespace Pousada.Controllers
{
    public class RelatorioDiarioController : Controller
    {
        private readonly Context _context;

        public RelatorioDiarioController (Context context)
        {
            _context = context;
        }

        // GET: RelatorioDiario
        public async Task<IActionResult> Index ()
        {
            return View (await _context.RelatorioDiario.ToListAsync ());
        }

        // GET: RelatorioDiario/Details/5
        public async Task<IActionResult> Details (int? id)
        {
            if (id == null)
            {
                return NotFound ();
            }

            var relatorioDiario = await _context.RelatorioDiario
                .FirstOrDefaultAsync (m => m.Id == id);
            if (relatorioDiario == null)
            {
                return NotFound ();
            }

            return View (relatorioDiario);
        }

        // GET: RelatorioDiario/Create
        public IActionResult Create ()
        {
            return View ();
        }

        // POST: RelatorioDiario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ([Bind ("Id,ValorTotal,Data,Telefonema,Alimentacao,ValorTelefonema,ValorAlimentacao")] RelatorioDiario relatorioDiario)
        {
            if (ModelState.IsValid)
            {
                _context.Add (relatorioDiario);
                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Index));
            }
            return View (relatorioDiario);
        }

        // GET: RelatorioDiario/Edit/5
        public async Task<IActionResult> Edit (int? id)
        {
            if (id == null)
            {
                return NotFound ();
            }

            var relatorioDiario = await _context.RelatorioDiario.FindAsync (id);
            if (relatorioDiario == null)
            {
                return NotFound ();
            }
            return View (relatorioDiario);
        }

        // POST: RelatorioDiario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, [Bind ("Id,ValorTotal,Data,Telefonema,Alimentacao,ValorTelefonema,ValorAlimentacao")] RelatorioDiario relatorioDiario)
        {
            if (id != relatorioDiario.Id)
            {
                return NotFound ();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update (relatorioDiario);
                    await _context.SaveChangesAsync ();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RelatorioDiarioExists (relatorioDiario.Id))
                    {
                        return NotFound ();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction (nameof (Index));
            }
            return View (relatorioDiario);
        }

        // GET: RelatorioDiario/Delete/5
        public async Task<IActionResult> Delete (int? id)
        {
            if (id == null)
            {
                return NotFound ();
            }

            var relatorioDiario = await _context.RelatorioDiario
                .FirstOrDefaultAsync (m => m.Id == id);
            if (relatorioDiario == null)
            {
                return NotFound ();
            }

            return View (relatorioDiario);
        }

        // POST: RelatorioDiario/Delete/5
        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            var relatorioDiario = await _context.RelatorioDiario.FindAsync (id);
            _context.RelatorioDiario.Remove (relatorioDiario);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index));
        }

        private bool RelatorioDiarioExists (int id)
        {
            return _context.RelatorioDiario.Any (e => e.Id == id);
        }
    }
}