// Program.cs
using System;

class Program
{
    static void Main()
    {
        Pharmacy pharmacy = new Pharmacy();
        pharmacy.InitializeData(); // Инициализация данных

        while (true)
        {
            Console.WriteLine("\nСистема управления аптекой");
            Console.WriteLine("1. Добавить медикамент");
            Console.WriteLine("2. Просмотреть медикаменты");
            Console.WriteLine("3. Создать заказ");
            Console.WriteLine("4. Просмотреть заказы");
            Console.WriteLine("5. Поиск медикамента");
            Console.WriteLine("6. Общая стоимость всех заказов");
            Console.WriteLine("7. Просмотреть медикаменты в заказе");
            Console.WriteLine("8. Удалить медикамент из заказа");
            Console.WriteLine("9. Выход");
            Console.Write("Выберите опцию: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // Добавление медикамента
                    Console.Write("Введите название медикамента: ");
                    string name = Console.ReadLine();
                    Console.Write("Введите цену медикамента: ");
                    double price = Convert.ToDouble(Console.ReadLine());
                    Medicine medicine = new Medicine(name, price);
                    pharmacy.AddMedicine(medicine);
                    Console.WriteLine("Медикамент успешно добавлен!");
                    break;

                case "2":
                    // Просмотр медикаментов
                    pharmacy.ViewMedicines();
                    break;

                case "3":
                    // Создание заказа
                    Console.Write("Введите имя клиента: ");
                    string customerName = Console.ReadLine();
                    Console.Write("Введите телефон клиента: ");
                    string customerPhone = Console.ReadLine();
                    Customer customer = new Customer(customerName, customerPhone);
                    pharmacy.CreateOrder(customer);

                    // Добавление медикаментов в заказ
                    MedicineOrder order = new MedicineOrder(customer);
                    while (true)
                    {
                        Console.Write("Введите название медикамента для добавления в заказ (или 'готово' для завершения): ");
                        string medicineName = Console.ReadLine();
                        if (medicineName.ToLower() == "готово") break;

                        Medicine foundMedicine = pharmacy.FindMedicine(medicineName);
                        if (foundMedicine != null)
                        {
                            order.AddMedicine(foundMedicine);
                            Console.WriteLine($"{foundMedicine.Name} добавлен в заказ.");
                        }
                        else
                        {
                            Console.WriteLine("Медикамент не найден.");
                        }
                    }

                    Console.WriteLine($"Общая стоимость заказа: {order.TotalPrice:C}");
                    break;

                case "4":
                    // Просмотр заказов
                    pharmacy.ViewOrders();
                    break;

                case "5":
                    // Поиск медикамента
                    Console.Write("Введите название медикамента для поиска: ");
                    string searchName = Console.ReadLine();
                    Medicine searchedMedicine = pharmacy.FindMedicine(searchName);
                    if (searchedMedicine != null)
                    {
                        Console.WriteLine($"Найденный медикамент: {searchedMedicine}");
                    }
                    else
                    {
                        Console.WriteLine("Медикамент не найден.");
                    }
                    break;

                case "6":
                    // Общая стоимость всех заказов
                    Console.WriteLine($"Общая стоимость всех заказов: {pharmacy.GetTotalOrdersPrice():C}");
                    break;

                case "7":
                    // Просмотр медикаментов в заказе
                    Console.Write("Введите индекс заказа для просмотра медикаментов: ");
                    int orderIndex = Convert.ToInt32(Console.ReadLine());
                    pharmacy.ViewMedicinesInOrder(orderIndex);
                    break;

                case "8":
                    // Удаление медикамента из заказа
                    Console.Write("Введите индекс заказа для удаления медикамента: ");
                    int removeOrderIndex = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Введите название медикамента для удаления: ");
                    string removeMedicineName = Console.ReadLine();
                    Medicine medicineToRemove = pharmacy.FindMedicine(removeMedicineName);
                    pharmacy.RemoveMedicineFromOrder(removeOrderIndex, medicineToRemove);
                    break;

                case "9":
                    // Выход
                    return;

                default:
                    Console.WriteLine("Неверная опция. Пожалуйста, попробуйте снова.");
                    break;
            }
        }
    }
}