using Confitec.Domain.Entities;
using Confitec.Infrastructure.Data.Extensions;
using Confitec.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Confitec.Infrastructure.Data.Context
{
    public class ConfitecContext : DbContext
    {
        public ConfitecContext(DbContextOptions<ConfitecContext> options) : base(options)
        {

        }

        #region "DbSets"
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Escolaridade> Escolaridades { get; set; }
        public DbSet<HistoricoEscolar> HistoricosEscolares { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());

            modelBuilder.SeedData();

            base.OnModelCreating(modelBuilder);
        }
    }
}
