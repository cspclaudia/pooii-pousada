using Pousada.Interfaces;

namespace Pousada.Models
{
    public class Dinheiro : IPagamento
    {
        public string RealizarPagamento() => "Aprovado";
    }
}