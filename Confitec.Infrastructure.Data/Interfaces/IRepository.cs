using Confitec.Domain.Entities;

namespace Confitec.Infrastructure.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : EntidadeBase
    {
        List<TEntity> Listar();
        TEntity? GetById(int id);
        void Criar(TEntity entity);
        void Atualizar(TEntity entity);
        void Deletar(TEntity entity);
        void SaveChanges();
    }
}
