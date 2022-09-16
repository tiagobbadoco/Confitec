using Confitec.Domain.Entities;
using Confitec.Infrastructure.Data.Context;
using Confitec.Infrastructure.Data.Interfaces;

namespace Confitec.Infrastructure.Data.Repositories
{
    public class HistoricoEscolarRepository : Repository<HistoricoEscolar>, IHistoricoEscolarRepository
    {
        public HistoricoEscolarRepository(ConfitecContext context) : base(context)
        {
        }
    }
}
