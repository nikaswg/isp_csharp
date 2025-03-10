public class SportItemOrder : Order
{
    private List<SportItem> items = new List<SportItem>(); // 16. Использование агрегации классов;

    public SportItemOrder(Customer customer) : base(customer) { }

    // 5. Использование индексаторов;
    public SportItem this[int index] => items[index];

    // 13. Использование обобщенных методов;
    public void AddItem(SportItem item)
    {
        items.Add(item);
    }

    // 10. Использование переопределений методов/свойств;
    public override double TotalPrice
    {
        get
        {
            double total = 0;
            foreach (var item in items)
            {
                total += item.Price;
            }
            return total;
        }
    }

    // 17. Использование композиции классов.
    public List<SportItem> GetItems() => items;

    public override string ToString()
    {
        string details = $"Клиент: {Customer.Name}, Общая стоимость: {TotalPrice:C}\n";
        details += "Товары в заказе:\n";
        foreach (var item in items)
        {
            details += $"- {item.DisplayInfo}\n";
        }
        return details;
    }
}