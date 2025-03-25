using System;
using System.Collections.Generic;

// 8. Статический класс
public static class Store
{
    // 8. Статический элемент
    public static int TotalOrders { get; private set; } = 0;

    public static void IncrementOrderCount() => TotalOrders++;
}