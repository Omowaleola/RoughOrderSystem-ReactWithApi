//   _   _      _ _                       
//  | | | |_ _ (_) |___ _____ _ _ ___ ___ 
//  | |_| | ' \| |  _\ V / -_) '_(_-</ -_)
//   \___/|_||_|_|\__|\_/\___|_| /__/\___|

// This file was generated with Unitverse
// It has not been added to a test project because generation of a detached file was selected in the user interface.
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

        [Test]
        public void CannotConstructWithNullContext()
        {
            Assert.Throws<ArgumentNullException>(() => new Controller(default(ApiContext), _mapper, _configuration));
        }

        [Test]
        public void CannotConstructWithNullMapper()
        {
            Assert.Throws<ArgumentNullException>(() => new Controller(_context, default(IMapper), _configuration));
        }

        [Test]
        public void CannotConstructWithNullConfiguration()
        {
            Assert.Throws<ArgumentNullException>(() => new Controller(_context, _mapper, default(IConfiguration)));
        }

        [Test]
        public void CanCallAddProduct()
        {
            // Arrange
            var model = new ProductViewModel
            {
                Id = "TestValue876554053",
                Name = "TestValue1353184132",
                Description = "TestValue1366502065",
                Price = 14738.2012F
            };

            // Act
            var result = _testClass.AddProduct(model);

            // Assert
            Assert.Fail("Create or modify test");
        }

        [Test]
        public void CannotCallAddProductWithNullModel()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.AddProduct(default(ProductViewModel)));
        }

        [Test]
        public void CanCallRemoveProducts()
        {
            // Arrange
            var ids = new List<string>();

            // Act
            var result = _testClass.RemoveProducts(ids);

            // Assert
            Assert.Fail("Create or modify test");
        }

        [Test]
        public void CannotCallRemoveProductsWithNullIds()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.RemoveProducts(default(List<string>)));
        }

        [Test]
        public void CanCallGetProducts()
        {
            // Arrange
            var ids = new List<string>();

            // Act
            var result = _testClass.GetProducts(ids);

            // Assert
            Assert.Fail("Create or modify test");
        }

        [Test]
        public void CanCallGetProduct()
        {
            // Arrange
            var id = "TestValue646055558";

            // Act
            var result = _testClass.GetProduct(id);

            // Assert
            Assert.Fail("Create or modify test");
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void CannotCallGetProductWithInvalidId(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.GetProduct(value));
        }

        [Test]
        public void CanCallUpdateProduct()
        {
            // Arrange
            var id = "TestValue502446444";
            var model = new ProductViewModel
            {
                Id = "TestValue815885233",
                Name = "TestValue494147858",
                Description = "TestValue1166372064",
                Price = 11354.9609F
            };

            // Act
            var result = _testClass.UpdateProduct(id, model);

            // Assert
            Assert.Fail("Create or modify test");
        }

        [Test]
        public void CannotCallUpdateProductWithNullModel()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.UpdateProduct("TestValue411335927", default(ProductViewModel)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void CannotCallUpdateProductWithInvalidId(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.UpdateProduct(value, new ProductViewModel
            {
                Id = "TestValue96801108",
                Name = "TestValue268315516",
                Description = "TestValue1774227877",
                Price = 351.286743F
            }));
        }

        [Test]
        public void CanCallAddOrder()
        {
            // Arrange
            var model = new OrderSavingModel
            {
                CustomerId = "TestValue871451772",
                Products = new List<OrderProductModel>()
            };

            // Act
            var result = _testClass.AddOrder(model);

            // Assert
            Assert.Fail("Create or modify test");
        }

        [Test]
        public void CannotCallAddOrderWithNullModel()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.AddOrder(default(OrderSavingModel)));
        }

        [Test]
        public void CanCallGetOrders()
        {
            // Arrange
            var model = new GetOrderRequestModel
            {
                Ids = new List<string>(),
                CustomerId = "TestValue1929518756"
            };

            // Act
            var result = _testClass.GetOrders(model);

            // Assert
            Assert.Fail("Create or modify test");
        }

        [Test]
        public void CannotCallGetOrdersWithNullModel()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.GetOrders(default(GetOrderRequestModel)));
        }

        [Test]
        public void CanCallEditOrder()
        {
            // Arrange
            var id = "TestValue1735226584";
            var model = new OrderEditingModel
            {
                OrderId = "TestValue55874653",
                CustomerId = "TestValue1551450219",
                Products = new List<OrderProductModel>()
            };

            // Act
            var result = _testClass.EditOrder(id, model);

            // Assert
            Assert.Fail("Create or modify test");
        }

        [Test]
        public void CannotCallEditOrderWithNullModel()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.EditOrder("TestValue1345155157", default(OrderEditingModel)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void CannotCallEditOrderWithInvalidId(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.EditOrder(value, new OrderEditingModel
            {
                OrderId = "TestValue1996624664",
                CustomerId = "TestValue1076685215",
                Products = new List<OrderProductModel>()
            }));
        }

        [Test]
        public void CanCallGetOrder()
        {
            // Arrange
            var id = "TestValue555323867";

            // Act
            var result = _testClass.GetOrder(id);

            // Assert
            Assert.Fail("Create or modify test");
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void CannotCallGetOrderWithInvalidId(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.GetOrder(value));
        }

        [Test]
        public void CanCallAddCustomer()
        {
            // Arrange
            var model = new CustomerViewModel
            {
                Id = "TestValue899734619",
                Name = "TestValue1638438538",
                Email = "TestValue1925074743",
                Password = "TestValue295802028",
                UserRole = 1833140184
            };

            // Act
            var result = _testClass.AddCustomer(model);

            // Assert
            Assert.Fail("Create or modify test");
        }

        [Test]
        public void CannotCallAddCustomerWithNullModel()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.AddCustomer(default(CustomerViewModel)));
        }

        [Test]
        public void CanCallDeleteCustomer()
        {
            // Arrange
            var id = "TestValue817800248";

            // Act
            var result = _testClass.DeleteCustomer(id);

            // Assert
            Assert.Fail("Create or modify test");
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void CannotCallDeleteCustomerWithInvalidId(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.DeleteCustomer(value));
        }

        [Test]
        public void CanCallLogin()
        {
            // Arrange
            var model = new LoginRequestModel
            {
                Email = "TestValue1235709535",
                Password = "TestValue1487957374"
            };

            // Act
            var result = _testClass.Login(model);

            // Assert
            Assert.Fail("Create or modify test");
        }

        [Test]
        public void CannotCallLoginWithNullModel()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.Login(default(LoginRequestModel)));
        }
    }
}