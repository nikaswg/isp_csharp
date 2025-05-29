using Lunopark.Services;
using System;
using System.Linq;
using Lunopark.Data.Repositories;
using Lunopark.Data;
using Lunopark.Core.Entities;

namespace Lunopark.ConsoleApp
{
    class Program
    {
        private static AppService _appService;

        static void Main(string[] args)
        {
            _appService = new AppService();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== УПРАВЛЕНИЕ ЛУНОПАРКОМ ===");
                Console.WriteLine("1. Управление сотрудниками");
                Console.WriteLine("2. Управление аттракционами");
                Console.WriteLine("3. Управление билетами");
                Console.WriteLine("4. Отчеты");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите пункт меню: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ManageEmployees();
                        break;
                    case "2":
                        ManageAttractions();
                        break;
                    case "3":
                        ManageTickets();
                        break;
                    case "4":
                        GenerateReports();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        #region Управление сотрудниками
        private static void ManageEmployees()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== УПРАВЛЕНИЕ СОТРУДНИКАМИ ===");
                Console.WriteLine("1. Просмотр всех сотрудников");
                Console.WriteLine("2. Добавить сотрудника");
                Console.WriteLine("3. Редактировать сотрудника");
                Console.WriteLine("4. Удалить сотрудника");
                Console.WriteLine("5. Поиск сотрудников");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите пункт меню: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllEmployees();
                        break;
                    case "2":
                        AddEmployee();
                        break;
                    case "3":
                        EditEmployee();
                        break;
                    case "4":
                        DeleteEmployee();
                        break;
                    case "5":
                        SearchEmployees();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void ShowAllEmployees()
        {
            Console.Clear();
            Console.WriteLine("=== СПИСОК СОТРУДНИКОВ ===");

            var employees = _appService.Employees.GetAllEmployees();

            if (!employees.Any())
            {
                Console.WriteLine("Сотрудники не найдены.");
            }
            else
            {
                Console.WriteLine("{0,-5} {1,-30} {2,-20} {3,-15} {4,-10}",
                    "ID", "ФИО", "Должность", "Телефон", "Год рожд.");

                foreach (var emp in employees)
                {
                    Console.WriteLine("{0,-5} {1,-30} {2,-20} {3,-15} {4,-10}",
                        emp.EmployeeId,
                        emp.FullName,
                        emp.Position,
                        emp.Phone ?? "-",
                        emp.BirthYear?.ToString() ?? "-");
                }
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static void AddEmployee()
        {
            Console.Clear();
            Console.WriteLine("=== ДОБАВЛЕНИЕ СОТРУДНИКА ===");

            var employee = new Employee();

            Console.Write("ФИО: ");
            employee.FullName = Console.ReadLine();

            Console.Write("Должность: ");
            employee.Position = Console.ReadLine();

            Console.Write("Адрес (необязательно): ");
            employee.Address = Console.ReadLine();

            Console.Write("Телефон (необязательно): ");
            employee.Phone = Console.ReadLine();

            Console.Write("Год рождения (необязательно): ");
            if (int.TryParse(Console.ReadLine(), out var birthYear))
                employee.BirthYear = birthYear;

            try
            {
                var id = _appService.Employees.AddEmployee(employee);
                Console.WriteLine($"\nСотрудник успешно добавлен с ID: {id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
        #endregion

        #region Управление аттракционами
        private static void ManageAttractions()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== УПРАВЛЕНИЕ АТТРАКЦИОНАМИ ===");
                Console.WriteLine("1. Просмотр всех аттракционов");
                Console.WriteLine("2. Добавить аттракцион");
                Console.WriteLine("3. Редактировать аттракцион");
                Console.WriteLine("4. Удалить аттракцион");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите пункт меню: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllAttractions();
                        break;
                    case "2":
                        AddAttraction();
                        break;
                    case "3":
                        EditAttraction();
                        break;
                    case "4":
                        DeleteAttraction();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void ShowAllAttractions()
        {
            Console.Clear();
            Console.WriteLine("=== СПИСОК АТТРАКЦИОНОВ ===");

            var attractions = _appService.Attractions.GetAllAttractions();
            var employees = _appService.GetEmployeesDictionary();

            if (!attractions.Any())
            {
                Console.WriteLine("Аттракционы не найдены.");
            }
            else
            {
                Console.WriteLine("{0,-5} {1,-30} {2,-15} {3,-30}",
                    "ID", "Название", "Год установки", "Ответственный");

                foreach (var attr in attractions)
                {
                    var responsible = attr.ResponsibleEmployeeId.HasValue &&
                                    employees.ContainsKey(attr.ResponsibleEmployeeId.Value)
                        ? employees[attr.ResponsibleEmployeeId.Value]
                        : "-";

                    Console.WriteLine("{0,-5} {1,-30} {2,-15} {3,-30}",
                        attr.AttractionId,
                        attr.AttractionName,
                        attr.InstallationYear,
                        responsible);
                }
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
        #endregion

        #region Управление билетами
        private static void ManageTickets()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== УПРАВЛЕНИЕ БИЛЕТАМИ ===");
                Console.WriteLine("1. Просмотр всех билетов");
                Console.WriteLine("2. Добавить билет");
                Console.WriteLine("3. Поиск билетов по дате");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите пункт меню: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllTickets();
                        break;
                    case "2":
                        AddTicket();
                        break;
                    case "3":
                        SearchTicketsByDate();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void ShowAllTickets()
        {
            Console.Clear();
            Console.WriteLine("=== СПИСОК БИЛЕТОВ ===");

            var tickets = _appService.Tickets.GetAllTickets();

            if (!tickets.Any())
            {
                Console.WriteLine("Билеты не найдены.");
            }
            else
            {
                Console.WriteLine("{0,-10} {1,-15} {2,-30} {3,-10} {4,-30} {5,-15}",
                    "Номер", "Цена", "Аттракцион", "ID", "Продавец", "Дата продажи");

                foreach (var ticket in tickets)
                {
                    Console.WriteLine("{0,-10} {1,-15} {2,-30} {3,-10} {4,-30} {5,-15}",
                        ticket.TicketNumber,
                        ticket.TicketPrice.ToString("C"),
                        ticket.AttractionName,
                        ticket.EmployeeId?.ToString() ?? "-",
                        ticket.EmployeeName,
                        ticket.SaleDate.ToShortDateString());
                }
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
        #endregion

        #region Отчеты
        private static void GenerateReports()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ГЕНЕРАЦИЯ ОТЧЕТОВ ===");
                Console.WriteLine("1. Отчет по сотрудникам");
                Console.WriteLine("2. Отчет по аттракционам за год");
                Console.WriteLine("3. Отчет по доходам");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите пункт меню: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        GenerateEmployeeReport();
                        break;
                    case "2":
                        GenerateAttractionsReport();
                        break;
                    case "3":
                        GenerateRevenueReport();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void GenerateEmployeeReport()
        {
            Console.Clear();
            Console.WriteLine("=== ОТЧЕТ ПО СОТРУДНИКАМ ===");
            Console.Write("Введите имя файла для сохранения (например: Employees.docx): ");
            var filename = Console.ReadLine();

            try
            {
                _appService.Reports.GenerateEmployeesReport(filename);
                Console.WriteLine($"Отчет успешно сохранен в файл: {filename}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при генерации отчета: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
        #endregion

        #region Реализация отсутствующих методов

        private static void EditEmployee()
        {
            Console.Clear();
            Console.WriteLine("=== РЕДАКТИРОВАНИЕ СОТРУДНИКА ===");

            Console.Write("Введите ID сотрудника для редактирования: ");
            if (!int.TryParse(Console.ReadLine(), out int employeeId))
            {
                Console.WriteLine("Неверный формат ID!");
                Console.ReadKey();
                return;
            }

            try
            {
                var employee = _appService.Employees.GetEmployeeById(employeeId);
                if (employee == null)
                {
                    Console.WriteLine("Сотрудник не найден!");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("\nТекущие данные:");
                Console.WriteLine($"ФИО: {employee.FullName}");
                Console.WriteLine($"Должность: {employee.Position}");
                Console.WriteLine($"Адрес: {employee.Address ?? "-"}");
                Console.WriteLine($"Телефон: {employee.Phone ?? "-"}");
                Console.WriteLine($"Год рождения: {employee.BirthYear?.ToString() ?? "-"}");

                Console.WriteLine("\nВведите новые данные (оставьте пустым, чтобы не изменять):");

                Console.Write("ФИО: ");
                var fullName = Console.ReadLine();
                if (!string.IsNullOrEmpty(fullName))
                    employee.FullName = fullName;

                Console.Write("Должность: ");
                var position = Console.ReadLine();
                if (!string.IsNullOrEmpty(position))
                    employee.Position = position;

                Console.Write("Адрес: ");
                var address = Console.ReadLine();
                employee.Address = string.IsNullOrEmpty(address) ? employee.Address : address;

                Console.Write("Телефон: ");
                var phone = Console.ReadLine();
                employee.Phone = string.IsNullOrEmpty(phone) ? employee.Phone : phone;

                Console.Write("Год рождения: ");
                var birthYearInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(birthYearInput) && int.TryParse(birthYearInput, out int birthYear))
                    employee.BirthYear = birthYear;

                _appService.Employees.UpdateEmployee(employee);
                Console.WriteLine("\nДанные сотрудника успешно обновлены!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка при редактировании: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static void DeleteEmployee()
        {
            Console.Clear();
            Console.WriteLine("=== УДАЛЕНИЕ СОТРУДНИКА ===");

            Console.Write("Введите ID сотрудника для удаления: ");
            if (!int.TryParse(Console.ReadLine(), out int employeeId))
            {
                Console.WriteLine("Неверный формат ID!");
                Console.ReadKey();
                return;
            }

            try
            {
                Console.Write("Вы уверены, что хотите удалить этого сотрудника? (y/n): ");
                var confirm = Console.ReadLine();
                if (confirm?.ToLower() != "y")
                {
                    Console.WriteLine("Удаление отменено.");
                    Console.ReadKey();
                    return;
                }

                _appService.Employees.DeleteEmployee(employeeId);
                Console.WriteLine("\nСотрудник успешно удален!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка при удалении: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static void SearchEmployees()
        {
            Console.Clear();
            Console.WriteLine("=== ПОИСК СОТРУДНИКОВ ===");
            Console.WriteLine("Введите критерии поиска (оставьте пустым, чтобы пропустить):");

            Console.Write("ФИО (частично): ");
            var namePart = Console.ReadLine();

            Console.Write("Должность (частично): ");
            var positionPart = Console.ReadLine();

            Console.Write("Год рождения (точно): ");
            int? birthYear = null;
            if (int.TryParse(Console.ReadLine(), out int year))
                birthYear = year;

            try
            {
                var employees = _appService.Employees.SearchEmployees(namePart, positionPart, birthYear);

                Console.WriteLine("\n=== РЕЗУЛЬТАТЫ ПОИСКА ===");
                if (!employees.Any())
                {
                    Console.WriteLine("Сотрудники не найдены.");
                }
                else
                {
                    Console.WriteLine("{0,-5} {1,-30} {2,-20} {3,-15} {4,-10}",
                        "ID", "ФИО", "Должность", "Телефон", "Год рожд.");

                    foreach (var emp in employees)
                    {
                        Console.WriteLine("{0,-5} {1,-30} {2,-20} {3,-15} {4,-10}",
                            emp.EmployeeId,
                            emp.FullName,
                            emp.Position,
                            emp.Phone ?? "-",
                            emp.BirthYear?.ToString() ?? "-");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка при поиске: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static void AddAttraction()
        {
            Console.Clear();
            Console.WriteLine("=== ДОБАВЛЕНИЕ АТТРАКЦИОНА ===");

            var attraction = new Attraction();

            Console.Write("Название аттракциона: ");
            attraction.AttractionName = Console.ReadLine();

            Console.Write("Год установки: ");
            if (int.TryParse(Console.ReadLine(), out int year))
                attraction.InstallationYear = year;

            Console.Write("ID ответственного сотрудника (необязательно): ");
            if (int.TryParse(Console.ReadLine(), out int responsibleId))
                attraction.ResponsibleEmployeeId = responsibleId;

            try
            {
                var id = _appService.Attractions.AddAttraction(attraction);
                Console.WriteLine($"\nАттракцион успешно добавлен с ID: {id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static void EditAttraction()
        {
            Console.Clear();
            Console.WriteLine("=== РЕДАКТИРОВАНИЕ АТТРАКЦИОНА ===");

            Console.Write("Введите ID аттракциона для редактирования: ");
            if (!int.TryParse(Console.ReadLine(), out int attractionId))
            {
                Console.WriteLine("Неверный формат ID!");
                Console.ReadKey();
                return;
            }

            try
            {
                var attraction = _appService.Attractions.GetAttractionById(attractionId);
                if (attraction == null)
                {
                    Console.WriteLine("Аттракцион не найден!");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("\nТекущие данные:");
                Console.WriteLine($"Название: {attraction.AttractionName}");
                Console.WriteLine($"Год установки: {attraction.InstallationYear}");
                Console.WriteLine($"Ответственный ID: {attraction.ResponsibleEmployeeId?.ToString() ?? "-"}");

                Console.WriteLine("\nВведите новые данные (оставьте пустым, чтобы не изменять):");

                Console.Write("Название: ");
                var name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                    attraction.AttractionName = name;

                Console.Write("Год установки: ");
                var yearInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(yearInput) && int.TryParse(yearInput, out int year))
                    attraction.InstallationYear = year;

                Console.Write("ID ответственного сотрудника (оставьте пустым, чтобы не изменять): ");
                var respIdInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(respIdInput))
                {
                    if (int.TryParse(respIdInput, out int respId))
                        attraction.ResponsibleEmployeeId = respId;
                    else if (respIdInput.ToLower() == "null")
                        attraction.ResponsibleEmployeeId = null;
                }

                _appService.Attractions.UpdateAttraction(attraction);
                Console.WriteLine("\nДанные аттракциона успешно обновлены!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка при редактировании: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static void DeleteAttraction()
        {
            Console.Clear();
            Console.WriteLine("=== УДАЛЕНИЕ АТТРАКЦИОНА ===");

            Console.Write("Введите ID аттракциона для удаления: ");
            if (!int.TryParse(Console.ReadLine(), out int attractionId))
            {
                Console.WriteLine("Неверный формат ID!");
                Console.ReadKey();
                return;
            }

            try
            {
                Console.Write("Вы уверены, что хотите удалить этот аттракцион? (y/n): ");
                var confirm = Console.ReadLine();
                if (confirm?.ToLower() != "y")
                {
                    Console.WriteLine("Удаление отменено.");
                    Console.ReadKey();
                    return;
                }

                _appService.Attractions.DeleteAttraction(attractionId);
                Console.WriteLine("\nАттракцион успешно удален!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка при удалении: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static void AddTicket()
        {
            Console.Clear();
            Console.WriteLine("=== ПРОДАЖА БИЛЕТА ===");

            var ticket = new Ticket();

            Console.Write("Номер билета: ");
            ticket.TicketNumber = Console.ReadLine();

            Console.Write("Цена билета: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal price))
                ticket.TicketPrice = price;

            Console.Write("ID аттракциона: ");
            if (int.TryParse(Console.ReadLine(), out int attractionId))
                ticket.AttractionId = attractionId;

            Console.Write("ID продавца (необязательно): ");
            if (int.TryParse(Console.ReadLine(), out int employeeId))
                ticket.EmployeeId = employeeId;

            ticket.SaleDate = DateTime.Now;

            try
            {
                var id = _appService.Tickets.AddTicket(ticket);
                Console.WriteLine($"\nБилет успешно продан с ID: {id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static void SearchTicketsByDate()
        {
            Console.Clear();
            Console.WriteLine("=== ПОИСК БИЛЕТОВ ПО ДАТЕ ===");

            Console.Write("Введите начальную дату (дд.мм.гггг): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
            {
                Console.WriteLine("Неверный формат даты!");
                Console.ReadKey();
                return;
            }

            Console.Write("Введите конечную дату (дд.мм.гггг): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
            {
                Console.WriteLine("Неверный формат даты!");
                Console.ReadKey();
                return;
            }

            try
            {
                var tickets = _appService.Tickets.GetTicketsByDateRange(startDate, endDate.AddDays(1));

                Console.WriteLine("\n=== РЕЗУЛЬТАТЫ ПОИСКА ===");
                if (!tickets.Any())
                {
                    Console.WriteLine("Билеты не найдены.");
                }
                else
                {
                    Console.WriteLine("{0,-10} {1,-15} {2,-30} {3,-10} {4,-30} {5,-15}",
                        "Номер", "Цена", "Аттракцион", "ID", "Продавец", "Дата продажи");

                    foreach (var ticket in tickets)
                    {
                        Console.WriteLine("{0,-10} {1,-15} {2,-30} {3,-10} {4,-30} {5,-15}",
                            ticket.TicketNumber,
                            ticket.TicketPrice.ToString("C"),
                            ticket.AttractionName,
                            ticket.EmployeeId?.ToString() ?? "-",
                            ticket.EmployeeName,
                            ticket.SaleDate.ToShortDateString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка при поиске: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static void GenerateAttractionsReport()
        {
            Console.Clear();
            Console.WriteLine("=== ОТЧЕТ ПО АТТРАКЦИОНАМ ===");
            Console.Write("Введите год для отчета: ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Неверный формат года!");
                Console.ReadKey();
                return;
            }

            Console.Write("Введите имя файла для сохранения (например: Attractions.docx): ");
            var filename = Console.ReadLine();

            try
            {
                _appService.Reports.GenerateAttractionsReport(year, filename);
                Console.WriteLine($"Отчет успешно сохранен в файл: {filename}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при генерации отчета: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static void GenerateRevenueReport()
        {
            Console.Clear();
            Console.WriteLine("=== ОТЧЕТ ПО ДОХОДАМ ===");
            Console.Write("Введите начальную дату (дд.мм.гггг): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
            {
                Console.WriteLine("Неверный формат даты!");
                Console.ReadKey();
                return;
            }

            Console.Write("Введите конечную дату (дд.мм.гггг): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
            {
                Console.WriteLine("Неверный формат даты!");
                Console.ReadKey();
                return;
            }

            Console.Write("Введите имя файла для сохранения (например: Revenue.docx): ");
            var filename = Console.ReadLine();

            try
            {
                _appService.Reports.GenerateRevenueReport(startDate, endDate, filename);
                Console.WriteLine($"Отчет успешно сохранен в файл: {filename}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при генерации отчета: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        #endregion
    }
}