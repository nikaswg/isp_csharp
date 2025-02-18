public class Customer
{
    // 3. Использование свойств;
    public string Name { get; set; }
    public string Phone { get; set; }

    // 2. Использование конструкторов классов с параметрами;
    public Customer(string name, string phone)
    {
        Name = name;
        Phone = phone;
    }
}