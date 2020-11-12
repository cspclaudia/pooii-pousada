using Microsoft.EntityFrameworkCore;
using Pousada.Models;

namespace Pousada.Data
{
    public class Context : DbContext
    {
        public Context (DbContextOptions<Context> options) : base (options) { }

        // public DbSet<Cartao> Cartao { get; set; }
        public DbSet<Conta> Conta { get; set; }
        // public DbSet<Dinheiro> Dinheiro { get; set; }
        public DbSet<Hospede> Hospede { get; set; }
        public DbSet<Quarto> Quarto { get; set; }
        // public DbSet<QuartoDuplo> QuartoDuplo { get; set; }
        // public DbSet<QuartoTriplo> QuartoTriplo { get; set; }
        public DbSet<RelatorioDiario> RelatorioDiario { get; set; }
        public DbSet<Reserva> Reserva { get; set; }
        // public DbSet<ValorPadrao> ValorPadrao { get; set; }
        // public DbSet<ValorMaster> ValorMaster { get; set; }
        // public DbSet<ValorPremium> ValorPremium { get; set; }
        // public DbSet<ValorGold> ValorGold { get; set; }
    }
}