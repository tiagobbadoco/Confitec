using Confitec.Domain.Entities;
using Confitec.Infrastructure.Data.Context;
using Confitec.Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Confitec.Infrastructure.Data.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ConfitecContext context) : base(context)
        {
        }

        public override List<Usuario> Listar()
        {
            return DbSet.Include(x => x.Escolaridade).AsNoTracking().ToList();
        }

        public override Usuario? GetById(int id)
        {
            return DbSet.Include(x => x.Escolaridade).FirstOrDefault(x => x.Id == id);
        }
    }
}
