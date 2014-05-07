using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MP.Library.TestesUnitarios.SolutionTest_v4.Office
{
    [TestClass]
    public class ExcelTest
    {
        [TestInitialize()]
        public void MyTestInitialize() { }

        [TestCleanup()]
        public void MyTestCleanup() { }


        [TestMethod]
        public void TestMethod1()
        {
            var fullFileName = @"D:\Relatório.xlsx";

            var spreadsheetDocument = SpreadsheetDocument.Create(fullFileName, SpreadsheetDocumentType.Workbook);

            var workbookPart = spreadsheetDocument.AddWorkbookPart();
            workbookPart.Workbook = new Workbook(new FileVersion { ApplicationName = "app" });

            var workbookStylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
            workbookStylesPart.Stylesheet = GenerateStylesheet();

            var sheets = new Sheets();
            workbookPart.Workbook.Append(sheets);

            var worksheetPart1 = workbookPart.AddNewPart<WorksheetPart>();
            sheets.Append(new Sheet() { Name = "Plan1", SheetId = 1, Id = workbookPart.GetIdOfPart(worksheetPart1) });
            var sheetData1 = new SheetData();
            worksheetPart1.Worksheet = new Worksheet(sheetData1);
            worksheetPart1.Worksheet.Save();

            var newRow1 = new Row { RowIndex = 1 };
            Cell cell1 = CreateCell("A1", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"), null);
            newRow1.AppendChild(cell1);
            sheetData1.Append(newRow1);



            var worksheetPart2 = workbookPart.AddNewPart<WorksheetPart>();
            sheets.Append(new Sheet() { Name = "Plan2", SheetId = 2, Id = workbookPart.GetIdOfPart(worksheetPart2) });
            var sheetData2 = new SheetData();
            worksheetPart2.Worksheet = new Worksheet(sheetData2);
            worksheetPart2.Worksheet.Save();

            var newRow2 = new Row { RowIndex = 2 };
            Cell cell2 = CreateCell("B2", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"), null);
            newRow2.AppendChild(cell2);
            sheetData2.Append(newRow2);

            worksheetPart2.Worksheet.Save();


            var worksheetPart3 = workbookPart.AddNewPart<WorksheetPart>();
            sheets.Append(new Sheet() { Name = "Plan3", SheetId = 3, Id = workbookPart.GetIdOfPart(worksheetPart3) });
            var sheetData3 = new SheetData();
            worksheetPart3.Worksheet = new Worksheet(sheetData3);
            worksheetPart3.Worksheet.Save();

            var newRow3 = new Row { RowIndex = 3 };
            Cell cell3 = CreateCell("C3", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"), null);
            newRow3.AppendChild(cell3);
            sheetData3.Append(newRow3);

            worksheetPart2.Worksheet.Save();


            workbookPart.Workbook.Save();
            spreadsheetDocument.Close();
            spreadsheetDocument.Dispose();
            spreadsheetDocument = null;
        }

        private Stylesheet GenerateStylesheet()
        {
            var stylesheet = new Stylesheet();
            stylesheet.Append(CreateFonts());
            stylesheet.Append(CreateFills());
            stylesheet.Append(CreateBorders());
            stylesheet.Append(CreateCellStyleFormats());
            stylesheet.Append(CreateCellFormats());
            stylesheet.Append(CreateCellStyles());
            return stylesheet;
        }


        private Fonts CreateFonts()
        {
            var fonts = new Fonts { Count = 1U };
            fonts.Append(CreateFont());
            return fonts;
        }
        private Font CreateFont()
        {
            var font = new Font();
            font.Append(new FontSize { Val = 11D });
            font.Append(new Color { Theme = 1U });
            font.Append(new FontName { Val = "Calibri" });
            font.Append(new FontFamilyNumbering { Val = 2 });
            font.Append(new FontScheme { Val = FontSchemeValues.Minor });
            return font;
        }


        private Fills CreateFills()
        {
            var fills = new Fills { Count = 2U };
            fills.Append(CreateFill());
            return fills;
        }
        private Fill CreateFill()
        {
            var fill = new Fill();
            var patternFill = new PatternFill { PatternType = PatternValues.None };
            fill.Append(patternFill);
            return fill;
        }


        private Borders CreateBorders()
        {
            var borders = new Borders { Count = 1U };
            borders.Append(CreateBorder());
            return borders;
        }
        private Border CreateBorder()
        {
            var border = new Border();
            border.Append(new LeftBorder());
            border.Append(new RightBorder());
            border.Append(new TopBorder());
            border.Append(new BottomBorder());
            border.Append(new DiagonalBorder());
            return border;
        }


        private CellStyles CreateCellStyles()
        {
            var cellStyles = new CellStyles { Count = 1 };
            cellStyles.Append(new CellStyle { Name = "Normal", FormatId = 0, BuiltinId = 0 });
            return cellStyles;
        }
        private CellStyle CreateCellStyle()
        {
            return new CellStyle
            {
                Name = "Normal",
                FormatId = 0,
                BuiltinId = 0
            };
        }


        private CellFormats CreateCellFormats()
        {
            var cellFormats = new CellFormats { Count = 1U };
            cellFormats.Append(CreateCellStyleFormat());
            return cellFormats;
        }
        private CellFormat CreateCellStyleFormat()
        {
            return new CellFormat()
            {
                NumberFormatId = 0U,
                FontId = 0U,
                FillId = 0U
            };
        }


        private CellStyleFormats CreateCellStyleFormats()
        {
            var cellStyleFormats = new CellStyleFormats { Count = 1U };
            cellStyleFormats.Append(CreateCellStyleFormat());
            return cellStyleFormats;
        }

        private Cell CreateCell(string cellReference, string cellStringValue, uint? styleIndex)
        {
            var cell = new Cell();
            cell.DataType = CellValues.InlineString;

            var t = new Text();
            t.Text = cellStringValue;

            if (styleIndex.HasValue)
                cell.StyleIndex = styleIndex;

            var inlineString = new InlineString();
            inlineString.AppendChild(t);

            cell.AppendChild(inlineString);

            cell.CellReference = cellReference;

            return cell;
        }
    }
}
