// Menu.cs
using System;
using ConsoleApp;

namespace RealEstateApp
{
    public class Menu
    {
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Система управления недвижимостью ===");
                Console.WriteLine("1. Дан текст. Первое по порядку самое короткое слово текста заменить на первое по порядку самое длинное слово,\n если их длины не совпадают. В противном случае текст оставить без изменения. ");
                Console.WriteLine("2. Дан текстовый файл. Найти все предложения, содержащие введенное с клавиатуры слово.");
                Console.WriteLine("3. Обработка JSON-файла для управления базой преподавателей.");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите пункт меню: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        TextProcessor.ProcessText();
                        break;
                    case "2":
                        FileProcessor.FindSentencesWithWord();
                        break;
                    case "3":
                        JsonProcessor.ProcessApartmentsJson();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }
    }
}