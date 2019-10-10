using Bayer.Infra.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Bayer.Infra.Repositories.generic
{
    public class Repository<TEntity> where TEntity : class
    {
        protected BayerContext Db;
        protected DbSet<TEntity> DbSet;

        public Repository()
        {
            Db = new BayerContext();
            DbSet = Db.Set<TEntity>();
        }

        public virtual void Adicionar(TEntity obj)
        {
            var objReturn = DbSet.Add(obj);
            SaveChanges();
        }

        public virtual void Atualizar(TEntity obj)
        {
            var entry = Db.Entry(obj);

            DbSet.Attach(obj);

            entry.State = EntityState.Modified;

            SaveChanges();
        }

        public virtual void Excluir(Guid id)
        {
            DbSet.Remove(ObterPorId(id, false));

            SaveChanges();
        }

        public virtual IEnumerable<TEntity> ObterTodos(bool readOnly)
        {
            IEnumerable<TEntity> todos = null;
            if (!readOnly)
            {
                todos = DbSet.ToList();
            }
            else
            {
                todos = DbSet.AsNoTracking().ToList();
            }
            return todos;
        }

        public virtual TEntity ObterPorId(Guid id, bool readOnly)
        {
            TEntity obj = null;
            if (!readOnly)
            {
                obj = DbSet.Find(id);
            }
            else
            {
                obj = DbSet.Find(id);
                Db.Entry(obj).State = EntityState.Detached;
            }

            return DbSet.Find(id);
        }

        public IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate, bool readOnly)
        {
            IEnumerable<TEntity> listaObj = null;

            if (!readOnly)
            {
                listaObj = DbSet.Where(predicate);
            }
            else
            {
                listaObj = DbSet.AsNoTracking().Where(predicate);
            }
            return listaObj;
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
