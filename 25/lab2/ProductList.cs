using System;
using System.Collections.Generic;

// 12. Обобщения
public class ProductList<T> where T : Product
{
    private List<T> products = new List<T>();

    // 13. Обобщенные методы
    public void Add(T product) => products.Add(product);

    // 5. Индексаторы
    public T this[int index] => products[index];

    public void DisplayAll()
    {
        Console.WriteLine("Список товаров:");
        foreach (var product in products)
        {
            Console.WriteLine(product);
        }
    }
}