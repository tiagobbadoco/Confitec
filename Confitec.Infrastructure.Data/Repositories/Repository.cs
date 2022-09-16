using Confitec.Domain.Entities;
using Confitec.Infrastructure.Data.Context;
using Confitec.Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Confitec.Infrastructure.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntidadeBase
    {
        protected readonly ConfitecContext _context;
        public Repository(ConfitecContext context)
        {
            _context = context;
        }

        protected DbSet<TEntity> DbSet
        {
            get
            {
                return _context.Set<TEntity>();
            }
        }

        public virtual List<TEntity> Listar()
        {
            return DbSet.AsNoTracking().ToList();
        }

        public virtual TEntity? GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual void Deletar(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Criar(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Atualizar(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
