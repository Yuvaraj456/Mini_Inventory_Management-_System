using Mini_Inventory_Management_System.Models;

namespace Mini_Inventory_Management_System.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();

        Task CreateProduct(Product product);

        Task EditProduct(Product product);

        Task<Product?> GetProductById(int? productId);

        Task DeleteProductById(Product? product);

        bool IsProductExists(int id);

        Task<IEnumerable<Product>> SortBy(string sortBy,string sortOrder, IEnumerable<Product> products);

        IEnumerable<Product> FilterBy(string searchBy, string searchString, IEnumerable<Product> products);

    }
}
