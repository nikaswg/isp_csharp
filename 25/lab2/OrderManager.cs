using System;
using System.Collections.Generic;

// 18. Собственный интерфейс
public interface IOrderManager
{
    void CreateOrder(Customer customer, List<Product> products);
    void ViewOrders();
}

// 16. Агрегация классов
public class OrderManager : IOrderManager
{
    private List<Order> orders = new List<Order>();

    public void CreateOrder(Customer customer, List<Product> products)
    {
        var order = new InternetOrder(customer, 5.99); // Каждый интернет-заказ имеет фиксированную стоимость доставки
        foreach (var product in products)
        {
            order.AddProduct(product);
        }
        orders.Add(order);
        Store.IncrementOrderCount();
    }

    public void ViewOrders()
    {
        if (orders.Count == 0)
        {
            Console.WriteLine("Нет заказов.");
            return;
        }

        foreach (var order in orders)
        {
            Console.WriteLine(order);
        }
    }
}