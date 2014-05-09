using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

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
            var planilhaDoExcel = new PlanilhaDoExcel(@"D:\Relatório.xlsx");

            planilhaDoExcel.AdicionarDados("Plan1", Celula.From(1, 1), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));
            planilhaDoExcel.AdicionarDados("Plan1", Celula.From(1, 1), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));

            planilhaDoExcel.AdicionarDados("Plan2", Celula.From(1, 1), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));
            planilhaDoExcel.AdicionarDados("Plan2", Celula.From(1, 1), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));

            planilhaDoExcel.AdicionarDados("Plan3", Celula.From(1, 1), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));
            planilhaDoExcel.AdicionarDados("Plan3", Celula.From(1, 1), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));

            planilhaDoExcel.AdicionarDados("Plan4", Celula.From("A5"), DateTime.Now.ToString("dd/MM/yyyy"));
            planilhaDoExcel.AdicionarDados("Plan4", Celula.From("C9"), DateTime.Now.ToString("HH:mm:ss.fff"));

            planilhaDoExcel.Gravar();
        }
    }

    public class PlanilhaDoExcel
    {
        private readonly SpreadsheetDocument _spreadsheetDocument;
        private readonly WorkbookPart _workbookPart;
        private readonly Dictionary<String, SheetData> _dicionario;

        public PlanilhaDoExcel(String fullFileName)
        {
            _dicionario = new Dictionary<string, SheetData>();
            _spreadsheetDocument = SpreadsheetDocument.Create(fullFileName, SpreadsheetDocumentType.Workbook);
            _workbookPart = _spreadsheetDocument.AddWorkbookPart();
            _workbookPart.Workbook = new Workbook(new FileVersion { ApplicationName = "PlanilhaDoExcel" }, new Sheets());
            _workbookPart.AddNewPart<WorkbookStylesPart>().Stylesheet = GenerateStylesheet();
        }

        public SheetData AdicionarPlanilha(String nomeDaPlanilha)
        {
            var planilhaExistente = _dicionario.ContainsKey(nomeDaPlanilha.ToUpper());
            var sheetData = planilhaExistente ? _dicionario[nomeDaPlanilha.ToUpper()] : new SheetData();
            if (!planilhaExistente)
            {
                var worksheetPart = _workbookPart.AddNewPart<WorksheetPart>();
                _workbookPart.Workbook.Sheets.Append(new Sheet() { Name = nomeDaPlanilha, SheetId = (uint)_workbookPart.GetPartsCountOfType<WorksheetPart>(), Id = _workbookPart.GetIdOfPart(worksheetPart) });
                worksheetPart.Worksheet = new Worksheet(sheetData);
                worksheetPart.Worksheet.Save();
                _dicionario.Add(nomeDaPlanilha.ToUpper(), sheetData);
            }
            return sheetData;
        }

        public void AdicionarDados(String nomeDaPlanilha, Celula celula, String conteudo)
        {
            var planilhaExistente = _dicionario.ContainsKey(nomeDaPlanilha.ToUpper());
            var sheetData = planilhaExistente ? _dicionario[nomeDaPlanilha.ToUpper()] : AdicionarPlanilha(nomeDaPlanilha);

            var newRow = new Row { RowIndex = celula.Linha };
            newRow.AppendChild(CreateCell(celula.Referencia, conteudo, null));
            sheetData.Append(newRow);
        }


        public void Gravar()
        {
            _workbookPart.Workbook.Save();
            _spreadsheetDocument.Close();
            _spreadsheetDocument.Dispose();
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
            var cell = new Cell(new InlineString(new Text(cellStringValue)))
            {
                DataType = CellValues.InlineString,
                CellReference = cellReference,
                StyleIndex = styleIndex
            };
            return cell;
        }
    }

    public class Celula
    {
        public uint Linha { get; private set; }
        public String Coluna { get; private set; }
        public String Referencia { get { return Coluna + Linha.ToString(); } }

        public Celula(String coluna, uint linha)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public static Celula From(uint coluna, uint linha)
        {
            return new Celula(GetExcelColumnName(coluna), linha);
        }

        public static Celula From(String referencia)
        {
            return new Celula(GetColuna(referencia), GetLinha(referencia));
        }

        private static String GetExcelColumnName(uint columnIndex)
        {
            if (columnIndex < 26)
                return ((char)('A' + columnIndex)).ToString();

            return string.Format("{0}{1}", (char)('A' + (columnIndex / 26) - 1), (char)('A' + (columnIndex % 26)));
        }

        private static String GetColuna(String referencia)
        {
            return referencia.Substring(0, referencia.IndexOfAny("1234567890".ToCharArray()) - 1);
        }

        private static uint GetLinha(String referencia)
        {
            return Convert.ToUInt32(referencia.Substring(referencia.IndexOfAny("1234567890".ToCharArray())));
        }

    }

}
