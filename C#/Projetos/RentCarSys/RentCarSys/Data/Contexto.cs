using Localdorateste.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Localdorateste.Data
{
    public class Contexto : DbContext

    {


        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<RetiradaVeiculo> Retiradas { get; set; }
        public DbSet<EntregaVeiculo> Entregas { get; set; }
    }
}

