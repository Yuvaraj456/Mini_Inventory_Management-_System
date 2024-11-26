using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mini_Inventory_Management_System.ApplicationDbContext;
using Mini_Inventory_Management_System.Models;
using Mini_Inventory_Management_System.Services.Interfaces;
using System.Diagnostics;

namespace Mini_Inventory_Management_System.Controllers
{ 
    public class ProductController : Controller
    {

        private readonly IProductService _productService;
        private readonly IOrderService _orderService;


        public ProductController(AppDbContext context, IProductService productService, IOrderService orderService) 
        { 
            _productService = productService;
            _orderService = orderService;
        }

        [Route("/")]
        public async Task<IActionResult> Index(string sortBy="", string sortOrder="", string searchBy="", string searchString="") 
        {
            ViewData["currentSortBy"] = sortBy;
            ViewData["currentSortOrder"] = sortOrder;
            ViewData["currentSearchBy"] = searchBy; 
            ViewData["currentSearchString"] = searchString;


            ViewBag.Search = new Dictionary<string, string>()
            {
               { nameof(Product.Name), "Name" },
               { nameof(Product.Price), "Price" },
               { nameof(Product.Stock), "Stock" },
            };

            IEnumerable<Product> products = _productService.GetAllProducts().Result;

            IEnumerable<Product> filteredProducts =  _productService.FilterBy(searchBy, searchString, products);

            IEnumerable<Product> sortedProducts = await _productService.SortBy(sortBy, sortOrder, filteredProducts);

            return View(sortedProducts);
        }

        public IActionResult Create() 
        { 
            return View(); 
        }

        [HttpPost] 
        public async Task<IActionResult> Create(Product product) 
        { 
            if (ModelState.IsValid) 
            { 
                await _productService.CreateProduct(product);
                return RedirectToAction(nameof(Index)); 
            } 
            return View(product); 
        }

        public async Task<IActionResult> Edit(int? id) 
        { 
            if (id == null) 
            { 
                return NotFound();      
            }
            var product = await _productService.GetProductById(id);

            if (product == null) 
            { 
                return NotFound(); 
            } 
            return View(product); 
        }

        [HttpPost] 
        public async Task<IActionResult> Edit([FromForm]Product product)
        { 
            
            if (ModelState.IsValid && ProductExists(product.Id)) 
            {
                try { 
                    await _productService.EditProduct(product);
                } 
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.InnerException);
                } 
                return RedirectToAction(nameof(Index)); 
            } 
            return View(product); 
        }

        public async Task<IActionResult> Delete(int? id) 
        { 
            if (id == null) 
            {
                return NotFound();
            } 
            var product = await _productService.GetProductById(id); 
            if (product == null) 
            {
                return NotFound();
            } 
            return View(product);
        }

        [HttpPost] 
        public async Task<IActionResult> Delete(int id) 
        { 
            Product? product = await _productService.GetProductById(id);

            if(product == null)
            {
                return NotFound();
            }
            await _productService.DeleteProductById(product);
                       
            
            return RedirectToAction(nameof(Index)); 
        }

        private bool ProductExists(int id) 
        {
            return _productService.IsProductExists(id);
        }
    }
}
