using System;

// 9. Наследование
public class InternetOrder : Order
{
    public InternetOrder(Customer customer, double deliveryFee) : base(customer)
    {
        DeliveryFee = deliveryFee;
    }

    public double DeliveryFee { get; private set; }

    // 10. Переопределение свойства
    public override double TotalPrice
    {
        get
        {
            double total = 0;
            foreach (var product in Products)
            {
                total += product.Price;
            }
            return total + DeliveryFee;
        }
    }
}