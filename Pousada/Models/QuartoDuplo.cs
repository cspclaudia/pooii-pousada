using Pousada.Interfaces;

namespace Pousada.Models
{
    public class QuartoDuplo : IQuarto
    {
        public float ValorDiaria { get; set; } = 200;
        public int Numero { get {return this.Numero;} set {this.Numero = value;} }
        public string Descricao { get; set; } = "Duplo";
        public bool Disponilidade { get; set; } = true;
    }
}
