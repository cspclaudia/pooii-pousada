using Pousada.Interfaces;

namespace Pousada.Models
{
    public class Valor4 : IValor
    {
        private double _telefonema { get; set; } = 10;
        private double _alimentacao { get; set; } = 100;
        public double CalcularValor (double diaria)
        {
            return diaria + _telefonema + _alimentacao;
        }
    }
}