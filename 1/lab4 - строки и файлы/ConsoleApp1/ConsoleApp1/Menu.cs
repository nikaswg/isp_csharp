using System;

namespace ConsoleApp
{
    public class Menu
    {
        public void Show()
        {
            while (true)
            {
                Console.WriteLine("Выберите пункт меню:");
                Console.WriteLine("1. Дан текст. Найти в строках первое по порядку слово с наибольшим числом вхождений в него буквы 'и'. ");
                Console.WriteLine("2. Дан текстовый файл. Вывести в обратном порядке все строки, т.е. 1-я строка выводится последней 2-я предпоследней и т.д.");
                Console.WriteLine("3. Обработка JSON файла преподавателей");
                Console.WriteLine("0. Выход");
                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        TextProcessor.FindWordWithMostI();
                        break;
                    case "2":
                        FileProcessor.ReverseFileLines();
                        break;
                    case "3":
                        JsonProcessor.ProcessTeachersJson();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}