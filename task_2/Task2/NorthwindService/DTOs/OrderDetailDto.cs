﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NorthwindService.DTOs
{
    [DataContract]
    public class OrderDetailDto
    {
        [DataMember]
        public int OrderID { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public int? EmployeeID { get; set; }

        [DataMember]
        public DateTime? OrderDate { get; set; }

        [DataMember]
        public DateTime? RequiredDate { get; set; }

        [DataMember]
        public DateTime? ShippedDate { get; set; }

        [DataMember]
        public int? ShipVia { get; set; }

        [DataMember]
        public decimal? Freight { get; set; }

        [DataMember]
        public string ShipName { get; set; }

        [DataMember]
        public string ShipAddress { get; set; }

        [DataMember]
        public string ShipCity { get; set; }

        [DataMember]
        public string ShipRegion { get; set; }

        [DataMember]
        public string ShipPostalCode { get; set; }

        [DataMember]
        public string ShipCountry { get; set; }

        [DataMember]
        public IEnumerable<ProductDto> ProductsInOrder { get; set; }
    }
}
