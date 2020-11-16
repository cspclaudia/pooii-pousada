using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pousada.Data;
using Pousada.Interfaces;
using Pousada.Models;

namespace Pousada.Controllers
{
    public class ContaController : Controller
    {
        private readonly Context _context;

        public ContaController (Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index ()
        {
            var context = _context.Conta.Include (c => c.Reserva.Quarto);
            return View (await context.ToListAsync ());
        }

        public async Task<IActionResult> Details (int? id)
        {
            if (id == null)
                return NotFound ();

            var conta = await _context.Conta
                .Include (c => c.Reserva.Quarto)
                .FirstOrDefaultAsync (m => m.ReservaId == id);

            if (conta == null)
                return NotFound ();

            return View (conta);
        }

        public IActionResult Create ()
        {
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ([Bind ("Id,FormaPagamento")] Conta conta)
        {
            conta.ReservaId = await _context.Reserva
                .Select (reserva => reserva.Id).MaxAsync ();
            conta.StatusPagamento = "Pendente";
            _context.Add (conta);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index));
        }

        public async Task<IActionResult> Pay (int? id)
        {
            if (id == null)
                return NotFound ();

            var conta = await _context.Conta.FindAsync (id);
            if (conta == null)
                return NotFound ();

            try
            {
                Dinheiro dinheiro = new Dinheiro ();
                IPagamento cartao = new Cartao (dinheiro);
                conta.StatusPagamento = cartao.RealizarPagamento ();
                _context.Update (conta);
                await _context.SaveChangesAsync ();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContaExists (conta.Id))
                    return NotFound ();
                else
                    throw;
            }
            return RedirectToAction (nameof (Details), new { id = conta.Id });
        }

        public async Task<IActionResult> Edit (int? id)
        {
            if (id == null)
                return NotFound ();

            var conta = await _context.Conta.FindAsync (id);
            if (conta == null)
                return NotFound ();

            return View (conta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, [Bind ("Id,FormaPagamento")] Conta conta)
        {
            if (id != conta.Id)
                return NotFound ();

            try
            {
                var _conta = await _context.Conta.FindAsync (id);
                _conta.FormaPagamento = conta.FormaPagamento;
                _context.Update (_conta);
                await _context.SaveChangesAsync ();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContaExists (conta.Id))
                    return NotFound ();
                else
                    throw;
            }
            return RedirectToAction (nameof (Index));
        }

        public async Task<IActionResult> Delete (int? id)
        {
            if (id == null)
                return NotFound ();

            var conta = await _context.Conta
                .Include (c => c.Reserva.Quarto)
                .FirstOrDefaultAsync (m => m.Id == id);
            if (conta == null)
                return NotFound ();

            return View (conta);
        }

        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            var conta = await _context.Conta.FindAsync (id);
            _context.Conta.Remove (conta);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index));
        }

        private bool ContaExists (int id)
        {
            return _context.Conta.Any (e => e.Id == id);
        }
    }
}