using System.Collections.Generic;

public class MedicineOrder : Order
{
    private List<Medicine> medicines = new List<Medicine>(); // Список медикаментов в заказе

    public MedicineOrder(Customer customer) : base(customer) { }

    // Метод для добавления медикамента в заказ
    public void AddMedicine(Medicine medicine)
    {
        medicines.Add(medicine);
    }

    // Метод для удаления медикамента из заказа
    public void RemoveMedicine(Medicine medicine)
    {
        medicines.Remove(medicine);
    }

    // Переопределение свойства для расчета общей стоимости
    public override double TotalPrice
    {
        get
        {
            double total = 0;
            foreach (var medicine in medicines)
            {
                total += medicine.Price; // Суммируем цены всех медикаментов
            }
            return total;
        }
    }

    // Свойство для доступа к медикаментам
    public List<Medicine> GetMedicines()
    {
        return medicines; // Возвращаем список медикаментов
    }

    // Переопределение метода ToString для отображения информации о заказе
    public override string ToString()
    {
        string details = base.ToString(); // Получаем базовую строку
        details += $", Общая стоимость: {TotalPrice:C}";
        details += "\nМедикаменты в заказе:\n";
        foreach (var medicine in medicines)
        {
            details += $"- {medicine}\n"; // Добавляем информацию о каждом медикаменте
        }
        return details;
    }
}