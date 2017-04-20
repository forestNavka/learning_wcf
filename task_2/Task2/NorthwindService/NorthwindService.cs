using System;
using System.Collections.Generic;
using NorthwindService.DTOs;
using DataLayerContracts;
using DataLayerContracts.Models;
using AutoMapper;
using System.Linq;
using Microsoft.Practices.Unity;
using DataLayer;

namespace NorthwindService
{
    public class NorthwindService : INorthwindService
    {
        [Dependency]
        private IOrderRepository _ordersRepo { get; } = new OrderRepository();

        public NorthwindService()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Order, OrderDto>();
                cfg.CreateMap<Order, OrderDetailDto>();
                cfg.CreateMap<Order_Detail, ProductDto>();
                cfg.CreateMap<Product, ProductDto>().ForMember(p => p.UnitPrice, opt => opt.Ignore());
            });
        }

        public IEnumerable<OrderDto> GetOrders()
        {
            var orders = _ordersRepo.GetAll();
            return orders.Select(order => {
                var dto = Mapper.Map<OrderDto>(order);
                dto.Status = SetStatus(dto.OrderDate, dto.ShippedDate);
                return dto;
                }).ToList();
        }

        public OrderDetailDto GetDetailedOrder(int id)
        {
            var order = _ordersRepo.GetDetailedOrderById(id);
            var dto = Mapper.Map<Order, OrderDetailDto>(order);
            dto.ProductsInOrder = order.Order_Details.Select(d => Mapper.Map<ProductDto>(d).Map(d.Product));
            return dto;
        }

        private static OrderStatus SetStatus(DateTime? orderDate, DateTime? shippedDate)
        {
            if (!orderDate.HasValue) return OrderStatus.New;
            return !shippedDate.HasValue ? OrderStatus.InProgress : OrderStatus.Completed;
        }
    }

    internal static class MapperHelper
    {
        internal static ProductDto Map(this ProductDto dto, Product product)
        {
            return Mapper.Map(product, dto);
        }
    }
}
