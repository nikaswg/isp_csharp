using RailwayTransport.Core.Entities;
using RailwayTransport.Data;
using RailwayTransport.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RailwayTransport.ConsoleApp
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
                Console.WriteLine("=== СИСТЕМА УПРАВЛЕНИЯ ЖЕЛЕЗНОДОРОЖНЫМИ ПЕРЕВОЗКАМИ ===");
                Console.WriteLine("1. Управление поездами");
                Console.WriteLine("2. Управление вагонами");
                Console.WriteLine("3. Управление билетами");
                Console.WriteLine("4. Отчеты");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите пункт меню: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ManageTrains();
                        break;
                    case "2":
                        ManageCarriages();
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

        #region Управление поездами
        private static void ManageTrains()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== УПРАВЛЕНИЕ ПОЕЗДАМИ ===");
                Console.WriteLine("1. Просмотр всех поездов");
                Console.WriteLine("2. Добавить поезд");
                Console.WriteLine("3. Редактировать поезд");
                Console.WriteLine("4. Удалить поезд");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите пункт меню: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllTrains();
                        break;
                    case "2":
                        AddTrain();
                        break;
                    case "3":
                        EditTrain();
                        break;
                    case "4":
                        DeleteTrain();
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

        private static void ShowAllTrains()
        {
            Console.Clear();
            Console.WriteLine("=== СПИСОК ПОЕЗДОВ ===");

            var trains = _appService.Trains.GetAllTrains();

            if (!trains.Any())
            {
                Console.WriteLine("Поезда не найдены.");
            }
            else
            {
                Console.WriteLine("{0,-5} {1,-15} {2,-15} {3,-20} {4,-20} {5,-15}",
                    "ID", "Номер", "Тип", "Отправление", "Назначение", "Дата отправления");

                foreach (var train in trains)
                {
                    Console.WriteLine("{0,-5} {1,-15} {2,-15} {3,-20} {4,-20} {5,-15}",
                        train.TrainId,
                        train.TrainNumber,
                        train.TrainType,
                        train.DeparturePoint,
                        train.DestinationPoint,
                        train.DepartureDate.ToString("dd.MM.yyyy"));
                }
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static void AddTrain()
        {
            Console.Clear();
            Console.WriteLine("=== ДОБАВЛЕНИЕ ПОЕЗДА ===");

            var train = new Train();

            Console.Write("Номер поезда: ");
            train.TrainNumber = Console.ReadLine();

            Console.Write("Тип поезда: ");
            train.TrainType = Console.ReadLine();

            Console.Write("Пункт отправления: ");
            train.DeparturePoint = Console.ReadLine();

            Console.Write("Пункт назначения: ");
            train.DestinationPoint = Console.ReadLine();

            Console.Write("Дата отправления (дд.мм.гггг): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime departureDate))
                train.DepartureDate = departureDate;

            try
            {
                var id = _appService.Trains.AddTrain(train);
                Console.WriteLine($"\nПоезд успешно добавлен с ID: {id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static void EditTrain()
        {
            Console.Clear();
            Console.WriteLine("=== РЕДАКТИРОВАНИЕ ПОЕЗДА ===");

            Console.Write("Введите ID поезда для редактирования: ");
            if (!int.TryParse(Console.ReadLine(), out int trainId))
            {
                Console.WriteLine("Неверный формат ID!");
                Console.ReadKey();
                return;
            }

            try
            {
                var train = _appService.Trains.GetTrainById(trainId);
                if (train == null)
                {
                    Console.WriteLine("Поезд не найден!");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("\nТекущие данные:");
                Console.WriteLine($"Номер: {train.TrainNumber}");
                Console.WriteLine($"Тип: {train.TrainType}");
                Console.WriteLine($"Отправление: {train.DeparturePoint}");
                Console.WriteLine($"Назначение: {train.DestinationPoint}");
                Console.WriteLine($"Дата отправления: {train.DepartureDate:dd.MM.yyyy}");

                Console.WriteLine("\nВведите новые данные (оставьте пустым, чтобы не изменять):");

                Console.Write("Номер: ");
                var number = Console.ReadLine();
                if (!string.IsNullOrEmpty(number))
                    train.TrainNumber = number;

                Console.Write("Тип: ");
                var type = Console.ReadLine();
                if (!string.IsNullOrEmpty(type))
                    train.TrainType = type;

                Console.Write("Пункт отправления: ");
                var departure = Console.ReadLine();
                if (!string.IsNullOrEmpty(departure))
                    train.DeparturePoint = departure;

                Console.Write("Пункт назначения: ");
                var destination = Console.ReadLine();
                if (!string.IsNullOrEmpty(destination))
                    train.DestinationPoint = destination;

                Console.Write("Дата отправления (дд.мм.гггг): ");
                var dateInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(dateInput) && DateTime.TryParse(dateInput, out DateTime newDate))
                    train.DepartureDate = newDate;

                _appService.Trains.UpdateTrain(train);
                Console.WriteLine("\nДанные поезда успешно обновлены!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка при редактировании: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static void DeleteTrain()
        {
            Console.Clear();
            Console.WriteLine("=== УДАЛЕНИЕ ПОЕЗДА ===");

            Console.Write("Введите ID поезда для удаления: ");
            if (!int.TryParse(Console.ReadLine(), out int trainId))
            {
                Console.WriteLine("Неверный формат ID!");
                Console.ReadKey();
                return;
            }

            try
            {
                Console.Write("Вы уверены, что хотите удалить этот поезд? (y/n): ");
                var confirm = Console.ReadLine();
                if (confirm?.ToLower() != "y")
                {
                    Console.WriteLine("Удаление отменено.");
                    Console.ReadKey();
                    return;
                }

                _appService.Trains.DeleteTrain(trainId);
                Console.WriteLine("\nПоезд успешно удален!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка при удалении: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
        #endregion

        #region Управление вагонами
        private static void ManageCarriages()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== УПРАВЛЕНИЕ ВАГОНАМИ ===");
                Console.WriteLine("1. Просмотр всех вагонов");
                Console.WriteLine("2. Добавить вагон");
                Console.WriteLine("3. Редактировать вагон");
                Console.WriteLine("4. Удалить вагон");
                Console.WriteLine("5. Просмотр вагонов поезда");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите пункт меню: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllCarriages();
                        break;
                    case "2":
                        AddCarriage();
                        break;
                    case "3":
                        EditCarriage();
                        break;
                    case "4":
                        DeleteCarriage();
                        break;
                    case "5":
                        ShowCarriagesByTrain();
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

        private static void ShowAllCarriages()
        {
            Console.Clear();
            Console.WriteLine("=== СПИСОК ВАГОНОВ ===");

            var carriages = _appService.Carriages.GetAllCarriages();
            var trains = _appService.GetTrainsDictionary();

            if (!carriages.Any())
            {
                Console.WriteLine("Вагоны не найдены.");
            }
            else
            {
                Console.WriteLine("{0,-5} {1,-15} {2,-15} {3,-10} {4,-10} {5,-30}",
                    "ID", "Номер", "Тип", "Места", "Цена", "Поезд");

                foreach (var carriage in carriages)
                {
                    var trainInfo = trains.ContainsKey(carriage.TrainId)
                        ? trains[carriage.TrainId]
                        : "Неизвестный поезд";

                    Console.WriteLine("{0,-5} {1,-15} {2,-15} {3,-10} {4,-10} {5,-30}",
                        carriage.CarriageId,
                        carriage.CarriageNumber,
                        carriage.CarriageType,
                        carriage.SeatCount,
                        carriage.SeatPrice.ToString("C"),
                        trainInfo);
                }
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static void AddCarriage()
        {
            Console.Clear();
            Console.WriteLine("=== ДОБАВЛЕНИЕ ВАГОНА ===");

            var carriage = new Carriage();

            Console.Write("Номер вагона: ");
            carriage.CarriageNumber = Console.ReadLine();

            Console.Write("Тип вагона: ");
            carriage.CarriageType = Console.ReadLine();

            Console.Write("Количество мест: ");
            if (int.TryParse(Console.ReadLine(), out int seatCount))
                carriage.SeatCount = seatCount;

            Console.Write("Цена места: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal seatPrice))
                carriage.SeatPrice = seatPrice;

            // Выбор поезда
            var trains = _appService.GetTrainsDictionary();
            Console.WriteLine("\nДоступные поезда:");
            foreach (var train in trains)
            {
                Console.WriteLine($"{train.Key} - {train.Value}");
            }

            Console.Write("\nВведите ID поезда: ");
            if (int.TryParse(Console.ReadLine(), out int trainId))
                carriage.TrainId = trainId;

            try
            {
                var id = _appService.Carriages.AddCarriage(carriage);
                Console.WriteLine($"\nВагон успешно добавлен с ID: {id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static void EditCarriage()
        {
            Console.Clear();
            Console.WriteLine("=== РЕДАКТИРОВАНИЕ ВАГОНА ===");

            Console.Write("Введите ID вагона для редактирования: ");
            if (!int.TryParse(Console.ReadLine(), out int carriageId))
            {
                Console.WriteLine("Неверный формат ID!");
                Console.ReadKey();
                return;
            }

            try
            {
                var carriage = _appService.Carriages.GetCarriageById(carriageId);
                if (carriage == null)
                {
                    Console.WriteLine("Вагон не найден!");
                    Console.ReadKey();
                    return;
                }

                var trains = _appService.GetTrainsDictionary();
                var trainInfo = trains.ContainsKey(carriage.TrainId)
                    ? trains[carriage.TrainId]
                    : "Неизвестный поезд";

                Console.WriteLine("\nТекущие данные:");
                Console.WriteLine($"Номер: {carriage.CarriageNumber}");
                Console.WriteLine($"Тип: {carriage.CarriageType}");
                Console.WriteLine($"Количество мест: {carriage.SeatCount}");
                Console.WriteLine($"Цена места: {carriage.SeatPrice:C}");
                Console.WriteLine($"Поезд: {trainInfo}");

                Console.WriteLine("\nВведите новые данные (оставьте пустым, чтобы не изменять):");

                Console.Write("Номер вагона: ");
                var number = Console.ReadLine();
                if (!string.IsNullOrEmpty(number))
                    carriage.CarriageNumber = number;

                Console.Write("Тип вагона: ");
                var type = Console.ReadLine();
                if (!string.IsNullOrEmpty(type))
                    carriage.CarriageType = type;

                Console.Write("Количество мест: ");
                var seatsInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(seatsInput) && int.TryParse(seatsInput, out int seats))
                    carriage.SeatCount = seats;

                Console.Write("Цена места: ");
                var priceInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(priceInput) && decimal.TryParse(priceInput, out decimal price))
                    carriage.SeatPrice = price;

                Console.WriteLine("\nДоступные поезда:");
                foreach (var train in trains)
                {
                    Console.WriteLine($"{train.Key} - {train.Value}");
                }

                Console.Write("\nID поезда: ");
                var trainInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(trainInput) && int.TryParse(trainInput, out int trainId))
                    carriage.TrainId = trainId;

                _appService.Carriages.UpdateCarriage(carriage);
                Console.WriteLine("\nДанные вагона успешно обновлены!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка при редактировании: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static void DeleteCarriage()
        {
            Console.Clear();
            Console.WriteLine("=== УДАЛЕНИЕ ВАГОНА ===");

            Console.Write("Введите ID вагона для удаления: ");
            if (!int.TryParse(Console.ReadLine(), out int carriageId))
            {
                Console.WriteLine("Неверный формат ID!");
                Console.ReadKey();
                return;
            }

            try
            {
                Console.Write("Вы уверены, что хотите удалить этот вагон? (y/n): ");
                var confirm = Console.ReadLine();
                if (confirm?.ToLower() != "y")
                {
                    Console.WriteLine("Удаление отменено.");
                    Console.ReadKey();
                    return;
                }

                _appService.Carriages.DeleteCarriage(carriageId);
                Console.WriteLine("\nВагон успешно удален!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка при удалении: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static void ShowCarriagesByTrain()
        {
            Console.Clear();
            Console.WriteLine("=== ВАГОНЫ ПОЕЗДА ===");

            var trains = _appService.GetTrainsDictionary();
            Console.WriteLine("Доступные поезда:");
            foreach (var train in trains)
            {
                Console.WriteLine($"{train.Key} - {train.Value}");
            }

            Console.Write("\nВведите ID поезда: ");
            if (!int.TryParse(Console.ReadLine(), out int trainId))
            {
                Console.WriteLine("Неверный формат ID!");
                Console.ReadKey();
                return;
            }

            var carriages = _appService.Carriages.GetCarriagesByTrain(trainId);
            var trainInfo = trains.ContainsKey(trainId) ? trains[trainId] : "Неизвестный поезд";

            Console.WriteLine($"\nВагоны поезда: {trainInfo}");

            if (!carriages.Any())
            {
                Console.WriteLine("Вагоны не найдены.");
            }
            else
            {
                Console.WriteLine("{0,-5} {1,-15} {2,-15} {3,-10} {4,-10}",
                    "ID", "Номер", "Тип", "Места", "Цена");

                foreach (var carriage in carriages)
                {
                    Console.WriteLine("{0,-5} {1,-15} {2,-15} {3,-10} {4,-10}",
                        carriage.CarriageId,
                        carriage.CarriageNumber,
                        carriage.CarriageType,
                        carriage.SeatCount,
                        carriage.SeatPrice.ToString("C"));
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
                Console.WriteLine("4. Удалить билет");
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
                    case "4":
                        DeleteTicket();
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

            var tickets = _appService.GetTicketsWithDetails();

            if (!tickets.Any())
            {
                Console.WriteLine("Билеты не найдены.");
            }
            else
            {
                Console.WriteLine("{0,-5} {1,-30} {2,-30} {3,-15} {4,-10} {5,-10} {6,-15}",
                    "ID", "Пассажир", "Поезд", "Вагон", "Место", "Цена", "Дата продажи");

                foreach (var ticket in tickets)
                {
                    Console.WriteLine("{0,-5} {1,-30} {2,-30} {3,-15} {4,-10} {5,-10} {6,-15}",
                        ticket.TicketId,
                        ticket.PassengerName,
                        ticket.TrainNumber,
                        ticket.CarriageNumber,
                        ticket.SeatNumber,
                        ticket.SeatPrice.ToString("C"),
                        ticket.SaleDate.ToString("dd.MM.yyyy"));
                }
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static void AddTicket()
        {
            Console.Clear();
            Console.WriteLine("=== ПРОДАЖА БИЛЕТА ===");

            var ticket = new Ticket();

            Console.Write("Имя пассажира: ");
            ticket.PassengerName = Console.ReadLine();

            // Выбор поезда
            var trains = _appService.GetTrainsDictionary();
            Console.WriteLine("\nДоступные поезда:");
            foreach (var train in trains)
            {
                Console.WriteLine($"{train.Key} - {train.Value}");
            }

            Console.Write("\nВведите ID поезда: ");
            if (int.TryParse(Console.ReadLine(), out int trainId))
                ticket.TrainId = trainId;

            // Выбор вагона
            var carriages = _appService.GetCarriagesDictionary(ticket.TrainId);
            Console.WriteLine("\nДоступные вагоны:");
            foreach (var carriage in carriages)
            {
                Console.WriteLine($"{carriage.Key} - {carriage.Value}");
            }

            Console.Write("\nВведите ID вагона: ");
            if (int.TryParse(Console.ReadLine(), out int carriageId))
                ticket.CarriageId = carriageId;

            Console.Write("Номер места: ");
            if (int.TryParse(Console.ReadLine(), out int seatNumber))
                ticket.SeatNumber = seatNumber;

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
                var tickets = _appService.Tickets.GetTicketsByDateRange(startDate, endDate);
                var ticketsWithDetails = _appService.GetTicketsWithDetails(tickets);

                Console.WriteLine($"\nБилеты за период с {startDate:dd.MM.yyyy} по {endDate:dd.MM.yyyy}");

                if (!ticketsWithDetails.Any())
                {
                    Console.WriteLine("Билеты не найдены.");
                }
                else
                {
                    Console.WriteLine("{0,-5} {1,-30} {2,-30} {3,-15} {4,-10} {5,-10} {6,-15}",
                        "ID", "Пассажир", "Поезд", "Вагон", "Место", "Цена", "Дата продажи");

                    foreach (var ticket in ticketsWithDetails)
                    {
                        Console.WriteLine("{0,-5} {1,-30} {2,-30} {3,-15} {4,-10} {5,-10} {6,-15}",
                            ticket.TicketId,
                            ticket.PassengerName,
                            ticket.TrainNumber,
                            ticket.CarriageNumber,
                            ticket.SeatNumber,
                            ticket.SeatPrice.ToString("C"),
                            ticket.SaleDate.ToString("dd.MM.yyyy"));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка при поиске билетов: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static void DeleteTicket()
        {
            Console.Clear();
            Console.WriteLine("=== УДАЛЕНИЕ БИЛЕТА ===");

            Console.Write("Введите ID билета для удаления: ");
            if (!int.TryParse(Console.ReadLine(), out int ticketId))
            {
                Console.WriteLine("Неверный формат ID!");
                Console.ReadKey();
                return;
            }

            try
            {
                Console.Write("Вы уверены, что хотите удалить этот билет? (y/n): ");
                var confirm = Console.ReadLine();
                if (confirm?.ToLower() != "y")
                {
                    Console.WriteLine("Удаление отменено.");
                    Console.ReadKey();
                    return;
                }

                _appService.Tickets.DeleteTicket(ticketId);
                Console.WriteLine("\nБилет успешно удален!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка при удалении: {ex.Message}");
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
                Console.WriteLine("1. Отчет по поездам");
                Console.WriteLine("2. Отчет по доходам");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите пункт меню: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        GenerateTrainsReport();
                        break;
                    case "2":
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

        private static void GenerateTrainsReport()
        {
            Console.Clear();
            Console.WriteLine("=== ОТЧЕТ ПО ПОЕЗДАМ ===");

            Console.Write("Введите путь для сохранения отчета (например: C:\\Reports\\TrainsReport.docx): ");
            var filePath = Console.ReadLine();

            try
            {
                _appService.Reports.GenerateTrainsReport(filePath);
                Console.WriteLine($"\nОтчет успешно сохранен по пути: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка при генерации отчета: {ex.Message}");
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

            Console.Write("\nВведите путь для сохранения отчета (например: C:\\Reports\\RevenueReport.docx): ");
            var filePath = Console.ReadLine();

            try
            {
                _appService.Reports.GenerateRevenueReport(startDate, endDate, filePath);
                Console.WriteLine($"\nОтчет успешно сохранен по пути: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка при генерации отчета: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
        #endregion
    }
}