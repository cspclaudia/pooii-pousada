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
    public class RelatorioDiarioController : Controller
    {
        private readonly Context _context;

        public RelatorioDiarioController (Context context)
        {
            _context = context;
        }

        // GET: RelatorioDiario
        public async Task<IActionResult> Index (int? id)
        {
            if (id == null)
                return NotFound ();

            var context = _context.RelatorioDiario
                .Include (r => r.Conta)
                .Where (m => m.ContaId == id);

            if (context == null)
                return NotFound ();
                
            return View (await context.ToListAsync ());
        }

        // GET: RelatorioDiario/Details/5
        public async Task<IActionResult> Details (int? id)
        {
            if (id == null)
            {
                return NotFound ();
            }

            var relatorioDiario = await _context.RelatorioDiario
                .Include (r => r.Conta)
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
            ViewData["ContaId"] = new SelectList (_context.Conta, "Id", "FormaPagamento");
            return View ();
        }

        // POST: RelatorioDiario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ([Bind ("Id,ValorTotal,Telefonema,Alimentacao,ValorTelefonema,ValorAlimentacao,ContaId")] RelatorioDiario relatorioDiario)
        {
            if (ModelState.IsValid)
            {
                relatorioDiario.ValorTelefonema = relatorioDiario.Telefonema ? 15 : 0;
                relatorioDiario.ValorAlimentacao = relatorioDiario.Alimentacao ? 70 : 0;

                Conta conta = _context.Conta
                    .Where (conta => conta.Id == relatorioDiario.ContaId)
                    .Include (conta => conta.Reserva.Quarto)
                    .FirstOrDefault ();

                if (!relatorioDiario.Telefonema && !relatorioDiario.Alimentacao)
                {
                    IValor valor = new Valor1 ();
                    relatorioDiario.ValorTotal = valor.CalcularValor (
                        conta.Reserva.Quarto.ValorDiaria,
                        relatorioDiario.ValorTelefonema,
                        relatorioDiario.ValorAlimentacao);
                }
                else if (relatorioDiario.Telefonema && !relatorioDiario.Alimentacao)
                {
                    IValor valor = new Valor2 ();
                    relatorioDiario.ValorTotal = valor.CalcularValor (
                        conta.Reserva.Quarto.ValorDiaria,
                        relatorioDiario.ValorTelefonema,
                        relatorioDiario.ValorAlimentacao);
                }
                else if (!relatorioDiario.Telefonema && relatorioDiario.Alimentacao)
                {
                    IValor valor = new Valor3 ();
                    relatorioDiario.ValorTotal = valor.CalcularValor (
                        conta.Reserva.Quarto.ValorDiaria,
                        relatorioDiario.ValorTelefonema,
                        relatorioDiario.ValorAlimentacao);
                }
                else
                {
                    IValor valor = new Valor4 ();
                    relatorioDiario.ValorTotal = valor.CalcularValor (
                        conta.Reserva.Quarto.ValorDiaria,
                        relatorioDiario.ValorTelefonema,
                        relatorioDiario.ValorAlimentacao);
                }

                conta.ValorTotal += relatorioDiario.ValorTotal;
                relatorioDiario.Data = DateTime.Now;
                _context.Add (relatorioDiario);
                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Index));
            }
            ViewData["ContaId"] = new SelectList (_context.Conta, "Id", "FormaPagamento", relatorioDiario.ContaId);
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
            ViewData["ContaId"] = new SelectList (_context.Conta, "Id", "FormaPagamento", relatorioDiario.ContaId);
            return View (relatorioDiario);
        }

        // POST: RelatorioDiario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, [Bind ("Id,ValorTotal,Data,Telefonema,Alimentacao,ValorTelefonema,ValorAlimentacao,ContaId")] RelatorioDiario relatorioDiario)
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
            ViewData["ContaId"] = new SelectList (_context.Conta, "Id", "FormaPagamento", relatorioDiario.ContaId);
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
                .Include (r => r.Conta)
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