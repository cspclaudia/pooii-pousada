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

        public bool RealizarPagamento ()
        {
            if (!this.VerificarLimite ())
                return false;
            return this._dinheiro.RealizarPagamento ();
        }

        public bool VerificarLimite ()
        {
            var result = new Random().Next(2) == 1; // 0 = false, 1 = true;
            return result;
        }
    }
}