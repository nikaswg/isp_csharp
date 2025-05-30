public class SportShop : IOrderManager //наследование
{
    private static int orderCount = 0;
    private List<Order> orders = new List<Order>();
    private List<SportItem> items = new List<SportItem>();

    // 18. Применение интерфейса
    public void AddItemToOrder(SportItem item)
    {
        if (orders.Count == 0)
        {
            Console.WriteLine("Сначала создайте заказ.");
            return;
        }
        var lastOrder = orders[^1] as SportItemOrder; // Получаем последний заказ
        lastOrder?.AddItem(item); // Добавляем товар в заказ
        Console.WriteLine($"Товар '{item.Name}' добавлен в заказ.");
    }

    public void CreateOrder(Customer customer)
    {
        var order = new SportItemOrder(customer);
        orders.Add(order);
        orderCount++;
    }

    public void AddItem(SportItem item)
    {
        items.Add(item);
    }

    public void ViewItems()
    {
        Console.WriteLine("Доступные товары:");
        foreach (var item in items)
        {
            Console.WriteLine(item.DisplayInfo);
        }
    }

    public void ViewOrders()
    {
        Console.WriteLine("Заказы:");
        foreach (var order in orders)
        {
            Console.WriteLine(order.ToString());
        }
    }

    public SportItem FindItem(string name)
    {
        return items.Find(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public static int OrderCount => orderCount; // 8. Использование статических элементов;
}

// 15. Использование методов расширения;
public static class SportItemExtensions
{
    public static void PrintInfo(this SportItem item)
    {
        Console.WriteLine($"Товар: {item.Name}, Цена: {item.Price:C}");
    }
}