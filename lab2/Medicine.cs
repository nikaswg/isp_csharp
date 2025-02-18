// 1. Использование минимум 4 собственных классов;

public class Medicine
{
    // 2. Использование конструкторов классов с параметрами;
    public string Name { get; private set; }
    public double Price { get; private set; }

    public Medicine(string name, double price)
    {
        Name = name;
        Price = price;
    }

    // 3. Использование свойств;
    public override string ToString()
    {
        return $"{Name}, Price: {Price:C}";
    }
}