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
    public class ContaController : Controller
    {
        private readonly Context _context;

        public ContaController(Context context)
        {
            _context = context;
        }

        // GET: Conta
        public async Task<IActionResult> Index()
        {
            var context = _context.Conta.Include(c => c.Reserva);
            return View(await context.ToListAsync());
        }

        // GET: Conta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conta = await _context.Conta
                .Include(c => c.Reserva)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conta == null)
            {
                return NotFound();
            }

            return View(conta);
        }

        // GET: Conta/Create
        public IActionResult Create()
        {
            ViewData["ReservaId"] = new SelectList(_context.Reserva, "Id", "Id");
            return View();
        }

        // POST: Conta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ValorTotal,FormaPagamento,Status,ReservaId")] Conta conta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReservaId"] = new SelectList(_context.Reserva, "Id", "Id", conta.ReservaId);
            return View(conta);
        }

        // GET: Conta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conta = await _context.Conta.FindAsync(id);
            if (conta == null)
            {
                return NotFound();
            }
            ViewData["ReservaId"] = new SelectList(_context.Reserva, "Id", "Id", conta.ReservaId);
            return View(conta);
        }

        // POST: Conta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ValorTotal,FormaPagamento,Status,ReservaId")] Conta conta)
        {
            if (id != conta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContaExists(conta.Id))
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
            ViewData["ReservaId"] = new SelectList(_context.Reserva, "Id", "Id", conta.ReservaId);
            return View(conta);
        }

        // GET: Conta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conta = await _context.Conta
                .Include(c => c.Reserva)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conta == null)
            {
                return NotFound();
            }

            return View(conta);
        }

        // POST: Conta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conta = await _context.Conta.FindAsync(id);
            _context.Conta.Remove(conta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContaExists(int id)
        {
            return _context.Conta.Any(e => e.Id == id);
        }
    }
}
