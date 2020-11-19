using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pousada.Data;
using Pousada.Models;

namespace Pousada.Controllers
{
    public class ReservaController : Controller
    {
        private readonly Context _context;

        public ReservaController (Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index ()
        {
            var context = _context.Reserva.Include (r => r.Hospede).Include (r => r.Quarto);
            return View (await context.ToListAsync ());
        }

        public async Task<IActionResult> Details (int? id)
        {
            if (id == null)
                return NotFound ();

            var reserva = await _context.Reserva
                .Include (r => r.Hospede)
                .Include (r => r.Quarto)
                .FirstOrDefaultAsync (m => m.Id == id);
            if (reserva == null)
                return NotFound ();

            return View (reserva);
        }

        public IActionResult Create ()
        {
            ViewData["HospedeId"] = new SelectList (_context.Hospede, "Id", "Nome");
            ViewData["QuartoId"] = new SelectList (_context.Quarto.Where (q => q.Disponivel == true), "Id", "Numero");
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ([Bind ("Id,DataEntrada,DataSaida,HospedeId,QuartoId")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                Reserva context = _context.Reserva
                    .Where (r => r.DataEntrada == reserva.DataEntrada || r.DataSaida == reserva.DataSaida &&
                        r.QuartoId == reserva.QuartoId).FirstOrDefault ();

                if (context == null &&
                    DateTime.Compare (reserva.DataEntrada, DateTime.Today) >= 0 &&
                    DateTime.Compare (reserva.DataSaida, reserva.DataEntrada) > 0)
                {
                    Quarto quarto = _context.Quarto.Where (q => q.Id == reserva.QuartoId).FirstOrDefault ();
                    quarto.Disponivel = false;
                    _context.Update (quarto);
                    _context.Add (reserva);
                    await _context.SaveChangesAsync ();
                    return RedirectToAction (nameof (Create), "Conta");
                }
            }
            ViewData["HospedeId"] = new SelectList (_context.Hospede, "Id", "Nome", reserva.HospedeId);
            ViewData["QuartoId"] = new SelectList (_context.Quarto, "Id", "Numero", reserva.QuartoId);
            return View (reserva);
        }

        public async Task<JsonResult> GetRoom (int id)
        {
            var quarto = await _context.Quarto.Where (q => q.Id == id).FirstOrDefaultAsync ();
            return new JsonResult (quarto);
        }

        public async Task<IActionResult> Edit (int? id)
        {
            if (id == null)
                return NotFound ();

            var reserva = await _context.Reserva.FindAsync (id);
            if (reserva == null)
                return NotFound ();

            ViewData["QuartoId"] = new SelectList (_context.Quarto, "Id", "Numero", reserva.QuartoId);
            return View (reserva);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, [Bind ("Id,DataEntrada,DataSaida,HospedeId,QuartoId")] Reserva reserva)
        {
            if (id != reserva.Id)
                return NotFound ();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update (reserva);
                    await _context.SaveChangesAsync ();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists (reserva.Id))
                        return NotFound ();
                    else
                        throw;
                }
                return RedirectToAction (nameof (Index));
            }
            ViewData["QuartoId"] = new SelectList (_context.Quarto, "Id", "Numero", reserva.QuartoId);
            return View (reserva);
        }

        public async Task<IActionResult> Delete (int? id)
        {
            if (id == null)
                return NotFound ();

            var reserva = await _context.Reserva
                .Include (r => r.Hospede)
                .Include (r => r.Quarto)
                .FirstOrDefaultAsync (m => m.Id == id);

            if (reserva == null)
                return NotFound ();

            return View (reserva);
        }

        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            var reserva = await _context.Reserva.FindAsync (id);

            Quarto quarto = _context.Quarto
                .Where (q => q.Id == reserva.QuartoId).FirstOrDefault ();
            if (quarto == null)
                return NotFound ();

            quarto.Disponivel = true;
            _context.Update (quarto);
            _context.Reserva.Remove (reserva);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index));
        }

        private bool ReservaExists (int id)
        {
            return _context.Reserva.Any (e => e.Id == id);
        }
    }
}