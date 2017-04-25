using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NorthwindService.DTOs
{
    [DataContract]
    public class OrderDetailDto : OrderDto
    {
        [DataMember]
        public IEnumerable<ProductDto> ProductsInOrder { get; set; }
    }
}
