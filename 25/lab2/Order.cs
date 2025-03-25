using System;
using System.Collections.Generic;

// 6. Абстрактный класс
public abstract class Order
{
    public Customer Customer { get; private set; }

    public Order(Customer customer)
    {
        Customer = customer;
        Products = new List<Product>();
    }

    // 7. Инкапсуляция
    protected List<Product> Products { get; private set; }

    public abstract double TotalPrice { get; }

    // 9. Наследование
    public void AddProduct(Product product)
    {
        if (product.Stock > 0)
        {
            Products.Add(product);
            product.Stock--; // Уменьшаем остаток
        }
        else
        {
            throw new InvalidOperationException($"Product {product.Name} is out of stock.");
        }
    }

    public override string ToString()
    {
        string details = $"Заказ для {Customer}:\n";
        foreach (var product in Products)
        {
            details += $"- {product}\n";
        }
        details += $"Общая стоимость: {TotalPrice:C}";
        return details;
    }
}