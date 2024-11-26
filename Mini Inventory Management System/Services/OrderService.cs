using Mini_Inventory_Management_System.ApplicationDbContext;
using Mini_Inventory_Management_System.Models;
using Mini_Inventory_Management_System.Services.Interfaces;

namespace Mini_Inventory_Management_System.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateOrder(Order order)
        {

            _context.Orders.Add(order);

            await _context.SaveChangesAsync();
        }
    }
}
