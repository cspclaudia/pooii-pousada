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

        public async Task<IActionResult> Index (int? id)
        {
            if (id == null)
                return NotFound ();

            var context = _context.RelatorioDiario
                .Include (r => r.Conta.Reserva.Quarto)
                .Where (m => m.ContaId == id);
            if (context == null)
                return NotFound ();

            return View (await context.ToListAsync ());
        }

        public async Task<IActionResult> Details (int? id)
        {
            if (id == null)
                return NotFound ();

            var relatorioDiario = await _context.RelatorioDiario
                .Include (r => r.Conta.Reserva.Quarto)
                .FirstOrDefaultAsync (m => m.Id == id);
            if (relatorioDiario == null)
                return NotFound ();

            return View (relatorioDiario);
        }

        public IActionResult Create (int? id)
        {
            ViewData["ContaId"] = new SelectList (_context.Conta.Where (c => c.Id == id), "Id", "Id");
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ([Bind ("Telefonema,Alimentacao,ContaId")] RelatorioDiario relatorioDiario)
        {
            relatorioDiario.ValorTelefonema = relatorioDiario.Telefonema ? 15 : 0;
            relatorioDiario.ValorAlimentacao = relatorioDiario.Alimentacao ? 70 : 0;

            Conta conta = _context.Conta
                .Where (conta => conta.Id == relatorioDiario.ContaId)
                .Include (conta => conta.Reserva.Quarto).FirstOrDefault ();
            if (conta == null)
                return NotFound ();

            IValor valor;

            if (!relatorioDiario.Telefonema && !relatorioDiario.Alimentacao)
                valor = new Valor1 ();
            else if (relatorioDiario.Telefonema && !relatorioDiario.Alimentacao)
                valor = new Valor2 ();
            else if (!relatorioDiario.Telefonema && relatorioDiario.Alimentacao)
                valor = new Valor3 ();
            else
                valor = new Valor4 ();

            relatorioDiario.ValorTotal = valor.CalcularValor (
                conta.Reserva.Quarto.ValorDiaria,
                relatorioDiario.ValorTelefonema,
                relatorioDiario.ValorAlimentacao);

            relatorioDiario.Data = DateTime.Now;
            _context.Add (relatorioDiario);

            conta.ValorTotal += relatorioDiario.ValorTotal;
            _context.Update (conta);

            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index), new { id = conta.Id });
        }

        public async Task<IActionResult> Edit (int? id)
        {
            if (id == null)
                return NotFound ();

            var relatorioDiario = await _context.RelatorioDiario.FindAsync (id);
            if (relatorioDiario == null)
                return NotFound ();

            ViewData["ContaId"] = new SelectList (_context.Conta, "Id", "Id", relatorioDiario.ContaId);
            return View (relatorioDiario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, [Bind ("Id,Telefonema,Alimentacao,ValorTelefonema,ValorAlimentacao,ValorTotal,Data,ContaId")] RelatorioDiario relatorioDiario)
        {
            if (id != relatorioDiario.Id)
                return NotFound ();

            if (ModelState.IsValid)
            {
                try
                {
                    Conta conta = _context.Conta
                        .Where (c => c.Id == relatorioDiario.ContaId)
                        .Include(c => c.Reserva.Quarto).FirstOrDefault ();
                    if (conta == null)
                        return NotFound ();

                    conta.ValorTotal -= relatorioDiario.ValorTotal;

                    relatorioDiario.ValorTelefonema = relatorioDiario.Telefonema ? 15 : 0;
                    relatorioDiario.ValorAlimentacao = relatorioDiario.Alimentacao ? 70 : 0;

                    IValor valor;

                    if (!relatorioDiario.Telefonema && !relatorioDiario.Alimentacao)
                        valor = new Valor1 ();
                    else if (relatorioDiario.Telefonema && !relatorioDiario.Alimentacao)
                        valor = new Valor2 ();
                    else if (!relatorioDiario.Telefonema && relatorioDiario.Alimentacao)
                        valor = new Valor3 ();
                    else
                        valor = new Valor4 ();

                    relatorioDiario.ValorTotal = valor.CalcularValor (
                        conta.Reserva.Quarto.ValorDiaria,
                        relatorioDiario.ValorTelefonema,
                        relatorioDiario.ValorAlimentacao);

                    conta.ValorTotal += relatorioDiario.ValorTotal;

                    _context.Update (relatorioDiario);
                    _context.Update (conta);
                    await _context.SaveChangesAsync ();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RelatorioDiarioExists (relatorioDiario.Id))
                        return NotFound ();
                    else
                        throw;
                }
                return RedirectToAction (nameof (Details), new { id = relatorioDiario.Id });
            }
            ViewData["ContaId"] = new SelectList (_context.Conta, "Id", "Id", relatorioDiario.ContaId);
            return View (relatorioDiario);
        }

        public async Task<IActionResult> Delete (int? id)
        {
            if (id == null)
                return NotFound ();

            var relatorioDiario = await _context.RelatorioDiario
                .Include (r => r.Conta.Reserva.Quarto)
                .FirstOrDefaultAsync (m => m.Id == id);
            if (relatorioDiario == null)
                return NotFound ();

            return View (relatorioDiario);
        }

        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            var relatorioDiario = await _context.RelatorioDiario.FindAsync (id);

            Conta conta = _context.Conta
                .Where (conta => conta.Id == relatorioDiario.ContaId).FirstOrDefault ();
            if (conta == null)
                return NotFound ();

            conta.ValorTotal -= relatorioDiario.ValorTotal;
            _context.Update (conta);
            _context.RelatorioDiario.Remove (relatorioDiario);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index), new { id = relatorioDiario.ContaId });
        }

        private bool RelatorioDiarioExists (int id)
        {
            return _context.RelatorioDiario.Any (e => e.Id == id);
        }
    }
}