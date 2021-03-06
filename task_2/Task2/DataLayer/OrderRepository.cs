﻿using System.Collections.Generic;
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

        public int Create(Order order)
        {
            var newOrder = base.Add(order);
            return newOrder.OrderID;
        }

        public void UpdateOrderExcludingProperties(Order order, IEnumerable<string>propertyNames)
        {
            base.UpdateExcludingProperties(order, order.OrderID, propertyNames);
        }
    }
}
