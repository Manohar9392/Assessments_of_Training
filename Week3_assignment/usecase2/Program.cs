using System;
using Online_Order_Processing;

class Program
{
    // ================= NOTIFICATION METHODS =================
    static void CustomerNotification(string msg)
    {
        Console.WriteLine($" Customer Notification: {msg}");
    }

    static void LogisticsNotification(string msg)
    {
        Console.WriteLine($"Logistics Notification: {msg}");
    }

    // ================= MAIN =================
    static void Main()
    {
        NotifyOrder notify = CustomerNotification;
        notify += LogisticsNotification; // multicast delegate

        OrderService service = new OrderService();

        int choice = -1;
        bool flag = true;

        while (flag)
        {
            Console.WriteLine("\n========= ONLINE ORDER PROCESSING =========");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Add Customer");
            Console.WriteLine("3. Create Order");
            Console.WriteLine("4. Add Item to Order");
            Console.WriteLine("5. Change Order Status");
            Console.WriteLine("6. Print Order Summary");
            Console.WriteLine("7. Print Order Status Timeline");
            Console.WriteLine("0. Exit");
            Console.Write("Enter Choice: ");

            choice = int.TryParse(Console.ReadLine(), out choice) ? choice : -1;

            switch (choice)
            {
                case 1:
                    Console.Write("Enter Product Id: ");
                    int pid = int.Parse(Console.ReadLine());

                    Console.Write("Enter Product Name: ");
                    string pname = Console.ReadLine();

                    Console.Write("Enter Price: ");
                    decimal price = decimal.Parse(Console.ReadLine());

                    ProcessStore.Products.Add(pid, new Product(pid, pname, price));
                    Console.WriteLine(" Product added successfully.");
                    break;

                case 2:
                    Console.Write("Enter Customer Id: ");
                    int cid = int.Parse(Console.ReadLine());

                    Console.Write("Enter Customer Name: ");
                    string cname = Console.ReadLine();

                    ProcessStore.Customers.Add(new Customer(cid, cname));
                    Console.WriteLine(" Customer added successfully.");
                    break;

                case 3:
                    Console.Write("Enter Order Id: ");
                    int oid = int.Parse(Console.ReadLine());

                    Console.Write("Enter Customer Id: ");
                    cid = int.Parse(Console.ReadLine());

                    Customer cust = ProcessStore.Customers
                        .Find(c => c.Id == cid);

                    if (cust == null)
                    {
                        Console.WriteLine(" Customer not found.");
                        break;
                    }

                    ProcessStore.Orders.Add(new Order(oid, cust));
                    Console.WriteLine("Order created successfully.");
                    break;

                case 4:
                    Console.Write("Enter Order Id: ");
                    oid = int.Parse(Console.ReadLine());

                    Order order = ProcessStore.Orders
                        .Find(o => o.OrderId == oid);

                    if (order == null)
                    {
                        Console.WriteLine(" Order not found.");
                        break;
                    }

                    Console.Write("Enter Product Id: ");
                    pid = int.Parse(Console.ReadLine());

                    if (!ProcessStore.Products.ContainsKey(pid))
                    {
                        Console.WriteLine(" Product not found.");
                        break;
                    }

                    Console.Write("Enter Quantity: ");
                    int qty = int.Parse(Console.ReadLine());

                    order.Items.Add(
                        new OrderItem(ProcessStore.Products[pid], qty)
                    );

                    Console.WriteLine(" Item added to order.");
                    break;

                case 5:
                    Console.Write("Enter Order Id: ");
                    oid = int.Parse(Console.ReadLine());

                    order = ProcessStore.Orders
                        .Find(o => o.OrderId == oid);

                    if (order == null)
                    {
                        Console.WriteLine(" Order not found.");
                        break;
                    }

                    Console.WriteLine("Choose Status:");
                    Console.WriteLine("1. Paid");
                    Console.WriteLine("2. Packed");
                    Console.WriteLine("3. Shipped");
                    Console.WriteLine("4. Delivered");
                    Console.WriteLine("5. Cancelled");

                    int s = int.Parse(Console.ReadLine());

                    OrderStatus newStatus = s switch
                    {
                        1 => OrderStatus.Paid,
                        2 => OrderStatus.Packed,
                        3 => OrderStatus.Shipped,
                        4 => OrderStatus.Delivered,
                        5 => OrderStatus.Cancelled,
                        _ => order.Status
                    };

                    service.UpdateStatus(order, newStatus, notify);
                    break;

                case 6:
                    Console.Write("Enter Order Id: ");
                    oid = int.Parse(Console.ReadLine());

                    order = ProcessStore.Orders
                        .Find(o => o.OrderId == oid);

                    if (order != null)
                        order.PrintSummary();
                    else
                        Console.WriteLine("Order not found.");
                    break;

                case 7:
                    Console.Write("Enter Order Id: ");
                    oid = int.Parse(Console.ReadLine());

                    order = ProcessStore.Orders
                        .Find(o => o.OrderId == oid);

                    if (order != null)
                        order.PrintTimeline();
                    else
                        Console.WriteLine(" Order not found.");
                    break;

                case 0:
                    flag = false;
                    Console.WriteLine("Thank you for using Order Processing System.");
                    break;

                default:
                break;
            }
        }
    }
}
