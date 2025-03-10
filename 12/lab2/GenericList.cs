// 12. Использование обобщений;
public class GenericList<T>
{
    private List<T> items = new List<T>();

    // 13. Использование обобщенных методов;
    public void Add(T item)
    {
        items.Add(item);
    }

    // 5. Использование индексаторов;
    public T this[int index] => items[index];
}