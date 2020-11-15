using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pousada.Interfaces;

namespace Pousada.Models
{
    [Table ("Quarto")]
    public class Quarto : IQuarto
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Tipo { get; set; }

        [Required]
        [Column (TypeName = "double(18, 2)")]
        public double ValorDiaria { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public bool Disponivel { get; set; }

        public Quarto () { }

        public Quarto (string Tipo, double ValorDiaria, string Descricao, bool Disponivel)
        {
            this.Tipo = Tipo;
            this.ValorDiaria = ValorDiaria;
            this.Descricao = Descricao;
            this.Disponivel = Disponivel;
        }

        public Quarto Clone (int Numero)
        {
            Quarto clone = (Quarto) this.MemberwiseClone ();
            clone.Numero = Numero;
            return clone;

        }
    }
}