using AutoMapper;
using DataLayer;
using DataLayerContracts;
using DataLayerContracts.Models;
using Microsoft.Practices.Unity;
using NorthwindService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

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
                cfg.CreateMap<Order, OrderDto>()
                    .ReverseMap()
                    .ForMember(o => o.OrderDate, opt => opt.Ignore())
                    .ForMember(o => o.ShippedDate, opt => opt.Ignore());
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

        public int CreateOrder(OrderDto orderDto)
        {
            return _ordersRepo.Create(Mapper.Map<Order>(orderDto));
        }

        public void UpdateOrder(OrderDto orderDto)
        {
            if (orderDto.Status != OrderStatus.New) return;
            _ordersRepo.UpdateOrderExcludingProperties(Mapper.Map<Order>(orderDto), new[] { nameof(Order.OrderDate), nameof(Order.ShippedDate) });
        }

        public void SetStatus(int id, OrderStatus status)
        {
            var order = _ordersRepo.GetById(id);
            var dto = Mapper.Map<OrderDto>(order);
            if (status == OrderStatus.InProgress && dto.Status == OrderStatus.New)
            {
                order.OrderDate = DateTime.Now;
                _ordersRepo.SetProperty(order, nameof(Order.OrderDate));
                return;
            }
            if (status == OrderStatus.Completed && dto.Status == OrderStatus.InProgress)
            {
                order.ShippedDate = DateTime.Now;
                _ordersRepo.SetProperty(order, nameof(Order.ShippedDate));
                return;
            }
            throw new InvalidOperationException("Can't swith from current state to destination");
        }

        public void DeleteOrder(int id)
        {
            var dto = Mapper.Map<OrderDto>(_ordersRepo.GetById(id));
            if (dto.Status == OrderStatus.Completed) throw new InvalidOperationException("Can't delete completed order");
            _ordersRepo.Delete(dto.OrderID);
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
