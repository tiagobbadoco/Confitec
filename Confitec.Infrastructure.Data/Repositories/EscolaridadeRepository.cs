using Confitec.Domain.Entities;
using Confitec.Infrastructure.Data.Context;
using Confitec.Infrastructure.Data.Interfaces;

namespace Confitec.Infrastructure.Data.Repositories
{
    public class EscolaridadeRepository : Repository<Escolaridade>, IEscolaridadeRepository
    {
        public EscolaridadeRepository(ConfitecContext context) : base(context)
        {
        }

        public Escolaridade? GetByDescricao(string descricao)
        {
            return DbSet.FirstOrDefault(x => x.Descricao == descricao);
        }
    }
}
