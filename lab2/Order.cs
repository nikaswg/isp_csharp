// 6. Использование абстрактных классов или членов класса;
public abstract class Order
{
    // 7. Использование принципов инкапсуляции.
    public Customer Customer { get; set; }
    public abstract double TotalPrice { get; }

    protected Order(Customer customer) // protected для инкапсуляции
    {
        Customer = customer;
    }
}