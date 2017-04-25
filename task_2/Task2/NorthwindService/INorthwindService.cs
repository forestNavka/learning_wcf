using NorthwindService.DTOs;
using System.Collections.Generic;
using System.ServiceModel;

namespace NorthwindService
{
    [ServiceContract(Namespace = "http://epm.com/Northwind")]
    public interface INorthwindService
    {
        [OperationContract]
        IEnumerable<OrderDto> GetOrders();

        [OperationContract]
        OrderDetailDto GetDetailedOrder(int id);

        [OperationContract]
        int CreateOrder (OrderDto orderDto);

        [OperationContract]
        void UpdateOrder(OrderDto orderDto);

        [OperationContract]
        void SetStatus(int id, OrderStatus status);

        [OperationContract]
        void DeleteOrder(int id);
    }
}
