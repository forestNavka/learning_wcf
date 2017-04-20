using NorthwindService.DTOs;
using System.Collections.Generic;
using System.ServiceModel;

namespace NorthwindService
{
    [ServiceContract]
    public interface INorthwindService
    {
        [OperationContract]
        IEnumerable<OrderDto> GetOrders();

        [OperationContract]
        OrderDetailDto GetDetailedOrder(int id);
    }
}
