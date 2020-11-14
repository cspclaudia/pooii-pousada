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
    public class QuartoController : Controller
    {
        private readonly Context _context;

        public QuartoController (Context context)
        {
            _context = context;
        }

        // GET: Quarto
        public async Task<IActionResult> Index ()
        {
            return View (await _context.Quarto.ToListAsync ());
        }

        // GET: Quarto/Details/5
        public async Task<IActionResult> Details (int? id)
        {
            if (id == null)
            {
                return NotFound ();
            }

            var quarto = await _context.Quarto
                .FirstOrDefaultAsync (m => m.Id == id);
            if (quarto == null)
            {
                return NotFound ();
            }

            return View (quarto);
        }

        // GET: Quarto/Create
        public IActionResult Create ()
        {
            return View ();
        }

        // POST: Quarto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Tipo,ValorDiaria,Numero,Descricao,Disponivel")] Quarto quarto)
        public async Task<IActionResult> Create ([Bind ("Id,Tipo,Numero")] Quarto quarto)
        {
                Quarto quartoSimples = new Quarto ();
                quartoSimples.Tipo = "Simples";
                quartoSimples.ValorDiaria = 90.0;
                quartoSimples.Descricao = "Com uma cama de Solteiro";
                quartoSimples.Disponivel = true;

                Quarto quartoDuplo = new Quarto ();
                quartoDuplo.Tipo = "Duplo";
                quartoDuplo.ValorDiaria = 180.0;
                quartoDuplo.Descricao = "Com uma cama de Casal";
                quartoDuplo.Disponivel = true;

                Quarto quartoTriplo = new Quarto ();
                quartoTriplo.Tipo = "Triplo";
                quartoTriplo.ValorDiaria = 270.0;
                quartoTriplo.Descricao = "Com uma cama de Casal e uma de Solteiro";
                quartoTriplo.Disponivel = true;

                Quarto _quarto = new Quarto ();

                switch (quarto.Tipo)
                {
                    case "Simples":
                        _quarto = quartoSimples.Clone ();
                        _quarto.Numero = quarto.Numero;
                        break;
                    case "Duplo":
                        _quarto = quartoDuplo.Clone ();
                        _quarto.Numero = quarto.Numero;
                        break;
                    case "Triplo":
                        _quarto = quartoTriplo.Clone ();
                        _quarto.Numero = quarto.Numero;
                        break;
                }
            // if (ModelState.IsValid)
            // {
                _context.Add (_quarto);
                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Index));
            // }
            // return View (quarto);
        }

        // GET: Quarto/Edit/5
        public async Task<IActionResult> Edit (int? id)
        {
            if (id == null)
            {
                return NotFound ();
            }

            var quarto = await _context.Quarto.FindAsync (id);
            if (quarto == null)
            {
                return NotFound ();
            }
            return View (quarto);
        }

        // POST: Quarto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, [Bind ("Id,Tipo,ValorDiaria,Numero,Descricao,Disponivel")] Quarto quarto)
        {
            if (id != quarto.Id)
            {
                return NotFound ();
            }

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
            return View (quarto);
        }

        // GET: Quarto/Delete/5
        public async Task<IActionResult> Delete (int? id)
        {
            if (id == null)
            {
                return NotFound ();
            }

            var quarto = await _context.Quarto
                .FirstOrDefaultAsync (m => m.Id == id);
            if (quarto == null)
            {
                return NotFound ();
            }

            return View (quarto);
        }

        // POST: Quarto/Delete/5
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