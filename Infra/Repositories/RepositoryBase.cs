using System;
using System.Collections.Generic;
using System.Linq;
using Infra.EntityConfiguration;
using Infra.Interfaces.Repositories.RepositoryBase;

namespace Infra.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _contex;
        public RepositoryBase(ApplicationDbContext contex)
           => _contex = contex;

        public TEntity Add(TEntity obj)
        {
          _contex.Add(obj);
            return obj;
        }

        public IEnumerable<TEntity> GetAll()
            => _contex.Set<TEntity>().ToList();

        public TEntity GetById(int id)
             => _contex.Set<TEntity>().Find(id);

        public TEntity GetById(Guid id)
            => _contex.Set<TEntity>().Find(id);

        public void Remove(TEntity obj)
        {
            _contex.Remove(obj);
            _contex.SaveChanges();
        }

        public void SaveChanges()
            => _contex.SaveChanges();

        public void Update(TEntity obj)
        {
            _contex.Set<TEntity>().Attach(obj);
            _contex.SaveChanges();
        }

    }
}