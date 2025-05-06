using System;

namespace ConsoleApp
{
    class Menu
    {
        public void Show()
        {
            while (true)
            {
                Console.WriteLine("Выберите пункт меню:");
                Console.WriteLine("1. Обработка строк");
                Console.WriteLine("2. Обработка текстового файла");
                Console.WriteLine("3. Работа с JSON файлом");
                Console.WriteLine("0. Выход");
                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        StringProcessor.ProcessStrings();
                        break;
                    case "2":
                        FileProcessor.ProcessTextFile();
                        break;
                    case "3":
                        JsonProcessor.ProcessJsonFile();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }
    }
}