using Blabn_E_commerce.Models;
using System.Collections.Generic;

namespace Blabn_E_commerce.Repositories
{
    public interface IOrderRepository
    {
        public void CreateOrder(Order order);
        public Order GetOrderById(int orderId);
        public List<Order> GetAllOrders();
        public void Save();
        public void UpdateOrder(Order order);
        public void DeleteOrder(int id);
    }
}
