using System;
using System.Collections.Generic;

namespace Online_Order_Processing
{
    // ================= DELEGATE =================
    public delegate void NotifyOrder(string message);

    // ================= ENUM =================
    public enum OrderStatus
    {
        Created,
        Paid,
        Packed,
        Shipped,
        Delivered,
        Cancelled
    }

    // ================= ENTITIES =================
    public class Product
    {
        public int Id { get; }
        public string Name { get; }
        public decimal Price { get; }

        public Product(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }

    public class Customer
    {
        public int Id { get; }
        public string Name { get; }

        public Customer(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    // Composition
    public class OrderItem
    {
        public Product Product { get; }
        public int Quantity { get; }

        public OrderItem(Product product, int qty)
        {
            Product = product;
            Quantity = qty;
        }

        public decimal SubTotal => Product.Price * Quantity;
    }

    // Status history log
    public class OrderStatusLog
    {
        public OrderStatus OldStatus { get; }
        public OrderStatus NewStatus { get; }
        public DateTime Time { get; }

        public OrderStatusLog(OrderStatus oldS, OrderStatus newS)
        {
            OldStatus = oldS;
            NewStatus = newS;
            Time = DateTime.Now;
        }
    }

    // ================= ORDER =================
    public class Order
    {
        public int OrderId { get; }
        public Customer Customer { get; }
        public List<OrderItem> Items { get; } = new List<OrderItem>();
        public OrderStatus Status { get; private set; }

        public List<OrderStatusLog> History { get; } = new List<OrderStatusLog>();

        public Order(int id, Customer customer)
        {
            OrderId = id;
            Customer = customer;
            Status = OrderStatus.Created;
        }

        public decimal CalculateTotal()
        {
            decimal total = 0;
            foreach (var item in Items)
                total += item.SubTotal;
            return total;
        }

        // Status transition validation
        public bool ChangeStatus(OrderStatus newStatus, NotifyOrder notify)
        {
            if (Status == OrderStatus.Cancelled)
            {
                Console.WriteLine(" Cancelled order cannot progress.");
                return false;
            }

            if (!IsValidTransition(Status, newStatus))
            {
                Console.WriteLine($" Invalid transition: {Status} → {newStatus}");
                return false;
            }

            var old = Status;
            Status = newStatus;
            History.Add(new OrderStatusLog(old, newStatus));

            notify?.Invoke(
                $"Order {OrderId} | {Customer.Name} | {old} → {newStatus}"
            );

            return true;
        }

        private bool IsValidTransition(OrderStatus oldS, OrderStatus newS)
        {
            return (oldS, newS) switch
            {
                (OrderStatus.Created, OrderStatus.Paid) => true,
                (OrderStatus.Paid, OrderStatus.Packed) => true,
                (OrderStatus.Packed, OrderStatus.Shipped) => true,
                (OrderStatus.Shipped, OrderStatus.Delivered) => true,
                (_, OrderStatus.Cancelled) => true,
                _ => false
            };
        }

        public void PrintSummary()
        {
            Console.WriteLine($"OrderId: {OrderId}, Customer: {Customer.Name}");
            foreach (var i in Items)
                Console.WriteLine($"  {i.Product.Name} x {i.Quantity} = {i.SubTotal}");
            Console.WriteLine($"Total: {CalculateTotal()}");
            Console.WriteLine($"Current Status: {Status}");
        }

        public void PrintTimeline()
        {
            Console.WriteLine("Status History:");
            foreach (var h in History)
                Console.WriteLine($"{h.Time} | {h.OldStatus} → {h.NewStatus}");
        }
    }

    // ================= SERVICE =================
    public class OrderService
    {
        public void UpdateStatus(Order order, OrderStatus status, NotifyOrder notify)
        {
            order.ChangeStatus(status, notify);
        }
    }

    // ================= PROCESS (IN-MEMORY STORE) =================
    public static class ProcessStore
    {
        public static Dictionary<int, Product> Products = new();
        public static List<Customer> Customers = new();
        public static List<Order> Orders = new();
    }
}
