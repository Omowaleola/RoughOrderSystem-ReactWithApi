using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Data;
using ProjectAPI.Data.Models;
using ProjectAPI.Data.Models.View;

namespace ProjectAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class Controller : ControllerBase
{
    private readonly IMapper mapper;
    private ApiContext context;
    
    [HttpPost("product")]
    public ActionResult<ProductViewModel> AddProduct(ProductViewModel model)
    {
        if (context.Products.Any(p => p.Name == model.Name))
        {
            return BadRequest("Product with name already exists");
        }

        var product = new Product
        {
            Id = GenerateId("Product"),
            Name = model.Name,
            Description = model.Description,
            Price = model.Price
        };
        context.Products.Add(product);
        context.SaveChanges();
        model.Id = product.Id;
        return model;

    }

    private string GenerateId(string type)
    {
        var time = DateTime.Now.Millisecond.ToString();
        return string.Join('-', type, time);
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
        return Ok();
    }
    [HttpGet("products")]
    public ActionResult<List<ProductViewModel>> GetProducts(List<string>? ids)
    {
        var allProducts = ids == null || ids.Count == 0;
        var products = context.Products
            .Where(p => allProducts ||(ids != null &&  ids.Contains(p.Id)))
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
    [HttpPut("")]
    public ActionResult<ProductViewModel> UpdateProduct(ProductViewModel model)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == model.Id);
        if (product == null)
        {
            return NotFound("Product Not Found");
        }
        if (model.Name != product.Name && context.Products.Any(p => p.Name == model.Name)  )
        {
            return BadRequest("Product with name already exists");
        }
        product.Description = model.Description;
        product.Name = model.Name;
        product.Price = model.Price;

        context.SaveChanges();
        return Ok();
    }
    
    [HttpPost("order")]
    public IActionResult AddOrder(OrderViewModel model)
    {
        using var transaction = context.Database.BeginTransaction();
        var order = new Order
        {
            Id = GenerateId("Order"),
            Paid = model.Paid,
            Total = model.Total,
            CustomerId = model.CustomerId,
            CreatedOn = DateTime.Now
        };
        context.Orders.Add(order);
        context.SaveChanges();
        var products = new List<OrderProduct>();
        foreach (var prod in model.Products)
        {
            if (!context.Products.Any(p => p.Id == prod))
            {
                transaction?.Rollback();
                return NotFound("Product Not Found");
            }

            var temp = new OrderProduct
            {
                OrderId = order.Id,
                ProductId = prod
            };
            products.Add(temp);
        }
        context.OrderProducts.AddRange(products);
        context.SaveChanges();
        transaction?.Commit();
        return Ok();
    }
    // [HttpPost("orders")]
    // public IActionResult AddOrders(int id)
    // {
    //     return null;
    // }
    [HttpPut("order/{id}")]
    public IActionResult EditOrder(string id, OrderViewModel model )
    {
        var order = context.Orders.FirstOrDefault(o => o.Id == id);
        if (order == null)
        {
            return NotFound("Order Not Found");
        }
        
        order.Paid = model.Paid;
        order.Total = model.Total;
        order.CustomerId = model.CustomerId;
        context.SaveChanges();
        var currentProducts = context.OrderProducts.Where(op => op.OrderId == model.Id);
        var curProductIds = currentProducts.Select(p => p.ProductId);
        var IdsToDelete = curProductIds.Where(p => !model.Products.Contains(p));
        var IdsToAdd = model.Products.Where(p => !curProductIds.Contains(p));
        var prodToDelete = currentProducts.Where(p => IdsToDelete.Contains(p.ProductId));
        var ToAdd = new List<OrderProduct>();
        foreach (var prodId in IdsToAdd)
        {
            var temp = new OrderProduct
            {
                OrderId = model.Id,
                ProductId = prodId
            };
            ToAdd.Add(temp);
        }
        context.OrderProducts.RemoveRange(prodToDelete);
        context.OrderProducts.AddRange(ToAdd);
        context.SaveChanges();
        return Ok();



    }
    // [HttpGet("")]
    // public IActionResult GetPastOrders()
    // {
    //     return null;
    // }
    
    
    // [HttpGet("customer/{id}")]
    // public IActionResult GetCustomer()
    // {
    //     return null;
    // }
    // [HttpGet("customers")]
    // public IActionResult GetCustomers()
    // {
    //     return null;
    // }
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
            Password = model.Password,
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
}