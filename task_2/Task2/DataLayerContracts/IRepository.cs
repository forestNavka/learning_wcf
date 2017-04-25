using System.Collections.Generic;

namespace DataLayerContracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);

        TEntity Add(TEntity entity);

        void UpdateExcludingProperties(TEntity entity, int id, IEnumerable<string> propertiesToExclude);

        void Delete(int id);

        void SetProperty(TEntity entity, string propertyName);
    }
}
