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
                Console.WriteLine("1. Удалить слова с длиной максимального слова");
                Console.WriteLine("2. Заменить первые строчные буквы в словах на прописные (работа с файлом)");
                Console.WriteLine("3. Обработка JSON файла с цветами");
                Console.WriteLine("0. Выход");
                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        StringProcessor.ProcessText();
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

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}