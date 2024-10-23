using Blabn_E_commerce.Models;

namespace Blabn_E_commerce.Repositories
{
    public interface IOrderDetailRepository
    {
        public void Add(OrderDetail orderDetail);  // Add a new OrderDetail
        public OrderDetail Get(int id);            // Retrieve an OrderDetail by its ID
        public IEnumerable<OrderDetail> GetAll();  // Retrieve all OrderDetails
        public void Update(OrderDetail orderDetail); // Update an existing OrderDetail
        public void Delete(int id);                // Delete an OrderDetail by its ID
        public void SaveChanges();                 // Save changes to the database
    }
}
