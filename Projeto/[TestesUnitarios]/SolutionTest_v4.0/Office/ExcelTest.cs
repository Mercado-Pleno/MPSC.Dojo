using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

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

            var sheets = new Sheets();
            var spreadsheetDocument = SpreadsheetDocument.Create(fullFileName, SpreadsheetDocumentType.Workbook);
            var workbookPart = spreadsheetDocument.AddWorkbookPart();

            workbookPart.Workbook = new Workbook(new FileVersion { ApplicationName = "app" }, sheets);
            workbookPart.AddNewPart<WorkbookStylesPart>().Stylesheet = GenerateStylesheet();

            var dataPlan1 = AdicionarPlanilha(workbookPart, "Plan1");
            var dataPlan2 = AdicionarPlanilha(workbookPart, "Plan2");
            var dataPlan3 = AdicionarPlanilha(workbookPart, "Plan3");
            AdicionarDados(dataPlan1, "A1");
            AdicionarDados(dataPlan2, "B1");
            AdicionarDados(dataPlan3, "C1");
            AdicionarDados(dataPlan1, "D1");
            AdicionarDados(dataPlan2, "E1");
            AdicionarDados(dataPlan3, "F1");


            workbookPart.Workbook.Save();
            spreadsheetDocument.Close();
            spreadsheetDocument.Dispose();
            spreadsheetDocument = null;
        }

        private SheetData AdicionarPlanilha(WorkbookPart workbookPart, String nomeDaPlanilha)
        {
            var sheetData = new SheetData();
            var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            var sheet = new Sheet() { Name = nomeDaPlanilha, SheetId = (uint)workbookPart.GetPartsCountOfType<WorksheetPart>(), Id = workbookPart.GetIdOfPart(worksheetPart) };
            workbookPart.Workbook.Sheets.Append(sheet);
            worksheetPart.Worksheet = new Worksheet(sheetData);
            worksheetPart.Worksheet.Save();

            return sheetData;
        }

        private void AdicionarDados(SheetData sheetData, String celula)
        {
            var newRow = new Row { RowIndex = 1 };
            Cell cell = CreateCell(celula, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"), null);
            newRow.AppendChild(cell);
            sheetData.Append(newRow);
            sheetData.Ancestors<Worksheet>().FirstOrDefault().Save();
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
