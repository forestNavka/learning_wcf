using DataLayerContracts;
using System.Collections.Generic;
using System.Data.Entity;
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

        public void Update(TEntity entity, int id)
        {
            using (var context = new NorthwindContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
