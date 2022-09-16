using Confitec.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confitec.Infrastructure.Data.Mappings
{
    internal class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Sobrenome)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.DataNascimento)
                .IsRequired();

            builder.HasOne(x => x.Escolaridade).WithMany().HasForeignKey(x => x.EscolaridadeId).OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.HistoricoEscolarId)
                .HasDefaultValue(0);
        }
    }
}
