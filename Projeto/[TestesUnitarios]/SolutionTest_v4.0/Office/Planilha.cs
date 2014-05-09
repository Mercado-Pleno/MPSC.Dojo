using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace MP.Library.TestesUnitarios.SolutionTest_v4.Office
{
	public enum Style : uint
	{
		Number = 0,
		Long = 1,
		Double = 4,
		Header = 999
	}

	public class Planilha
	{
		private readonly SpreadsheetDocument _spreadsheetDocument;
		private readonly WorkbookPart _workbookPart;
		private readonly Dictionary<String, SheetData> _planilhas;
		private readonly Dictionary<Style, UInt32Value> _styles;
		private List<PropertyInfo> _propertyInfos;

		public Planilha(String fullFileName)
		{
			_planilhas = new Dictionary<string, SheetData>();
			_styles = new Dictionary<Style, UInt32Value>();
			_spreadsheetDocument = SpreadsheetDocument.Create(fullFileName, SpreadsheetDocumentType.Workbook);
			_workbookPart = _spreadsheetDocument.AddWorkbookPart();
			_workbookPart.Workbook = new Workbook(new FileVersion { ApplicationName = "PlanilhaDoExcel" }, new Sheets());
			_workbookPart.AddNewPart<WorkbookStylesPart>().Stylesheet = new Formatacao().GenerateStylesheet(_styles);
		}

		public void AdicionarDados<T>(String nomeDaPlanilha, IEnumerable<T> lista)
		{
			AdicionarCabecalho<T>(nomeDaPlanilha);
			AdicionarLinhas<T>(nomeDaPlanilha, lista);
		}

		public void Escrever(String nomeDaPlanilha, Celula celula, String conteudo)
		{
			Escrever(nomeDaPlanilha, celula, conteudo, null);
		}

		public void Escrever(String nomeDaPlanilha, Celula celula, String conteudo, uint? styleIndex)
		{
			var planilhaExistente = _planilhas.ContainsKey(nomeDaPlanilha.ToUpper());
			var sheetData = planilhaExistente ? _planilhas[nomeDaPlanilha.ToUpper()] : AdicionarPlanilha(nomeDaPlanilha);

			var row = sheetData.ChildElements.Cast<Row>().FirstOrDefault(r => r.RowIndex == celula.Linha);
			if (row == null)
			{
				row = new Row { RowIndex = celula.Linha };
				sheetData.Append(row);
			}
			var cell = row.ChildElements.Cast<Cell>().FirstOrDefault(c => c.CellReference == celula.Referencia);
			if (cell != null)
				row.RemoveChild(cell);

			row.AppendChild(CreateTextCell(celula.Referencia, conteudo, styleIndex));
		}

		public void Gravar()
		{
			_workbookPart.Workbook.Save();
			_spreadsheetDocument.Close();
			_spreadsheetDocument.Dispose();
		}

		private SheetData AdicionarPlanilha(String nomeDaPlanilha)
		{
			var planilhaExistente = _planilhas.ContainsKey(nomeDaPlanilha.ToUpper());
			var sheetData = planilhaExistente ? _planilhas[nomeDaPlanilha.ToUpper()] : new SheetData();
			if (!planilhaExistente)
			{
				var worksheetPart = _workbookPart.AddNewPart<WorksheetPart>();
				_workbookPart.Workbook.Sheets.Append(new Sheet() { Name = nomeDaPlanilha, SheetId = (uint)_workbookPart.GetPartsCountOfType<WorksheetPart>(), Id = _workbookPart.GetIdOfPart(worksheetPart) });
				worksheetPart.Worksheet = new Worksheet(sheetData);
				worksheetPart.Worksheet.Save();
				_planilhas.Add(nomeDaPlanilha.ToUpper(), sheetData);
			}
			return sheetData;
		}

		private void AdicionarCabecalho<T>(String nomeDaPlanilha)
		{
			var planilhaExistente = _planilhas.ContainsKey(nomeDaPlanilha.ToUpper());
			var sheetData = planilhaExistente ? _planilhas[nomeDaPlanilha.ToUpper()] : AdicionarPlanilha(nomeDaPlanilha);
			var header = new Row { RowIndex = Convert.ToUInt32(sheetData.ChildElements.Count()) + 1 };
			sheetData.Append(header);

			_propertyInfos = typeof(T).GetProperties().Where(p => p.GetCustomAttributes(false).Any()).ToList();

			int numberOfColumns = _propertyInfos.Count();
			uint columnsCount = 0;
			foreach (PropertyInfo propertyInfo in _propertyInfos)
			{
				columnsCount++;
				IEnumerable<SheetColumnAttribute> attributes = propertyInfo.GetCustomAttributes(true).OfType<SheetColumnAttribute>();
				foreach (SheetColumnAttribute attr in attributes)
				{
					header.Append(CreateTextCell(Celula.From(columnsCount, 1).Referencia, attr.Description, _styles[Style.Header]));
				}
			}
		}

		private void AdicionarLinhas<T>(String nomeDaPlanilha, IEnumerable<T> list)
		{
			var planilhaExistente = _planilhas.ContainsKey(nomeDaPlanilha.ToUpper());
			var sheetData = planilhaExistente ? _planilhas[nomeDaPlanilha.ToUpper()] : AdicionarPlanilha(nomeDaPlanilha);

			foreach (T item in list)
			{
				var rowIndex = Convert.ToUInt32(sheetData.ChildElements.Count() + 1);
				var newRow = new Row { RowIndex = rowIndex };
				uint columnIndex = 0;

				foreach (PropertyInfo propertyInfo in _propertyInfos)
				{
					var cellReference = Celula.From(++columnIndex, rowIndex).Referencia;
					object value = propertyInfo.GetValue(item, null);
					string cellValue = value != null ? value.ToString() : string.Empty;

					IEnumerable<SheetColumnAttribute> attributes = propertyInfo.GetCustomAttributes(true).OfType<SheetColumnAttribute>();
					foreach (SheetColumnAttribute attr in attributes)
					{
						switch (attr.DataType)
						{
							case DataTypeEnum.Int:
							case DataTypeEnum.ExcelGeneral:
								newRow.AppendChild(CreateNumericCell(cellReference, cellValue, _styles[Style.Number], attr.NumberFormat));
								break;

							case DataTypeEnum.Long:
								newRow.AppendChild(CreateNumericCell(cellReference, cellValue, _styles[Style.Long], attr.NumberFormat));
								break;

							case DataTypeEnum.Double:
							case DataTypeEnum.Decimal:
								newRow.AppendChild(CreateNumericCell(cellReference, cellValue, _styles[Style.Double], attr.NumberFormat));
								break;

							case DataTypeEnum.DateTime:
								var datetime = (DateTime?)value;
								var date = (datetime != null) && datetime.HasValue && (datetime.Value.Year > 1) ? datetime.Value : DateTime.MinValue;
								string dateValue = (date > DateTime.MinValue) ? date.ToString(attr.DataFormat) : String.Empty;
								newRow.AppendChild(CreateTextCell(cellReference, dateValue, null));
								break;

							default:
								newRow.AppendChild(CreateTextCell(cellReference, cellValue, null));
								break;
						}
					}

				}
				sheetData.Append(newRow);

				if (newRow.RowIndex % 20000 == 0)
				{
					_workbookPart.Workbook.Save();
					GC.Collect();
				}
			}
			_workbookPart.Workbook.Save();
			_workbookPart.WorksheetParts.FirstOrDefault().Worksheet.Save();
			GC.Collect();
		}


		private Cell CreateTextCell(string cellReference, string cellStringValue, uint? styleIndex)
		{
			var cell = new Cell(new InlineString(new Text(cellStringValue)))
			{
				DataType = CellValues.InlineString,
				CellReference = cellReference,
				StyleIndex = styleIndex
			};
			return cell;
		}

		private Cell CreateNumericCell(string cellReference, string cellStringValue, uint? styleIndex, String formato)
		{
			cellStringValue = TransformInNumericValue(cellStringValue, formato);
			var cell = new Cell(new CellValue(cellStringValue))
			{
				DataType = CellValues.Number,
				CellReference = cellReference,
				StyleIndex = styleIndex
			};

			return cell;
		}

		private string TransformInNumericValue(string cellValue, string formato)
		{
			double doubleCellValue;
			if (Double.TryParse(cellValue, out doubleCellValue))
				cellValue = String.IsNullOrEmpty(formato)
					? doubleCellValue.ToString("F", CultureInfo.InvariantCulture)
					: String.Format(CultureInfo.InvariantCulture, formato, doubleCellValue);

			return cellValue;
		}
	}

	public class Formatacao
	{
		#region // "Styles"

		public Stylesheet GenerateStylesheet(Dictionary<Style, UInt32Value> _styles)
		{
			var stylesheet = new Stylesheet();
			stylesheet.Append(CreateFonts());
			stylesheet.Append(CreateFills());
			stylesheet.Append(CreateBorders());
			stylesheet.Append(CreateCellStyleFormats());
			stylesheet.Append(CreateCellFormats());
			stylesheet.Append(CreateCellStyles());

			_styles[Style.Number] = CreateCellFormat(stylesheet, null, UInt32Value.FromUInt32((uint)Style.Number));
			_styles[Style.Long] = CreateCellFormat(stylesheet, null, UInt32Value.FromUInt32((uint)Style.Long));
			_styles[Style.Double] = CreateCellFormat(stylesheet, null, UInt32Value.FromUInt32((uint)Style.Double));
			_styles[Style.Header] = CreateCellFormat(stylesheet, CreateFont(stylesheet, "Calibri", 11, true), null);

			return stylesheet;
		}

		public Fonts CreateFonts()
		{
			var fonts = new Fonts { Count = 1U };
			fonts.Append(CreateFont());
			return fonts;
		}
		public Font CreateFont()
		{
			var font = new Font();
			font.Append(new FontSize { Val = 11D });
			font.Append(new Color { Theme = 1U });
			font.Append(new FontName { Val = "Calibri" });
			font.Append(new FontFamilyNumbering { Val = 2 });
			font.Append(new FontScheme { Val = FontSchemeValues.Minor });
			return font;
		}

		public UInt32Value CreateFont(Stylesheet stylesheet, string fontName, double? fontSize, bool isBold)
		{
			Font font = new Font();
			stylesheet.Fonts.Append(font);

			if (!string.IsNullOrEmpty(fontName))
				font.Append(new FontName() { Val = fontName });

			if (fontSize.HasValue)
				font.Append(new FontSize() { Val = fontSize.Value });

			if (isBold)
				font.Append(new Bold());

			return stylesheet.Fonts.Count++;
		}

		public Fills CreateFills()
		{
			var fills = new Fills { Count = 2U };
			fills.Append(CreateFill());
			return fills;
		}
		public Fill CreateFill()
		{
			var fill = new Fill();
			var patternFill = new PatternFill { PatternType = PatternValues.None };
			fill.Append(patternFill);
			return fill;
		}


		public Borders CreateBorders()
		{
			var borders = new Borders { Count = 1U };
			borders.Append(CreateBorder());
			return borders;
		}
		public Border CreateBorder()
		{
			var border = new Border();
			border.Append(new LeftBorder());
			border.Append(new RightBorder());
			border.Append(new TopBorder());
			border.Append(new BottomBorder());
			border.Append(new DiagonalBorder());
			return border;
		}


		public CellStyles CreateCellStyles()
		{
			var cellStyles = new CellStyles { Count = 1 };
			cellStyles.Append(new CellStyle { Name = "Normal", FormatId = 0, BuiltinId = 0 });
			return cellStyles;
		}
		public CellStyle CreateCellStyle()
		{
			return new CellStyle
			{
				Name = "Normal",
				FormatId = 0,
				BuiltinId = 0
			};
		}


		public CellFormats CreateCellFormats()
		{
			var cellFormats = new CellFormats { Count = 1U };
			cellFormats.Append(CreateCellStyleFormat());
			return cellFormats;
		}
		public CellFormat CreateCellStyleFormat()
		{
			return new CellFormat()
			{
				NumberFormatId = 0U,
				FontId = 0U,
				FillId = 0U
			};
		}


		public CellStyleFormats CreateCellStyleFormats()
		{
			var cellStyleFormats = new CellStyleFormats { Count = 1U };
			cellStyleFormats.Append(CreateCellStyleFormat());
			return cellStyleFormats;
		}

		public UInt32Value CreateCellFormat(Stylesheet stylesheet, UInt32Value fontId, UInt32Value numberFormatId)
		{
			var cellFormat = new CellFormat();
			stylesheet.CellFormats.Append(cellFormat);

			if (fontId != null)
				cellFormat.FontId = fontId;

			if (numberFormatId != null)
			{
				cellFormat.NumberFormatId = numberFormatId;
				cellFormat.ApplyNumberFormat = BooleanValue.FromBoolean(true);
			}

			return stylesheet.CellFormats.Count++;
		}

		#endregion // "Styles"
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
			return new Celula(GetExcelColumnName(coluna - 1), linha);
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
			return referencia.Substring(0, referencia.IndexOfAny("1234567890".ToCharArray()));
		}

		private static uint GetLinha(String referencia)
		{
			return Convert.ToUInt32(referencia.Substring(referencia.IndexOfAny("1234567890".ToCharArray())));
		}
	}

	public enum DataTypeEnum
	{
		String,
		Int,
		Long,
		Double,
		Decimal,
		DateTime,
		ExcelGeneral
	}

	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class SheetColumnAttribute : Attribute
	{
		public string Description { get; set; }
		public short Order { get; set; }
		public string Format { get; set; }
		public string NumberFormat { get; set; }
		public DataTypeEnum DataType { get; set; }
		public string DataFormat
		{
			get { return Format ?? (Format = "{0:yyyy/MM/dd}"); }
			set { Format = value; }
		}

		public SheetColumnAttribute()
		{
			DataType = DataTypeEnum.String;
		}

		public SheetColumnAttribute(string descricao)
			: this()
		{
			Description = descricao;
		}

		public SheetColumnAttribute(short order)
			: this()
		{
			Order = order;
		}

		public SheetColumnAttribute(string description, short order)
			: this()
		{
			Description = description;
			Order = order;
		}

		public SheetColumnAttribute(string descricao, DataTypeEnum dataType)
			: this()
		{
			Description = descricao;
			DataType = dataType;
		}

		public SheetColumnAttribute(string descricao, DataTypeEnum dataType, string dataFormat)
			: this()
		{
			Description = descricao;
			DataType = dataType;
			DataFormat = dataFormat;
		}
	}
}
