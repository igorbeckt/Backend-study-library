using Localdorateste.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RentCarSys.Application.Data.Mappings
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder) 
        {
            builder.ToTable("CLIENTE");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.Status)
                .HasColumnName("CLIENTE_STATUS");

            builder.Property(e => e.NomeCompleto)
                .HasColumnName("NOME_COMPLETO");

            builder.Property(e => e.Email)
                .HasColumnName("EMAIL");

            builder.Property(e => e.RG)
                .HasColumnName("RG");

            builder.Property(e => e.CPF)
                .HasColumnName("CPF");

            builder.Property(e => e.ReservaId)
                .HasColumnName("RESERVA_ID");

            builder.HasOne(s => s.Reserva).WithMany(j => j.Cliente)
                .HasForeignKey(s => s.ReservaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_REFERENCE_CLIENTE_RESERVA");
        }
    }
}