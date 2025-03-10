// 6. Использование абстрактных классов или членов класса;
public abstract class Order
{
    // 7. Использование принципов инкапсуляции.
    public Customer Customer { get; protected set; } // protected для инкапсуляции
    public abstract double TotalPrice { get; } // 10. Использование переопределений методов/свойств;

    protected Order(Customer customer)
    {
        Customer = customer;
    }
}