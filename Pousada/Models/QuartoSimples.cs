using Pousada.Interfaces;

namespace Pousada.Models
{
    public class QuartoSimples : IQuarto
    {
        public float ValorDiaria { get; set; } = 100;
        public int Numero { get {return this.Numero;} set {this.Numero = value;} }
        public string Descricao { get; set; } = "Simples";
        public bool Disponilidade { get; set; } = true;
    }
}
