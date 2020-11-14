using Pousada.Interfaces;

namespace Pousada.Models
{
    public class Valor1 : IValor
    {
        private double _telefonema { get; set; } = 0;
        private double _alimentacao { get; set; } = 0;
        public double CalcularValor (double diaria)
        {
            return diaria + _telefonema + _alimentacao;
        }
    }
}