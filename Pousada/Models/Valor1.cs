using Pousada.Interfaces;

namespace Pousada.Models
{
    public class Valor1 : IValor
    {
        public double CalcularValor (double diaria, double telefonema, double alimentacao)
        {
            return diaria;
        }
    }
}