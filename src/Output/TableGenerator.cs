using System;
using System.Collections.Generic;
using System.Linq;

namespace Timetracker.Output
{

	class TableGenerator
	{	
		
		private enum LineType
		{
			TopLine,
			SingleLine,
			BottomLine,
		};

		private readonly char ColumnSeparator = (char)0x2503;
		private readonly char RowSeparator = (char)0x2501;

		private readonly char LeftRight = (char)0x2523;
		private readonly char RightLeft = (char)0x252B;

		private readonly char CornerTopLeft = (char)0x250F;
		private readonly char CornerTopRight = (char)0x2513;

		private readonly char CornerBottomLeft = (char)0x2517;
		private readonly char CornerBottomRight = (char)0x251B;

		private readonly char ShadowBottom =  (char)0x2588 ;//(char)0x2588;
		private readonly char ShadowRight = (char)0x2588;

		public bool AddShadow { get; set; } = false;
		public bool ContainsHeader { get; set; } = true;
		public bool ContainsFooter { get; set; } = false;
		public int CellPadding { get; set; } = 2;
		public string Caption { get; set; } = string.Empty;

		private List<string[]> Rows = new List<string[]>();

		private int Columns = 0;

		private Dictionary<int, int> ColLength = new Dictionary<int, int>();


		public void AddRow(params string[] columns)
		{
			if(Columns < columns.Length)
				Columns = columns.Length;

			for(var index = 0; index < columns.Length; index++)
			{
				if(!ColLength.ContainsKey(index))
					ColLength[index] = 0;

				if(columns[index].Length > ColLength[index])
					ColLength[index] = columns[index].Length;
			}

			Rows.Add(columns);
		}


		private string DrawBottomShadow(int length, int offset = 2)
		{
			return "".PadLeft(offset) + new String(ShadowBottom, length + 2);
		}


		private string DrawHorizonalLine(int length, LineType type, bool withShadow)
		{
			string result;
			
			switch(type)
			{
				case LineType.TopLine:
					result = DrawHorizonalLineWithEndings(length, RowSeparator, CornerTopLeft, CornerTopRight);
					break;

				case LineType.BottomLine:
					result = DrawHorizonalLineWithEndings(length, RowSeparator, CornerBottomLeft, CornerBottomRight);
					break;

				default:
					result = DrawHorizonalLineWithEndings(length, RowSeparator, LeftRight, RightLeft);
					break;
			}

			if(withShadow)
				result += ShadowRight + "" + ShadowRight;

			return result;
		}


		private string DrawHorizonalLineWithEndings(int length, char line, char left, char right)
		{
			return left + new String(line, length - 2 + CellPadding) + right;
		}


		public List<string> Generate()
		{
			var result = new List<string>();
			var width = TotalWidth();

			if(!string.IsNullOrEmpty(Caption))
			{
				result.Add(CreateCaption());
				result.Add("");
			}

			result.Add(DrawHorizonalLine(width, LineType.TopLine, false));

			var rows = Rows.Count();
			for(var index = 0; index < rows; index++)
			{
				if(ContainsFooter && index == rows - 1)
					result.Add(DrawHorizonalLine(width, LineType.SingleLine, AddShadow));

				result.Add(BuildRow(Rows[index], AddShadow));

				if(ContainsHeader && index == 0)
					result.Add(DrawHorizonalLine(width, LineType.SingleLine, AddShadow));
			}

			result.Add(DrawHorizonalLine(width, LineType.BottomLine, AddShadow));

			if(AddShadow)
				result.Add(DrawBottomShadow(width));

			return result;
		}


		string CreateCaption()
		{
			var width = TotalWidth();
			var tableCaption = Caption;

			if(tableCaption.Length < width)
				tableCaption = new String(' ', (width - tableCaption.Length) / 2) + tableCaption;

			return tableCaption;
		}


		public override string ToString()
		{
			return string.Join(string.Empty, Generate());		
		}


		private string BuildRow(string[] row, bool withShadow)
		{
			string result = "" + ColumnSeparator;
			string padding = new String(' ', CellPadding);

			for(int index = 0; index < row.Length; index++)
			{
				var pad = ColLength[index];

				// Add some padding to the right if this column
				// contains less then the normal amount of cells.

				if(index == row.Length - 1 && row.Length < Columns)
					pad += TotalWidth() - (result.Length + pad) - CellPadding - 1;

				result += padding;
				result += row[index].PadRight(pad);

				// Add padding on the right side of the last cell
				
				if(index == row.Length - 1)
				 	result += padding;
			}

			result += ColumnSeparator;

			if(withShadow)
				result += ShadowRight + "" + ShadowRight;

			return result;
		}


		int TotalWidth()
		{
			var length = 2;

			foreach(var colLength in ColLength.Values)
				length += colLength + CellPadding;
			
			return length;
		}

	}
}

