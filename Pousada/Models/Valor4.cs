using Pousada.Interfaces;

namespace Pousada.Models
{
    public class Valor4 : IValor
    {
        public double CalcularValor (double diaria, double telefonema, double alimentacao)
        {
            return diaria + telefonema + alimentacao;
        }
    }
}