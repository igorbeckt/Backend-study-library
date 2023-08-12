using Localdorateste.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentCarSys.Enums;

namespace RentCarSys.Application.Data.Mappings
{
    public class ReservaMap : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.ToTable("RESERVA");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.Status)
                .HasColumnName("RESERVA_STATUS");

            builder.Property(e => e.DataReserva)
                .HasColumnName("DATA_RESERVA");

            builder.Property(e => e.ValorLocacao)
                .HasColumnName("VALOR_LOCACAO");

            builder.Property(e => e.DataRetirada)
                .HasColumnName("DATA_RETIRADA");

            builder.Property(e => e.DataEntrega)
                .HasColumnName("DATA_ENTREGA");

        }
    }
}