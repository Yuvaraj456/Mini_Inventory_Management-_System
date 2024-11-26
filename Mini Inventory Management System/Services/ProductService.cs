using Microsoft.EntityFrameworkCore;
using Mini_Inventory_Management_System.ApplicationDbContext;
using Mini_Inventory_Management_System.Models;
using Mini_Inventory_Management_System.Services.Interfaces;

namespace Mini_Inventory_Management_System.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateProduct(Product product)
        {
             _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductById(Product? product)
        {
            if (product == null)
            {
                return;
            }
            _context.Products.Remove(product);

            await _context.SaveChangesAsync();            
            

        }

        public async Task EditProduct(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
        }

        public  IEnumerable<Product> FilterBy(string searchBy, string searchString, IEnumerable<Product> products)
        {
            products = searchBy switch
            {
                nameof(Product.Name) =>
                  products.Where(x => x.Name.Contains(searchString,StringComparison.OrdinalIgnoreCase)).ToList(),

                nameof(Product.Price) =>
                   products.Where(x => x.Price.Equals(Convert.ToDouble(searchString))).ToList(),

                nameof(Product.Stock) =>
                     products.Where(x => x.Stock.Equals(Convert.ToInt32(searchString))).ToList(),                

                 _ => products
             };

            return products;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductById(int? productId)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
        }

        public bool IsProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        public Task<IEnumerable<Product>> SortBy(string sortBy, string sortOrder, IEnumerable<Product> products)
        {

            switch (sortBy, sortOrder)
            {
                case ("Name","asc"):
                    products = products.OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase).ToList();
                    break;

                case ("Name", "desc"):
                    products = products.OrderByDescending(p => p.Name, StringComparer.OrdinalIgnoreCase).ToList();
                    break;

                case ("Price", "asc"):
                    products = products.OrderBy(p => p.Price).ToList();
                    break;

                case ("Price", "desc"):
                    products = products.OrderByDescending(p => p.Price).ToList();
                    break;

                case ("Stock", "asc"):
                    products = products.OrderBy(p => p.Stock).ToList();
                    break;

                case ("Stock", "desc"):
                    products = products.OrderByDescending(p => p.Stock).ToList();
                    break;

                case ("Id", "desc"):
                    products = products.OrderByDescending(p => p.Id).ToList();
                    break;

                default:
                    products = products.OrderBy(p => p.Id).ToList();
                    break;
            }

            return Task.FromResult(products);
        } 
    }
}
