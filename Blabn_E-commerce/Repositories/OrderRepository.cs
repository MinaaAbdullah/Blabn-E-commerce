using Blabn_E_commerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Blabn_E_commerce.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        Blabn_Context _context;
        public OrderRepository()
        {
            _context = new Blabn_Context();
        }
        
        

        public void CreateOrder(Order order)
        {
            _context.Orders.Add(order);
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.Include(o => o.OrderDetails).FirstOrDefault(o => o.OrderId == orderId);
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders.Include(o => o.OrderDetails).ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            // Check if the order exists in the context
            var existingOrder = _context.Orders.Include(o => o.OrderDetails)
                                                .ThenInclude(od => od.Product) // Include related OrderDetails and Products
                                                .FirstOrDefault(o => o.OrderId == order.OrderId);

            if (existingOrder != null)
            {
                // Update the existing order's properties
                existingOrder.UserId = order.UserId; // Update UserId if needed
                existingOrder.OrderDate = order.OrderDate; // Update order date if needed
                existingOrder.TotalAmount = order.TotalAmount; // Update total amount if needed
                existingOrder.OrderStatus = order.OrderStatus; // Update order status if needed

                // Update OrderDetails
                foreach (var orderDetail in order.OrderDetails)
                {
                    var existingDetail = existingOrder.OrderDetails.FirstOrDefault(od => od.OrderDetailId == orderDetail.OrderDetailId);
                    if (existingDetail != null)
                    {
                        // Update existing OrderDetail
                        existingDetail.Quantity = orderDetail.Quantity;
                        existingDetail.Price = orderDetail.Price; // Assuming you want to update the price as well
                    }
                    else
                    {
                        // Optionally add new OrderDetails if they don't exist
                        existingOrder.OrderDetails.Add(new OrderDetail
                        {
                            ProductId = orderDetail.ProductId,
                            Quantity = orderDetail.Quantity,
                            Price = orderDetail.Price
                        });
                    }
                }

                // Remove OrderDetails that are no longer in the updated order
                foreach (var existingDetail in existingOrder.OrderDetails.ToList())
                {
                    if (!order.OrderDetails.Any(od => od.OrderDetailId == existingDetail.OrderDetailId))
                    {
                        _context.OrderDetails.Remove(existingDetail);
                    }
                }

                // Mark the existing order as modified
                _context.Entry(existingOrder).State = EntityState.Modified;
            }
        }


        public void DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order); // Remove the order from the context
            }
        }
    }
}
