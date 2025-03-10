public class SportItem
{
    // 3. Использование свойств;
    public string Name { get; private set; }
    public double Price { get; private set; }

    // 2. Использование конструкторов классов с параметрами;
    public SportItem(string name, double price)
    {
        Name = name;
        Price = price;
    }

    // 4. Использование свойств с логикой в get и/или set блоках.
    public string DisplayInfo => $"{Name}, Цена: {Price:C}";
}