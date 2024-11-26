using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mini_Inventory_Management_System.ApplicationDbContext;
using Mini_Inventory_Management_System.Models;
using Mini_Inventory_Management_System.Services.Interfaces;

namespace Mini_Inventory_Management_System.Controllers
{
    public class OrderController : Controller
    {

        private readonly IProductService _productService;
        private readonly IOrderService _orderService;


        public OrderController(IProductService productService, IOrderService orderService)
        { 
            _productService = productService;
            _orderService = orderService;

        }

        public async Task<IActionResult> Create() 
        {
            IEnumerable<Product> products = await _productService.GetAllProducts();

            ViewData["Products"] = new SelectList(products, "Id", "Name"); 
            return View(); 
        }

        [HttpPost] 
        public async Task<IActionResult> Create(int productId, int quantity) 
        { 
            var product = await _productService.GetProductById(productId); 
            if (product == null || product.Stock < quantity) 
            {
                IEnumerable<Product> products = await _productService.GetAllProducts();

                ModelState.AddModelError("", $"Insufficient stock, Selected product having stock count maximum:- {product?.Stock}"); 
                ViewData["Products"] = new SelectList(products, "Id", "Name"); 
                return View(); 
            } 
            var order = new Order 
            { 
                ProductId = productId, 
                Quantity = quantity, 
                Product = product 
            }; 

            product.Stock -= quantity; 

            await _orderService.CreateOrder(order);

            return RedirectToAction("Index", "Product");
        }
      
    }

}
