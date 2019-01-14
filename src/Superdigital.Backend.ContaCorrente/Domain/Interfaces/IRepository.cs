using System;

namespace Superdigital.Backend.ContaCorrente.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        int Add(TEntity entity);
        TEntity Find(int Id);
        int Update(TEntity entity);
        int SaveChanges();
    }
}