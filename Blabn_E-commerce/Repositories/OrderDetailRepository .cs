using Blabn_E_commerce.Models;
using System.Collections.Generic;
using System.Linq;

namespace Blabn_E_commerce.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        Blabn_Context _context;
        public OrderDetailRepository()
        {
            _context = new Blabn_Context();
        }

        // Add a new OrderDetail to the database
        public void Add(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
        }

        // Retrieve an OrderDetail by its ID
        public OrderDetail Get(int id)
        {
            return _context.OrderDetails.FirstOrDefault(od => od.OrderDetailId == id);
        }

        // Retrieve all OrderDetails
        public IEnumerable<OrderDetail> GetAll()
        {
            return _context.OrderDetails.ToList();
        }

        // Update an existing OrderDetail
        public void Update(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
        }

        // Delete an OrderDetail by its ID
        public void Delete(int id)
        {
            var orderDetail = Get(id);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
            }
        }

        // Save changes to the database
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
