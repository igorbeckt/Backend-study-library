using Localdorateste.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentCarSys.Enums;

namespace RentCarSys.Application.Data.Mappings
{
    public class VeiculoMap : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.ToTable("VEICULO");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.Status)
                .HasColumnName("VEICULO_STATUS");

            builder.Property(e => e.Placa)
                .HasColumnName("PLACA");

            builder.Property(e => e.Marca)
                .HasColumnName("MARCA");

            builder.Property(e => e.Modelo)
                .HasColumnName("MODELO");

            builder.Property(e => e.AnoFabricacao)
                .HasColumnName("ANO_FABRICACAO");

            builder.Property(e => e.KM)
                .HasColumnName("KM");

            builder.Property(e => e.Cor)
                .HasColumnName("COR");

            builder.Property(e => e.Automatico)
                .HasColumnName("AUTOMATICO");

            builder.Property(e => e.ReservaId)
                .HasColumnName("RESERVA_ID");

            builder.HasOne(s => s.Reserva).WithMany(j => j.Veiculo)
                .HasForeignKey(s => s.ReservaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_REFERENCE_VEICULO_RESERVA");
        }
    }
}