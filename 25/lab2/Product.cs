using System;

public class Product
{
    // 2. Использование конструктора с параметрами
    public Product(string name, double price, int stock)
    {
        Name = name;
        Price = price;
        Stock = stock;
    }

    // 3. Использование свойств
    public string Name { get; private set; }
    public double Price { get; private set; }

    // 4. Свойство с логикой в set
    private int stock;
    public int Stock
    {
        get => stock;
        set
        {
            if (value < 0)
                throw new ArgumentException("Stock cannot be negative.");
            stock = value;
        }
    }

    public override string ToString() => $"{Name}, Цена: {Price:C}, Остаток: {Stock}";
}