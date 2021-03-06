using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pousada.Data;
using Pousada.Models;

namespace Pousada.Controllers
{
    public class HospedeController : Controller
    {
        private readonly Context _context;

        public HospedeController (Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index (string searchString, DateTime searchDate)
        {
            var hospedes = _context.Hospede.Select (h => h);

            if (!String.IsNullOrEmpty (searchString))
                hospedes = hospedes.Where (h => h.Nome.Contains (searchString));
            else if (searchDate != default(DateTime) || searchDate != DateTime.MinValue)
                hospedes = hospedes.Where (h => h.DataNascimento.Equals (searchDate));

            return View (await hospedes.ToListAsync ());
        }

        public async Task<IActionResult> Details (int? id)
        {
            if (id == null)
                return NotFound ();

            Hospede hospede = await _context.Hospede
                .FirstOrDefaultAsync (h => h.Id == id);
            if (hospede == null)
                return NotFound ();

            return View (hospede);
        }

        public IActionResult Create ()
        {
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ([Bind ("Id,Nome,Telefone,RG,DataNascimento,Logradouro,Bairro,Cidade,Estado")] Hospede hospede)
        {
            if (ModelState.IsValid)
            {
                _context.Add (hospede);
                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Index));
            }
            return View (hospede);
        }

        public async Task<IActionResult> Edit (int? id)
        {
            if (id == null)
                return NotFound ();

            Hospede hospede = await _context.Hospede.FindAsync (id);
            if (hospede == null)
                return NotFound ();

            return View (hospede);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, [Bind ("Id,Nome,Telefone,RG,DataNascimento,Logradouro,Bairro,Cidade,Estado")] Hospede hospede)
        {
            if (id != hospede.Id)
                return NotFound ();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update (hospede);
                    await _context.SaveChangesAsync ();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HospedeExists (hospede.Id))
                        return NotFound ();
                    else
                        throw;
                }
                return RedirectToAction (nameof (Index));
            }
            return View (hospede);
        }

        public async Task<IActionResult> Delete (int? id)
        {
            if (id == null)
                return NotFound ();

            Hospede hospede = await _context.Hospede
                .FirstOrDefaultAsync (h => h.Id == id);
            if (hospede == null)
                return NotFound ();

            return View (hospede);
        }

        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            Hospede hospede = await _context.Hospede.FindAsync (id);
            _context.Hospede.Remove (hospede);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index));
        }

        private bool HospedeExists (int id)
        {
            return _context.Hospede.Any (e => e.Id == id);
        }
    }
}