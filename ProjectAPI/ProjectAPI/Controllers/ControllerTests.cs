//   _   _      _ _                       
//  | | | |_ _ (_) |___ _____ _ _ ___ ___ 
//  | |_| | ' \| |  _\ V / -_) '_(_-</ -_)
//   \___/|_||_|_|\__|\_/\___|_| /__/\___|

// This file was generated with Unitverse

using Microsoft.AspNetCore.Mvc;

namespace ProjectAPI.Controllers.Tests
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Microsoft.Extensions.Configuration;
    using NSubstitute;
    using NUnit.Framework;
    using ProjectAPI.Controllers;
    using ProjectAPI.Data;
    using ProjectAPI.Data.Models.View;

    [TestFixture]
    public class ControllerTests
    {
        private Controller _testClass;
        private ApiContext _context;
        private IMapper _mapper;
        private IConfiguration _configuration;

        [SetUp]
        public void SetUp()
        {
            _context = new ApiContext();
            _mapper = Substitute.For<IMapper>();
            _configuration = Substitute.For<IConfiguration>();
            _testClass = new Controller(_context, _mapper, _configuration);
        }

        [Test]
        public void CanConstruct()
        {
            // Act
            var instance = new Controller(_context, _mapper, _configuration);

            // Assert
            Assert.That(instance, Is.Not.Null);
        }

        // [Test]
        // public void CanCallAddProduct()
        // {
        //     // Arrange
        //     var model = new ProductSavingModel
        //     {
        //         Name = "Some Product 48",
        //         Description = "Some description",
        //         Price = 29290.5078F
        //     };
        //
        //     // Act
        //     var result = _testClass.AddProduct(model);
        //     if (result.Value != null)
        //     {
        //         Assert.Pass("Add Product works");
        //     }
        //
        //     // Assert
        //     Assert.Fail("Create or modify test");
        // }

        [Test]
        public void CanCallRemoveProducts()
        {
            // Arrange
            var model = new ProductDeletingModel { Ids = new List<string>() };

            // Act
            var result = _testClass.RemoveProducts(model);
            if (result is OkResult)
            {
                Assert.Pass("Delete Product works");
            }

            // Assert
            Assert.Fail("Create or modify test");
        }


        // [Test]
        // public void CanCallGetProducts()
        // {
        //     // Arrange
        //     var model = new RequestModel { Ids = null };
        //
        //     // Act
        //     var result = _testClass.GetProducts(model);
        //     if (result.Value != null)
        //     {
        //         Assert.Pass("Get Product Passed");
        //     }
        //
        //     // Assert
        //     Assert.Fail("Create or modify test");
        // }
        // [Test]
        // public void CanCallGetProduct()
        // {
        //     // Arrange
        //     var id = "Product-638310072977884927";
        //
        //     // Act
        //     var result = _testClass.GetProduct(id);
        //     if (result.Value != null)
        //     {
        //         Assert.Pass("Get Product Passed");
        //     }
        //
        //     // Assert
        //     Assert.Fail("Create or modify test");
        // }


        // [Test]
        // public void CanCallUpdateProduct()
        // {
        //     // Arrange
        //     var id = "Product-638311971723522370";
        //     var model = new ProductViewModel
        //     {
        //         Id = "Product-638311971723522370",
        //         Name = "Some New Name For Product",
        //         Description = "Some Other Description",
        //         Price = 19553.85F
        //     };
        //
        //     // Act
        //     var result = _testClass.UpdateProduct(id, model);
        //     if (result.Value != null)
        //     {
        //         Assert.Pass("Update Product Works");
        //     }
        //
        //     // Assert
        //     Assert.Fail("Create or modify test");
        // }

        [Test]
        public void CanCallAddOrder()
        {
            var prodList = new List<OrderProductModel>();
            var product = new OrderProductModel()
            {
                ProductId = "Product-638310072977884913",
                Quantity = 10
            };
            prodList.Add(product);
            // Arrange
            var model = new OrderSavingModel
            {
                CustomerId = "Customer-243",
                Products = prodList
            };

            // Act
            var result = _testClass.AddOrder(model);
            if (result.Value != null)
            {
                Assert.Pass("Add Order Passed");
            }

            // Assert
            Assert.Fail("Create or modify test");
        }

        [Test]
        public void CanCallGetOrders()
        {
            // Arrange
            var model = new GetOrderRequestModel
            {
                Ids = new List<string>(),
                CustomerId = "Customer-243"
            };

            // Act
            var result = _testClass.GetOrders(model);
            if (result.Value != null)
            {
                Assert.Pass("Get Orders Work");
            }

            // Assert
            Assert.Fail("Create or modify test");
        }

        [Test]
        public void CanCallEditOrder()
        {
            // Arrange
            var id = "Order-638311936509639582";
            var prodList = new List<OrderProductModel>();
            var product = new OrderProductModel()
            {
                ProductId = "Product-638310072977884913",
                Quantity = 10
            };
            prodList.Add(product);
            var model = new OrderEditingModel
            {
                OrderId = "Order-638311936509639582",
                CustomerId = "Customer-243",
                Products = prodList
            };

            // Act
            var result = _testClass.EditOrder(id, model);
            if (result.Value != null)
            {
                Assert.Pass("Edit Order Passed");
            }

            // Assert
            Assert.Fail("Create or modify test");
        }


        [Test]
        public void CanCallGetOrder()
        {
            // Arrange
            var id = "Order-638310953257182104";

            // Act
            var result = _testClass.GetOrder(id);
            if (result.Value != null)
            {
                Assert.Pass("Get Order Passed");
            }

            // Assert
            Assert.Fail("Create or modify test");
        }

        [Test]
        public void CanCallAddCustomer()
        {
            // Arrange
            var model = new CustomerViewModel
            {
                Name = "Customer 1",
                Email = "customer13@gmail.com",
                Password = "Happy",
                UserRole = 1
            };

            // Act
            var result = _testClass.AddCustomer(model);
            if (result is OkResult)
            {
                Assert.Pass("Customer Created Successfully");
            }


            // Assert
            Assert.Fail("Create or modify test");
        }


        [Test]
        public void CanCallDeleteCustomer()
        {
            // Arrange
            var id = "Customer-638311990629454301";

            // Act
            var result = _testClass.DeleteCustomer(id);
            if (result is OkResult)
            {
                Assert.Pass("Delete Customer Passed");
            }

            // Assert
            Assert.Fail("Create or modify test");
        }
    }
}