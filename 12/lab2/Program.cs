using System;

class Program
{
    static void Main()
    {
        SportShop shop = new SportShop();

        // Инициализация данных
        shop.AddItem(new SportItem("Футбольный мяч", 29.99));
        shop.AddItem(new SportItem("Беговая дорожка", 499.99));
        shop.AddItem(new SportItem("Гантели", 49.99));

        while (true)
        {
            Console.WriteLine("\nМагазин спортивных товаров");
            Console.WriteLine("1. Добавить товар");
            Console.WriteLine("2. Просмотреть товары");
            Console.WriteLine("3. Создать заказ");
            Console.WriteLine("4. Добавить товар в заказ");
            Console.WriteLine("5. Просмотреть заказы");
            Console.WriteLine("6. Поиск товара");
            Console.WriteLine("7. Выход");
            Console.Write("Выберите опцию: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите название товара: ");
                    string name = Console.ReadLine();
                    Console.Write("Введите цену товара: ");
                    double price = Convert.ToDouble(Console.ReadLine());
                    shop.AddItem(new SportItem(name, price));
                    Console.WriteLine("Товар успешно добавлен!");
                    break;

                case "2":
                    shop.ViewItems();
                    break;

                case "3":
                    Console.Write("Введите имя клиента: ");
                    string customerName = Console.ReadLine();
                    Console.Write("Введите телефон клиента: ");
                    string customerPhone = Console.ReadLine();
                    shop.CreateOrder(new Customer(customerName, customerPhone));
                    Console.WriteLine("Заказ создан!");
                    break;

                case "4":
                    Console.Write("Введите название товара для добавления в заказ: ");
                    string itemName = Console.ReadLine();
                    SportItem foundItem = shop.FindItem(itemName);
                    if (foundItem != null)
                    {
                        shop.AddItemToOrder(foundItem); // Добавляем товар в заказ
                    }
                    else
                    {
                        Console.WriteLine("Товар не найден.");
                    }
                    break;

                case "5":
                    shop.ViewOrders();
                    break;

                case "6":
                    Console.Write("Введите название товара для поиска: ");
                    string searchName = Console.ReadLine();
                    SportItem searchedItem = shop.FindItem(searchName);
                    if (searchedItem != null)
                    {
                        searchedItem.PrintInfo(); // Использование метода расширения
                    }
                    else
                    {
                        Console.WriteLine("Товар не найден.");
                    }
                    break;

                case "7":
                    return;

                default:
                    Console.WriteLine("Неверная опция.");
                    break;
            }
        }
    }
}