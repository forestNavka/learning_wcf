using DataLayerContracts;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace DataLayer
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        public TEntity Add(TEntity entity)
        {
            using (var context = new NorthwindContext())
            {
                var createdEntity = context.Set<TEntity>().Add(entity);
                context.SaveChanges();
                return createdEntity;
            }
        }

        public void Delete(int id)
        {
            using (var context = new NorthwindContext())
            {
                var entity = context.Set<TEntity>().Find(id);
                context.Set<TEntity>().Remove(entity);
                context.SaveChanges();
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (var context = new NorthwindContext())
            {
                return context.Set<TEntity>().Take(100).ToList();
            }
        }

        public TEntity GetById(int id)
        {
            using (var context = new NorthwindContext())
            {
                return context.Set<TEntity>().Find(id);
            }
        }

        public void UpdateExcludingProperties(TEntity entity, int id, IEnumerable<string>propertiesToExclude = null)
        {
            using (var context = new NorthwindContext())
            {
                var entry = context.Entry(entity);
                entry.State = EntityState.Modified;
                ExcludeSomeProperties(propertiesToExclude, entry);
                context.SaveChanges();
            }
        }

        public void SetProperty(TEntity entity, string propertyName)
        {
            using (var context = new NorthwindContext())
            {
                var entry = context.Entry(entity);
                entry.State = EntityState.Unchanged;
                entry.Property(propertyName).IsModified = true;
                context.SaveChanges();
            }
        }

        private static void ExcludeSomeProperties(IEnumerable<string> propertiesToExclude, DbEntityEntry<TEntity> entry)
        {
            if (propertiesToExclude == null) return;
            foreach (var property in propertiesToExclude)
            {
                entry.Property(property).IsModified = false;
            }
        }
    }
}
