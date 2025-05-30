using RailwayTransport.Core.Entities;
using RailwayTransport.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace RailwayTransport.Services
{
    public class ReportService
    {
        private readonly UnitOfWork _unitOfWork;

        public ReportService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void GenerateTrainsReport(string filePath)
        {
            var trains = _unitOfWork.Trains.GetAll().ToList();

            using (var doc = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
            {
                var mainPart = doc.AddMainDocumentPart();
                mainPart.Document = new Document();
                var body = mainPart.Document.AppendChild(new Body());

                // Заголовок
                var title = new Paragraph();
                var run = new Run();
                run.Append(new Text("Отчет по поездам"));
                run.RunProperties = new RunProperties(new Bold());
                title.Append(run);
                body.Append(title);

                // Таблица с данными
                var table = new Table();

                // Заголовки таблицы
                var headerRow = new TableRow();
                string[] headers = { "Номер", "Тип", "Отправление", "Назначение", "Дата отправления" };

                foreach (var header in headers)
                {
                    var cell = new TableCell();
                    cell.Append(new Paragraph(new Run(new Text(header))));
                    cell.TableCellProperties = new TableCellProperties(
                        new TableCellBorders(
                            new TopBorder() { Val = BorderValues.Single },
                            new BottomBorder() { Val = BorderValues.Single },
                            new LeftBorder() { Val = BorderValues.Single },
                            new RightBorder() { Val = BorderValues.Single }));
                    headerRow.Append(cell);
                }
                table.Append(headerRow);

                // Данные поездов
                foreach (var train in trains)
                {
                    var row = new TableRow();

                    string[] data = {
                        train.TrainNumber,
                        train.TrainType,
                        train.DeparturePoint,
                        train.DestinationPoint,
                        train.DepartureDate.ToString("dd.MM.yyyy")
                    };

                    foreach (var item in data)
                    {
                        var cell = new TableCell();
                        cell.Append(new Paragraph(new Run(new Text(item))));
                        cell.TableCellProperties = new TableCellProperties(
                            new TableCellBorders(
                                new TopBorder() { Val = BorderValues.Single },
                                new BottomBorder() { Val = BorderValues.Single },
                                new LeftBorder() { Val = BorderValues.Single },
                                new RightBorder() { Val = BorderValues.Single }));
                        row.Append(cell);
                    }

                    table.Append(row);
                }

                body.Append(table);
                doc.Save();
            }
        }

        public void GenerateRevenueReport(DateTime startDate, DateTime endDate, string filePath)
        {
            var tickets = _unitOfWork.Tickets.GetByDateRange(startDate, endDate).ToList();
            var trains = _unitOfWork.Trains.GetAll().ToList();

            var reportData = from t in tickets
                             join tr in trains on t.TrainId equals tr.TrainId
                             group t by new { tr.TrainId, tr.TrainNumber } into g
                             select new
                             {
                                 TrainId = g.Key.TrainId,
                                 TrainNumber = g.Key.TrainNumber,
                                 TicketCount = g.Count(),
                                 TotalRevenue = g.Sum(t =>
                                     _unitOfWork.Carriages.GetById(t.CarriageId)?.SeatPrice ?? 0)
                             };

            using (var doc = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
            {
                var mainPart = doc.AddMainDocumentPart();
                mainPart.Document = new Document();
                var body = mainPart.Document.AppendChild(new Body());

                // Заголовок
                var title = new Paragraph();
                var run = new Run();
                run.Append(new Text($"Отчет по доходам за период с {startDate:dd.MM.yyyy} по {endDate:dd.MM.yyyy}"));
                run.RunProperties = new RunProperties(new Bold());
                title.Append(run);
                body.Append(title);

                // Таблица с данными
                var table = new Table();

                // Заголовки таблицы
                var headerRow = new TableRow();
                string[] headers = { "Поезд", "Кол-во билетов", "Сумма дохода" };

                foreach (var header in headers)
                {
                    var cell = new TableCell();
                    cell.Append(new Paragraph(new Run(new Text(header))));
                    cell.TableCellProperties = new TableCellProperties(
                        new TableCellBorders(
                            new TopBorder() { Val = BorderValues.Single },
                            new BottomBorder() { Val = BorderValues.Single },
                            new LeftBorder() { Val = BorderValues.Single },
                            new RightBorder() { Val = BorderValues.Single }));
                    headerRow.Append(cell);
                }
                table.Append(headerRow);

                // Данные поездов
                decimal totalRevenue = 0;
                int totalTickets = 0;

                foreach (var item in reportData.OrderByDescending(r => r.TotalRevenue))
                {
                    var row = new TableRow();

                    string[] data = {
                        item.TrainNumber,
                        item.TicketCount.ToString(),
                        item.TotalRevenue.ToString("C")
                    };

                    foreach (var value in data)
                    {
                        var cell = new TableCell();
                        cell.Append(new Paragraph(new Run(new Text(value))));
                        cell.TableCellProperties = new TableCellProperties(
                            new TableCellBorders(
                                new TopBorder() { Val = BorderValues.Single },
                                new BottomBorder() { Val = BorderValues.Single },
                                new LeftBorder() { Val = BorderValues.Single },
                                new RightBorder() { Val = BorderValues.Single }));
                        row.Append(cell);
                    }

                    table.Append(row);

                    totalRevenue += item.TotalRevenue;
                    totalTickets += item.TicketCount;
                }

                // Итоговая строка
                var footerRow = new TableRow();
                string[] footerData = {
                    "ИТОГО:",
                    totalTickets.ToString(),
                    totalRevenue.ToString("C")
                };

                foreach (var value in footerData)
                {
                    var cell = new TableCell();
                    cell.Append(new Paragraph(new Run(new Text(value))));
                    cell.TableCellProperties = new TableCellProperties(
                        new TableCellBorders(
                            new TopBorder() { Val = BorderValues.Single },
                            new BottomBorder() { Val = BorderValues.Single },
                            new LeftBorder() { Val = BorderValues.Single },
                            new RightBorder() { Val = BorderValues.Single }));
                    footerRow.Append(cell);
                }
                table.Append(footerRow);

                body.Append(table);
                doc.Save();
            }
        }
    }
}