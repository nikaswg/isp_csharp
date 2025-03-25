// Pharmacy.cs
using System.Collections.Generic;

public class Pharmacy
{
    private static int orderCount = 0; // Статический элемент
    private List<Order> orders = new List<Order>();
    private List<Medicine> medicines = new List<Medicine>(); // Для хранения медикаментов

    public void CreateOrder(Customer customer)
    {
        var order = new MedicineOrder(customer);
        orders.Add(order);
        orderCount++;
    }

    public void AddMedicine(Medicine medicine)
    {
        medicines.Add(medicine);
    }

    public void ViewMedicines()
    {
        if (medicines.Count == 0)
        {
            Console.WriteLine("No medicines available.");
            return;
        }

        Console.WriteLine("Available Medicines:");
        foreach (var medicine in medicines)
        {
            Console.WriteLine(medicine);
        }
    }

    public void ViewOrders()
    {
        if (orders.Count == 0)
        {
            Console.WriteLine("Нет доступных заказов.");
            return;
        }

        Console.WriteLine("Заказы:");
        foreach (var order in orders)
        {
            Console.WriteLine($"Клиент: {order.Customer.Name}, Общая стоимость: {order.TotalPrice:C}");

            if (order is MedicineOrder medicineOrder)
            {
                Console.WriteLine("Медикаменты в заказе:");
                foreach (var medicine in medicineOrder.GetMedicines())
                {
                    Console.WriteLine($"- {medicine.Name}, Цена: {medicine.Price:C}");
                }
            }
        }
    }

    public Medicine FindMedicine(string name)
    {
        return medicines.Find(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public void InitializeData()
    {
        // Добавление медикаментов
        AddMedicine(new Medicine("Аспирин", 10.99));
        AddMedicine(new Medicine("Ибупрофен", 12.49));

        // Создание клиентов и заказов
        Customer customer1 = new Customer("Иван Иванов", "123-456-7890");
        Customer customer2 = new Customer("Мария Петрова", "987-654-3210");

        CreateOrder(customer1);
        CreateOrder(customer2);

        // Добавление медикаментов в заказы
        MedicineOrder order1 = new MedicineOrder(customer1);
        order1.AddMedicine(FindMedicine("Аспирин"));
        order1.AddMedicine(FindMedicine("Ибупрофен"));

        MedicineOrder order2 = new MedicineOrder(customer2);
        order2.AddMedicine(FindMedicine("Ибупрофен"));

        // Добавление заказов в список
        orders.Add(order1);
        orders.Add(order2);
    }

    public double GetTotalOrdersPrice()
    {
        double total = 0;
        foreach (var order in orders)
        {
            total += order.TotalPrice;
        }
        return total;
    }

    public void ViewMedicinesInOrder(int orderIndex)
    {
        if (orderIndex < 0 || orderIndex >= orders.Count)
        {
            Console.WriteLine("Неверный индекс заказа.");
            return;
        }

        var order = orders[orderIndex];
        Console.WriteLine(order.ToString());
    }

    public void RemoveMedicineFromOrder(int orderIndex, Medicine medicine)
    {
        if (orderIndex < 0 || orderIndex >= orders.Count)
        {
            Console.WriteLine("Неверный индекс заказа.");
            return;
        }

        var order = (MedicineOrder)orders[orderIndex]; // Приводим к типу MedicineOrder
        if (order != null)
        {
            // Удаление медикамента
            order.RemoveMedicine(medicine);
            Console.WriteLine($"{medicine.Name} удален из заказа.");
        }
    }

    public static int OrderCount => orderCount;
}