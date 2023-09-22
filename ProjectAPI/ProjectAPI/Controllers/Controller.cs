using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Data;
using ProjectAPI.Data.Models;
using ProjectAPI.Data.Models.View;
using Microsoft.Extensions.Configuration;

namespace ProjectAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class Controller : ControllerBase
{
    private readonly IMapper mapper;
    private ApiContext context;
    private IConfiguration Configuration { get; }

    public Controller(ApiContext context, IMapper mapper, IConfiguration configuration)
    {
        this.context = context;
        this.mapper = mapper;
        Configuration = configuration;
    }

    private string GenerateId(string type)
    {
        var time = DateTime.Now.Ticks.ToString();
        return string.Join('-', type, time);
    }

    #region Products

    [HttpPost("product")]
    public ActionResult<ProductViewModel> AddProduct(ProductViewModel model)
    {
        if (context.Products.Any(p => p.Name == model.Name))
        {
            return BadRequest("Product with name already exists");
        }

        var product = GenerateDbProduct(model);
        context.Products.Add(product);
        context.SaveChanges();
        model.Id = product.Id;
        return model;
    }

    private Product GenerateDbProduct(ProductViewModel model)
    {
        var product = new Product
        {
            Id = GenerateId("Product"),
            Name = model.Name,
            Description = model.Description,
            Price = model.Price
        };
        return product;
    }

    [HttpPost("remove-products")]
    public IActionResult RemoveProducts(List<string> ids)
    {
        if (ids.Any(id => !context.Products.Any(p => p.Id == id)))
        {
            return BadRequest("One or More Ids don't exist in the system");
        }

        var productsToRemove = context.Products.Where(p => ids.Contains(p.Id)).Select(p => p);
        context.Products.RemoveRange(productsToRemove);
        context.SaveChanges();
        return Ok();
    }

    [HttpPost("products")]
    public ActionResult<List<ProductViewModel>> GetProducts(List<string>? ids)
    {
        var allProducts = ids == null || ids.Count == 0;
        var products = context.Products
            .Where(p => allProducts || (ids != null && ids.Contains(p.Id)))
            .Select(p => p)
            .ToList();
        return mapper.Map<List<ProductViewModel>>(products);
    }

    [HttpGet("product/{id}")]
    public ActionResult<ProductViewModel> GetProduct(string id)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound("Product Not Found");
        }

        return mapper.Map<ProductViewModel>(product);
    }

    [HttpPut("product/{id}")]
    public ActionResult<ProductViewModel> UpdateProduct(string id, ProductViewModel model)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound("Product Not Found");
        }

        if (model.Name != product.Name && context.Products.Any(p => p.Name == model.Name))
        {
            return BadRequest("Product with name already exists");
        }

        product.Description = model.Description;
        product.Name = model.Name;
        product.Price = model.Price;

        context.SaveChanges();
        return Ok();
    }

    #endregion


    #region Orders

    [HttpPost("order")]
    public ActionResult<OrderViewModel> AddOrder(OrderSavingModel model)
    {
        var numRecords = context.Products.Count() + 1;
        var modelProductIds = model.Products.Select(x => x.ProductId);
        var productsToAdd = context.Products.Where(p => modelProductIds.Contains(p.Id)).ToList();
        var total = CalculateTotal(productsToAdd, model.Products);

        using var transaction = context.Database.BeginTransaction();
        var order = new Order
        {
            Id = GenerateId("Order"),
            OrderNo = GenerateId(string.Join('-', "Order", ConstantsAndMethods.ConvertNumberToCode(numRecords))),
            Paid = false,
            Total = total,
            CustomerId = model.CustomerId,
            CreatedOn = DateTime.Now
        };
        context.Orders.Add(order);
        context.SaveChanges();
        var products = new List<OrderProduct>();
        foreach (var prod in model.Products)
        {
            if (!context.Products.Any(p => p.Id == prod.ProductId))
            {
                transaction?.Rollback();
                return NotFound("Product Not Found");
            }

            var temp = new OrderProduct
            {
                OrderId = order.Id,
                ProductId = prod.ProductId,
                Quantity = prod.Quantity
            };
            products.Add(temp);
        }
        context.OrderProducts.AddRange(products);
        context.SaveChanges();
        transaction?.Commit();
        if (getOrderViewModel(order.Id, out var orderModel, out var notFound)) return notFound;
        return orderModel;
    }

    private double CalculateTotal(List<Product> productsToAdd, List<OrderProductModel> modelProducts)
    {
        var total = 0.0;
        foreach (var pTAdd in modelProducts)
        {
            var product = productsToAdd.First(p => pTAdd.ProductId == p.Id);
            total += (product.Price * pTAdd.Quantity);
        }

        return total;
    }

    [HttpPost("orders")]
    public ActionResult<List<OrderGridViewModel>> GetOrders(GetOrderRequestModel model)
    {
        var allCustomers = model.CustomerId == null || string.IsNullOrEmpty(model.CustomerId);
        var allOrders = model.Ids == null || model.Ids.Count == 0;
        var orders = from o in context.Orders
            join c in context.Customers on o.CustomerId equals c.Id
            where (allCustomers || o.CustomerId == model.CustomerId)
                  && (allOrders || model.Ids.Contains(o.Id))
            select new OrderGridViewModel()
            {
                Id = o.Id,
                OrderNo = o.OrderNo,
                Paid = o.Paid,
                Customer = c.Name,
                Total = o.Total,
                CreatedOn = o.CreatedOn
            };
        return orders.ToList();
    }

    [HttpPut("order/{id}")]
    public ActionResult<OrderViewModel> EditOrder(string id, OrderEditingModel model)
    {
        var order = context.Orders.FirstOrDefault(o => o.Id == id);
        if (order == null)
        {
            return NotFound("Order Not Found");
        }

        var modelProducts = context.Products
            .Where(p => model.Products.Select(p => p.ProductId).Contains(p.Id))
            .ToList();
        var currentProducts = context.OrderProducts.Where(op => op.OrderId == model.OrderId);
        var modelProdIds = model.Products.Select(p => p.ProductId);
        var curProductIds = currentProducts.Select(p => p.ProductId);
        var curProductToEdit = currentProducts.Where(p => modelProdIds.Contains(p.ProductId));
        var idsToDelete = curProductIds.Where(p => !modelProdIds.Contains(p));
        var idsToAdd = model.Products.Where(p => !curProductIds.Contains(p.ProductId));
        var prodToDelete = currentProducts.Where(p => idsToDelete.Contains(p.ProductId));
        var toAdd = new List<OrderProduct>();
        foreach (var prod in curProductToEdit)
        {
            prod.Quantity = model.Products.FirstOrDefault(p => p.ProductId == prod.ProductId)?.Quantity ?? 0;
        }

        foreach (var prod in idsToAdd)
        {
            var temp = new OrderProduct
            {
                OrderId = model.OrderId,
                ProductId = prod.ProductId,
                Quantity = prod.Quantity
            };
            toAdd.Add(temp);
        }

        var total = CalculateTotal(modelProducts, model.Products);
        order.Total = total;
        order.Paid = true;
        context.OrderProducts.RemoveRange(prodToDelete);
        context.OrderProducts.AddRange(toAdd);
        context.SaveChanges();
        if (getOrderViewModel(order.Id, out var orderModel, out var notFound)) return notFound;
        return orderModel;
    }

    [HttpGet("order/{id}")]
    public ActionResult<OrderViewModel> GetOrder(string id)
    {
        if (getOrderViewModel(id, out var model, out var notFound)) return notFound;
        return model;
    }

    private bool getOrderViewModel(string id, out OrderViewModel model, out ActionResult<OrderViewModel> notFound)
    {
        var orderDetails = (from o in context.Orders
            join c in context.Customers on o.CustomerId equals c.Id
            where o.Id == id
            select new
            {
                order = o,
                customer = c.Name
            }).FirstOrDefault();
        if (orderDetails == null)
        {
            {
                notFound = NotFound("Order Not Found");
                model = null;
                return true;
            }
        }

        var orderProducts = (from op in context.OrderProducts
            join p in context.Products on op.ProductId equals p.Id
            where op.OrderId == id
            select new OrderProductViewModel
            {
                Id = op.Id,
                ProductId = op.ProductId,
                Quantity = op.Quantity,
                ProductPrice = p.Price
            }).ToList();
        model = new OrderViewModel
        {
            Id = orderDetails.order.Id,
            OrderNo = orderDetails.order.OrderNo,
            Customer = orderDetails.customer,
            Paid = orderDetails.order.Paid,
            Total = orderDetails.order.Total,
            CreatedOn = orderDetails.order.CreatedOn,
            Products = orderProducts
        };
        notFound = null;
        return false;
    }

    #endregion

    #region Customers

    [HttpPost("customer")]
    public IActionResult AddCustomer(CustomerViewModel model)
    {
        if (context.Customers.Any(c => c.Email == model.Email))
        {
            return BadRequest("Email exists");
        }

        var customer = new Customer
        {
            Id = GenerateId("Customer"),
            Name = model.Name,
            Email = model.Email,
            Password = ConstantsAndMethods.EncryptUsingSha256(model.Password),
            UserRole = (int)UserTypes.Customer
        };
        context.Customers.Add(customer);
        context.SaveChanges();
        return Ok();
    }

    [HttpDelete("customer/{id}")]
    public IActionResult DeleteCustomer(string id)
    {
        var customer = context.Customers.FirstOrDefault(c => c.Id == id);
        if (customer == null)
        {
            return NotFound("Customer Not Found");
        }

        if (context.Orders.Any(o => o.CustomerId == id))
        {
            return BadRequest("Cannot Delete Customer. Order exists with customer's details");
        }

        context.Customers.Remove(customer);
        context.SaveChanges();
        return Ok();
    }

    [HttpPost("login")]
    public ActionResult<CustomerLoggedInModel> Login(LoginRequestModel model)
    {
        var customer = context.Customers.FirstOrDefault(c => c.Email.ToLower() == model.Email.ToLower());
        if (customer == null)
        {
            return BadRequest("Email or Password are incorrect");
        }

        var loginPassword = ConstantsAndMethods.EncryptUsingSha256(model.Password.Trim());
        if (!loginPassword.Equals(customer.Password))
        {
            return BadRequest("Email or Password are incorrect");
        }

        var token = ConstantsAndMethods.GenerateJwtToken(customer.Id,
            customer.UserRole.ToString(),
            customer.Email, Configuration["ApplicationSettings:JWTToken"]);
        var loggedInModel = new CustomerLoggedInModel
        {
            Id = customer.Id,
            UserRole = customer.UserRole,
            Email = customer.Email,
            Name = customer.Name,
            Token = token.Item1,
            TokenExpiry = token.Item2
        };
        return loggedInModel;
    }

    #endregion
}