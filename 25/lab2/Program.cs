using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // 19. Демонстрация работы системы
        Console.WriteLine("Добро пожаловать в интернет-магазин!");

        // Создаем список продуктов
        var productList = new ProductList<Product>();
        productList.Add(new Product("Ноутбук", 55000, 10));
        productList.Add(new Product("Смартфон", 30000, 20));
        productList.Add(new Product("Наушники", 5000, 50));

        // 15. Использование методов расширения
        productList.DisplayAll();

        // Создаем клиентов
        var customer1 = new Customer("Иван Иванов", "ivan@example.com");
        var customer2 = new Customer("Мария Петрова", "maria@example.com");

        // Создаем менеджер заказов
        var orderManager = new OrderManager();

        // Создаем заказы
        orderManager.CreateOrder(customer1, new List<Product> { productList[0], productList[2] }); // Ноутбук и наушники
        orderManager.CreateOrder(customer2, new List<Product> { productList[1] }); // Смартфон

        // Просмотр заказов
        orderManager.ViewOrders();

        // Общая информация о заказах
        Console.WriteLine($"\nОбщее количество заказов: {Store.TotalOrders}");
    }
}