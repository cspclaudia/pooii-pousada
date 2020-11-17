using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pousada.Data;
using Pousada.Interfaces;
using Pousada.Models;

namespace Pousada.Controllers
{
    public class QuartoController : Controller
    {
        private Quarto quartoSimples = new Quarto ("Simples", 90, "Com uma cama de Solteiro", true);
        private Quarto quartoDuplo = new Quarto ("Duplo", 180, "Com uma cama de Casal", true);
        private Quarto quartoTriplo = new Quarto ("Triplo", 270, "Com uma cama de Casal e uma de Solteiro", true);

        private readonly Context _context;

        public QuartoController (Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index ()
        {
            return View (await _context.Quarto.ToListAsync ());
        }

        public async Task<IActionResult> Details (int? id)
        {
            if (id == null)
                return NotFound ();

            var quarto = await _context.Quarto
                .FirstOrDefaultAsync (m => m.Id == id);
            if (quarto == null)
                return NotFound ();

            return View (quarto);
        }

        public IActionResult Create ()
        {
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ([Bind ("Id,Tipo,Numero")] Quarto quarto)
        {
            if (quarto.Numero > 0)
            {
                IQuarto _quarto = new Quarto ();

                switch (quarto.Tipo)
                {
                    case "Simples":
                        _quarto = quartoSimples.Clone (quarto.Numero);
                        break;
                    case "Duplo":
                        _quarto = quartoDuplo.Clone (quarto.Numero);
                        break;
                    case "Triplo":
                        _quarto = quartoTriplo.Clone (quarto.Numero);
                        break;
                }

                _context.Add (_quarto);
                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Index));
            }
            return View (quarto);
        }

        public async Task<IActionResult> Edit (int? id)
        {
            if (id == null)
                return NotFound ();

            var quarto = await _context.Quarto.FindAsync (id);
            if (quarto == null)
                return NotFound ();

            return View (quarto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, [Bind ("Id,Tipo,ValorDiaria,Numero,Descricao,Disponivel")] Quarto quarto)
        {
            if (id != quarto.Id)
                return NotFound ();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update (quarto);
                    await _context.SaveChangesAsync ();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuartoExists (quarto.Id))
                        return NotFound ();
                    else
                        throw;
                }
                return RedirectToAction (nameof (Index));
            }
            return View (quarto);
        }

        public async Task<IActionResult> Delete (int? id)
        {
            if (id == null)
                return NotFound ();

            var quarto = await _context.Quarto
                .FirstOrDefaultAsync (m => m.Id == id);
            if (quarto == null)
                return NotFound ();

            return View (quarto);
        }

        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            var quarto = await _context.Quarto.FindAsync (id);
            _context.Quarto.Remove (quarto);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index));
        }

        private bool QuartoExists (int id)
        {
            return _context.Quarto.Any (e => e.Id == id);
        }
    }
}