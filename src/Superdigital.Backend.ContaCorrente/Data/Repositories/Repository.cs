using Microsoft.EntityFrameworkCore;
using Superdigital.Backend.ContaCorrente.Data.Context;
using Superdigital.Backend.ContaCorrente.Domain.Interfaces;
using System;

namespace Superdigital.Backend.ContaCorrente.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ContaCorrenteContext _contaCorrenteContext;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(ContaCorrenteContext contaCorrenteContext)
        {
            _contaCorrenteContext = contaCorrenteContext;
            _dbSet = _contaCorrenteContext.Set<TEntity>();
        }

        public int Add(TEntity entity)
        {
            _dbSet.Add(entity);
            return SaveChanges();

        }

        public TEntity Find(int Id)
        {
            return _dbSet.Find(Id);
        }

        public int Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return SaveChanges();
        }

        public int SaveChanges()
        {
            return _contaCorrenteContext.SaveChanges();
        }

        public void Dispose()
        {
            _contaCorrenteContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}