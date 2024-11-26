using Mini_Inventory_Management_System.Models;

namespace Mini_Inventory_Management_System.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(Order order);
    }
}
