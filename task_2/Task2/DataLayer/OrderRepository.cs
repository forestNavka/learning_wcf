using DataLayerContracts;
using DataLayerContracts.Models;
using System.Data.Entity;
using System.Linq;

namespace DataLayer
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public Order GetDetailedOrderById(int id)
        {
            using (var context = new NorthwindContext())
            {
               return context.Orders
                    .Where(o => o.OrderID == id)
                    .Include(o => o.Order_Details.Select(d => d.Product))
                    .FirstOrDefault();
            }
        }
    }
}
