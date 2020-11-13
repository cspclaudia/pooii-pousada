using Pousada.Interfaces;

namespace Pousada.Models
{
    public class QuartoTriplo : IQuarto
    {
        public float ValorDiaria { get; set; } = 300;
        public int Numero { get {return this.Numero;} set {this.Numero = value;} }
        public string Descricao { get; set; } = "Triplo";
        public bool Disponilidade { get; set; } = true;
    }
}