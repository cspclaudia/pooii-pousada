using System;
using Pousada.Interfaces;

namespace Pousada.Models
{
    public class Cartao : IPagamento
    {
        private Dinheiro _dinheiro;

        public Cartao (Dinheiro dinheiro)
        {
            this._dinheiro = dinheiro;
        }

        public bool VerificarLimite ()
        {
            return new Random ().Next (2) == 1; // 0 = false, 1 = true;
        }

        public string RealizarPagamento ()
        {
            if (this.VerificarLimite ())
                return this._dinheiro.RealizarPagamento ();
            return "Reprovado";
        }

    }
}