using System;

public class Customer
{
    // 2. Использование конструктора с параметрами
    public Customer(string name, string email)
    {
        Name = name;
        Email = email;
    }

    // 3. Использование свойств
    public string Name { get; private set; }
    public string Email { get; private set; }

    public override string ToString() => $"{Name} ({Email})";
}