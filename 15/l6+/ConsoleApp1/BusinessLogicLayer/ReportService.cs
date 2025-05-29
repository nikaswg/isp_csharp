using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormatOpenXml = DocumentFormat.OpenXml;
using DocumentFormatOpenXmlWordprocessing = DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormatOpenXmlPackaging = DocumentFormat.OpenXml.Packaging;
using Lunopark.Core.Entities;
using Lunopark.Data;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using WordDocType = DocumentFormat.OpenXml.WordprocessingDocumentType;

namespace Lunopark.Services
{
    public class ReportService
    {
        private readonly UnitOfWork _unitOfWork;

        public ReportService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void GenerateEmployeesReport(string filePath)
        {
            var employees = _unitOfWork.Employees.GetAll().ToList();

            using (var doc = DocumentFormatOpenXmlPackaging.WordprocessingDocument.Create(
                filePath, WordDocType.Document))
            {
                var mainPart = doc.AddMainDocumentPart();
                mainPart.Document = new DocumentFormatOpenXmlWordprocessing.Document();
                var body = mainPart.Document.AppendChild(new DocumentFormatOpenXmlWordprocessing.Body());

                // Заголовок
                var title = new DocumentFormatOpenXmlWordprocessing.Paragraph();
                var run = new DocumentFormatOpenXmlWordprocessing.Run();
                run.Append(new DocumentFormatOpenXmlWordprocessing.Text("Отчет по сотрудникам Лунопарка"));
                run.RunProperties = new DocumentFormatOpenXmlWordprocessing.RunProperties(
                    new DocumentFormatOpenXmlWordprocessing.Bold());
                title.Append(run);
                body.Append(title);

                // Таблица с данными
                var table = new DocumentFormatOpenXmlWordprocessing.Table();

                // Заголовки таблицы
                var headerRow = new DocumentFormatOpenXmlWordprocessing.TableRow();
                string[] headers = { "ФИО", "Должность", "Телефон", "Год рождения" };

                foreach (var header in headers)
                {
                    var cell = new DocumentFormatOpenXmlWordprocessing.TableCell();
                    cell.Append(new DocumentFormatOpenXmlWordprocessing.Paragraph(
                        new DocumentFormatOpenXmlWordprocessing.Run(
                            new DocumentFormatOpenXmlWordprocessing.Text(header))));

                    cell.TableCellProperties = new DocumentFormatOpenXmlWordprocessing.TableCellProperties(
                        new DocumentFormatOpenXmlWordprocessing.TableCellBorders(
                            new DocumentFormatOpenXmlWordprocessing.TopBorder()
                            {
                                Val =
                                DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                            },
                            new DocumentFormatOpenXmlWordprocessing.BottomBorder()
                            {
                                Val =
                                DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                            },
                            new DocumentFormatOpenXmlWordprocessing.LeftBorder()
                            {
                                Val =
                                DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                            },
                            new DocumentFormatOpenXmlWordprocessing.RightBorder()
                            {
                                Val =
                                DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                            }));
                    headerRow.Append(cell);
                }
                table.Append(headerRow);

                // Данные сотрудников
                foreach (var employee in employees)
                {
                    var row = new DocumentFormatOpenXmlWordprocessing.TableRow();

                    string[] data = {
                        employee.FullName,
                        employee.Position,
                        employee.Phone ?? "-",
                        employee.BirthYear?.ToString() ?? "-"
                    };

                    foreach (var item in data)
                    {
                        var cell = new DocumentFormatOpenXmlWordprocessing.TableCell();
                        cell.Append(new DocumentFormatOpenXmlWordprocessing.Paragraph(
                            new DocumentFormatOpenXmlWordprocessing.Run(
                                new DocumentFormatOpenXmlWordprocessing.Text(item))));

                        cell.TableCellProperties = new DocumentFormatOpenXmlWordprocessing.TableCellProperties(
                            new DocumentFormatOpenXmlWordprocessing.TableCellBorders(
                                new DocumentFormatOpenXmlWordprocessing.TopBorder()
                                {
                                    Val =
                                    DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                                },
                                new DocumentFormatOpenXmlWordprocessing.BottomBorder()
                                {
                                    Val =
                                    DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                                },
                                new DocumentFormatOpenXmlWordprocessing.LeftBorder()
                                {
                                    Val =
                                    DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                                },
                                new DocumentFormatOpenXmlWordprocessing.RightBorder()
                                {
                                    Val =
                                    DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                                }));
                        row.Append(cell);
                    }

                    table.Append(row);
                }

                body.Append(table);
                doc.Save();
            }
        }

        public void GenerateAttractionsByYearReport(int year, string filePath)
        {
            var attractions = _unitOfWork.Attractions.GetAll()
                .Where(a => a.InstallationYear == year)
                .OrderBy(a => a.AttractionName)
                .ToList();

            using (var doc = DocumentFormatOpenXmlPackaging.WordprocessingDocument.Create(
                filePath, WordDocType.Document))
            {
                var mainPart = doc.AddMainDocumentPart();
                mainPart.Document = new DocumentFormatOpenXmlWordprocessing.Document();
                var body = mainPart.Document.AppendChild(new DocumentFormatOpenXmlWordprocessing.Body());

                // Заголовок
                var title = new DocumentFormatOpenXmlWordprocessing.Paragraph();
                var run = new DocumentFormatOpenXmlWordprocessing.Run();
                run.Append(new DocumentFormatOpenXmlWordprocessing.Text(
                    $"Отчет по аттракционам установленным в {year} году"));
                run.RunProperties = new DocumentFormatOpenXmlWordprocessing.RunProperties(
                    new DocumentFormatOpenXmlWordprocessing.Bold());
                title.Append(run);
                body.Append(title);

                // Таблица с данными
                var table = new DocumentFormatOpenXmlWordprocessing.Table();

                // Заголовки таблицы
                var headerRow = new DocumentFormatOpenXmlWordprocessing.TableRow();
                string[] headers = { "Название", "Год установки", "Ответственный" };

                foreach (var header in headers)
                {
                    var cell = new DocumentFormatOpenXmlWordprocessing.TableCell();
                    cell.Append(new DocumentFormatOpenXmlWordprocessing.Paragraph(
                        new DocumentFormatOpenXmlWordprocessing.Run(
                            new DocumentFormatOpenXmlWordprocessing.Text(header))));

                    cell.TableCellProperties = new DocumentFormatOpenXmlWordprocessing.TableCellProperties(
                        new DocumentFormatOpenXmlWordprocessing.TableCellBorders(
                            new DocumentFormatOpenXmlWordprocessing.TopBorder()
                            {
                                Val =
                                DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                            },
                            new DocumentFormatOpenXmlWordprocessing.BottomBorder()
                            {
                                Val =
                                DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                            },
                            new DocumentFormatOpenXmlWordprocessing.LeftBorder()
                            {
                                Val =
                                DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                            },
                            new DocumentFormatOpenXmlWordprocessing.RightBorder()
                            {
                                Val =
                                DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                            }));
                    headerRow.Append(cell);
                }
                table.Append(headerRow);

                // Данные аттракционов
                foreach (var attraction in attractions)
                {
                    var responsible = attraction.ResponsibleEmployeeId.HasValue ?
                        _unitOfWork.Employees.GetById(attraction.ResponsibleEmployeeId.Value)?.FullName : "-";

                    var row = new DocumentFormatOpenXmlWordprocessing.TableRow();

                    string[] data = {
                        attraction.AttractionName,
                        attraction.InstallationYear.ToString(),
                        responsible ?? "-"
                    };

                    foreach (var item in data)
                    {
                        var cell = new DocumentFormatOpenXmlWordprocessing.TableCell();
                        cell.Append(new DocumentFormatOpenXmlWordprocessing.Paragraph(
                            new DocumentFormatOpenXmlWordprocessing.Run(
                                new DocumentFormatOpenXmlWordprocessing.Text(item))));

                        cell.TableCellProperties = new DocumentFormatOpenXmlWordprocessing.TableCellProperties(
                            new DocumentFormatOpenXmlWordprocessing.TableCellBorders(
                                new DocumentFormatOpenXmlWordprocessing.TopBorder()
                                {
                                    Val =
                                    DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                                },
                                new DocumentFormatOpenXmlWordprocessing.BottomBorder()
                                {
                                    Val =
                                    DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                                },
                                new DocumentFormatOpenXmlWordprocessing.LeftBorder()
                                {
                                    Val =
                                    DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                                },
                                new DocumentFormatOpenXmlWordprocessing.RightBorder()
                                {
                                    Val =
                                    DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                                }));
                        row.Append(cell);
                    }

                    table.Append(row);
                }

                body.Append(table);
                doc.Save();
            }
        }

        public void GenerateAttractionRevenueReport(DateTime startDate, DateTime endDate, string filePath)
        {
            var tickets = _unitOfWork.Tickets.GetAll()
                .Where(t => t.SaleDate >= startDate && t.SaleDate <= endDate)
                .ToList();

            var attractions = _unitOfWork.Attractions.GetAll().ToList();

            var reportData = from a in attractions
                             join t in tickets on a.AttractionId equals t.AttractionId into at
                             from subTicket in at.DefaultIfEmpty()
                             group subTicket by new { a.AttractionId, a.AttractionName } into g
                             select new
                             {
                                 AttractionId = g.Key.AttractionId,
                                 AttractionName = g.Key.AttractionName,
                                 TicketCount = g.Count(t => t != null),
                                 TotalRevenue = g.Sum(t => t?.TicketPrice ?? 0)
                             };

            using (var doc = DocumentFormatOpenXmlPackaging.WordprocessingDocument.Create(
                filePath, WordDocType.Document))
            {
                var mainPart = doc.AddMainDocumentPart();
                mainPart.Document = new DocumentFormatOpenXmlWordprocessing.Document();
                var body = mainPart.Document.AppendChild(new DocumentFormatOpenXmlWordprocessing.Body());

                // Заголовок
                var title = new DocumentFormatOpenXmlWordprocessing.Paragraph();
                var run = new DocumentFormatOpenXmlWordprocessing.Run();
                run.Append(new DocumentFormatOpenXmlWordprocessing.Text(
                    $"Отчет по доходам аттракционов за период с {startDate:dd.MM.yyyy} по {endDate:dd.MM.yyyy}"));
                run.RunProperties = new DocumentFormatOpenXmlWordprocessing.RunProperties(
                    new DocumentFormatOpenXmlWordprocessing.Bold());
                title.Append(run);
                body.Append(title);

                // Таблица с данными
                var table = new DocumentFormatOpenXmlWordprocessing.Table();

                // Заголовки таблицы
                var headerRow = new DocumentFormatOpenXmlWordprocessing.TableRow();
                string[] headers = { "Аттракцион", "Кол-во билетов", "Сумма дохода" };

                foreach (var header in headers)
                {
                    var cell = new DocumentFormatOpenXmlWordprocessing.TableCell();
                    cell.Append(new DocumentFormatOpenXmlWordprocessing.Paragraph(
                        new DocumentFormatOpenXmlWordprocessing.Run(
                            new DocumentFormatOpenXmlWordprocessing.Text(header))));

                    cell.TableCellProperties = new DocumentFormatOpenXmlWordprocessing.TableCellProperties(
                        new DocumentFormatOpenXmlWordprocessing.TableCellBorders(
                            new DocumentFormatOpenXmlWordprocessing.TopBorder()
                            {
                                Val =
                                DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                            },
                            new DocumentFormatOpenXmlWordprocessing.BottomBorder()
                            {
                                Val =
                                DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                            },
                            new DocumentFormatOpenXmlWordprocessing.LeftBorder()
                            {
                                Val =
                                DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                            },
                            new DocumentFormatOpenXmlWordprocessing.RightBorder()
                            {
                                Val =
                                DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                            }));
                    headerRow.Append(cell);
                }
                table.Append(headerRow);

                // Данные аттракционов
                decimal totalRevenue = 0;
                int totalTickets = 0;

                foreach (var item in reportData.OrderByDescending(r => r.TotalRevenue))
                {
                    var row = new DocumentFormatOpenXmlWordprocessing.TableRow();

                    string[] data = {
                        item.AttractionName,
                        item.TicketCount.ToString(),
                        item.TotalRevenue.ToString("C")
                    };

                    foreach (var value in data)
                    {
                        var cell = new DocumentFormatOpenXmlWordprocessing.TableCell();
                        cell.Append(new DocumentFormatOpenXmlWordprocessing.Paragraph(
                            new DocumentFormatOpenXmlWordprocessing.Run(
                                new DocumentFormatOpenXmlWordprocessing.Text(value))));

                        cell.TableCellProperties = new DocumentFormatOpenXmlWordprocessing.TableCellProperties(
                            new DocumentFormatOpenXmlWordprocessing.TableCellBorders(
                                new DocumentFormatOpenXmlWordprocessing.TopBorder()
                                {
                                    Val =
                                    DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                                },
                                new DocumentFormatOpenXmlWordprocessing.BottomBorder()
                                {
                                    Val =
                                    DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                                },
                                new DocumentFormatOpenXmlWordprocessing.LeftBorder()
                                {
                                    Val =
                                    DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                                },
                                new DocumentFormatOpenXmlWordprocessing.RightBorder()
                                {
                                    Val =
                                    DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                                }));
                        row.Append(cell);
                    }

                    table.Append(row);

                    totalRevenue += item.TotalRevenue;
                    totalTickets += item.TicketCount;
                }

                // Итоговая строка
                var footerRow = new DocumentFormatOpenXmlWordprocessing.TableRow();
                string[] footerData = {
                    "ИТОГО:",
                    totalTickets.ToString(),
                    totalRevenue.ToString("C")
                };

                foreach (var value in footerData)
                {
                    var cell = new DocumentFormatOpenXmlWordprocessing.TableCell();
                    cell.Append(new DocumentFormatOpenXmlWordprocessing.Paragraph(
                        new DocumentFormatOpenXmlWordprocessing.Run(
                            new DocumentFormatOpenXmlWordprocessing.Text(value))));

                    cell.TableCellProperties = new DocumentFormatOpenXmlWordprocessing.TableCellProperties(
                        new DocumentFormatOpenXmlWordprocessing.TableCellBorders(
                            new DocumentFormatOpenXmlWordprocessing.TopBorder()
                            {
                                Val =
                                DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                            },
                            new DocumentFormatOpenXmlWordprocessing.BottomBorder()
                            {
                                Val =
                                DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                            },
                            new DocumentFormatOpenXmlWordprocessing.LeftBorder()
                            {
                                Val =
                                DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                            },
                            new DocumentFormatOpenXmlWordprocessing.RightBorder()
                            {
                                Val =
                                DocumentFormatOpenXmlWordprocessing.BorderValues.Single
                            }));
                    footerRow.Append(cell);
                }
                table.Append(footerRow);

                body.Append(table);
                doc.Save();
            }
        }

        public void GenerateAttractionsReport(int year, string filePath)
        {
            GenerateAttractionsByYearReport(year, filePath);
        }

        public void GenerateRevenueReport(DateTime startDate, DateTime endDate, string filePath)
        {
            GenerateAttractionRevenueReport(startDate, endDate, filePath);
        }
    }
}