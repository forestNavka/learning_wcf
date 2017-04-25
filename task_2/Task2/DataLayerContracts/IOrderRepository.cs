using System.Collections.Generic;
using DataLayerContracts.Models;

namespace DataLayerContracts
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order GetDetailedOrderById(int id);

        int Create(Order order);

        void UpdateOrderExcludingProperties(Order order, IEnumerable<string>propertyNamesToExclude);
    }
}
