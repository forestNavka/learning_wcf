using FluentAssertions;
using NUnit.Framework;
using System;
using System.ServiceModel;
using TestClient.NorthwindServiceReference;

namespace TestClient
{
    [TestFixture]
    public class NorthwindServiceTests
    {
        [Test]
        public void GetAll_ShouldReturn100Entities()
        {
            using (var client = new NorthwindServiceClient())
            {
                var result = client.GetOrders();
                Assert.AreEqual(100, result.Length);
            }
        }

        [Test]
        public void GetDetailedOrder_ShouldReturnExistingDetailedOrder()
        {
            const int id = 11000;

            //An entity from Northwind database
            var expected = new OrderDetailDto
            {
                OrderID = id,
                CustomerID = "RATTC",
                EmployeeID = 2,
                OrderDate = DateTime.Parse("1998-04-06 00:00:00.000"),
                RequiredDate = DateTime.Parse("1998-05-04 00:00:00.000"),
                ShippedDate = DateTime.Parse("1998-04-14 00:00:00.000"),
                ShipVia = 3,
                Freight = 55.12m,
                ShipName = "Rattlesnake Canyon Grocery",
                ShipAddress = "2817 Milton Dr.",
                ShipCity = "Albuquerque",
                ShipRegion = "NM",
                ShipPostalCode = "87110",
                ShipCountry = "USA",
                Status = OrderStatus.Completed,
                ProductsInOrder = new[] {
                    new ProductDto
                    {
                        ProductID = 4,
                        UnitPrice = 22,
                        Quantity = 25,
                        Discount = 0.25f,
                        Discontinued = false,
                        ProductName = "Chef Anton\'s Cajun Seasoning",
                        SupplierID = 2,
                        CategoryID = 2,
                        QuantityPerUnit = "48 - 6 oz jars",
                        UnitsInStock = 53,
                        UnitsOnOrder = 0,
                        ReorderLevel = 0

                    },
                    new ProductDto
                    {
                        ProductID = 24,
                        UnitPrice = 4.5m,
                        Quantity = 30,
                        Discount = 0.25f,
                        Discontinued = true,
                        ProductName = "Guarana Fantastica",
                        SupplierID = 10,
                        CategoryID = 1,
                        QuantityPerUnit = "12 - 355 ml cans",
                        UnitsInStock = 20,
                        UnitsOnOrder = 0,
                        ReorderLevel = 0

                    },
                    new ProductDto
                    {
                        ProductID = 77,
                        UnitPrice = 13,
                        Quantity = 30,
                        Discount = 0,
                        Discontinued = false,
                        ProductName = "Original Frankfurter grune So?e",
                        SupplierID = 12,
                        CategoryID = 2,
                        QuantityPerUnit = "12 boxes",
                        UnitsInStock = 32,
                        UnitsOnOrder = 0,
                        ReorderLevel = 15
                        
                    } }
                
                

            };

            using (var client = new NorthwindServiceClient())
            {
                var result = client.GetDetailedOrder(id);
                result.ShouldBeEquivalentTo(expected,
                    options => options.Excluding(ctx => ctx.SelectedMemberPath.EndsWith(nameof(ProductDto.ExtensionData))));
            }
        }

        [Test]
        public void Create_ShouldCreateNewOrder()
        {
            using (var client = new NorthwindServiceClient())
            {
                var id = client.CreateOrder(new OrderDto());
                var newOrder = client.GetDetailedOrder(id);
                newOrder.ShouldBeEquivalentTo(new {OrderID = id, Status = OrderStatus.New},
                    opt => opt.ExcludingMissingMembers());
            }
        }

        [Test]
        public void Update_ShouldUpdateOrder()
        {
            const string cityToUpdate = "SomeCityName";

            using (var client = new NorthwindServiceClient())
            {
                //Arrange
                var id = client.CreateOrder(new OrderDto());
                var updatedOrder = new OrderDto {OrderID = id, ShipCity = cityToUpdate};

                //Act
                client.UpdateOrder(updatedOrder);

                //Assert
                var orderFromDb = client.GetDetailedOrder(id);
                orderFromDb.ShipCity.Should().Be(cityToUpdate);
            }
        }

        [Test]
        public void Update_ShouldNotUpdateOrderDates()
        {
            var date = DateTime.Today;

            using (var client = new NorthwindServiceClient())
            {
                //Arrange
                var id = client.CreateOrder(new OrderDto());
                var updatedOrder = new OrderDto { OrderID = id, OrderDate = date};

                //Act
                client.UpdateOrder(updatedOrder);

                //Assert
                var orderFromDb = client.GetDetailedOrder(id);
                orderFromDb.OrderDate.Should().BeNull();
            }
        }

        [Test]
        public void SetStatus_ShouldChangeStatusInValidScenarios()
        {
            using (var client = new NorthwindServiceClient())
            {
                //Arrange
                var id = client.CreateOrder(new OrderDto());
                
                //Act
                client.SetStatus(id, OrderStatus.InProgress);

                //Assert
                var orderFromDb = client.GetDetailedOrder(id);
                orderFromDb.Status.Should().Be(OrderStatus.InProgress, "Status should be changed from New to InProgress");

                //Act
                client.SetStatus(id, OrderStatus.Completed);

                //Assert
                var order2FromDb = client.GetDetailedOrder(id);
                order2FromDb.Status.Should().Be(OrderStatus.Completed, "Status should be changed from InProgress to Completed");
            }

        }

        [Test]
        public void SetStatus_ShouldNotChangeStatus_FromNewToCompleted()
        {
            using (var client = new NorthwindServiceClient())
            {
                //Arrange
                var id = client.CreateOrder(new OrderDto());

                //Act
                //Assert
                Assert.Throws<FaultException>(() => client.SetStatus(id, OrderStatus.Completed));
            }

        }

        [Test]
        public void Delete_ShouldDeleteNewOrder()
        {
            using (var client = new NorthwindServiceClient())
            {
                //Arrange
                var id = client.CreateOrder(new OrderDto());

                //Act
                client.DeleteOrder(id);

                //Assert
                Assert.Throws<FaultException>(() => client.GetDetailedOrder(id));
            }

        }

        [Test]
        public void Delete_ShouldNotDeleteCompletedOrder()
        {
            using (var client = new NorthwindServiceClient())
            {
                //Arrange
                const int id = 11000; // existing completed order ID;

                //Act
                //Assert
                Assert.Throws<FaultException>(() => client.DeleteOrder(id));
            }

        }
    }
}
