using DataLayerContracts.Models;

namespace DataLayerContracts
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order GetDetailedOrderById(int id);
    }
}
