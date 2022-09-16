using Confitec.Domain.Entities;

namespace Confitec.Infrastructure.Data.Interfaces
{
    public interface IEscolaridadeRepository : IRepository<Escolaridade>
    {
        Escolaridade? GetByDescricao(string descricao);
    }
}
