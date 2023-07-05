using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGameCore
{
    public class Coordinates
    {
        public int ColumnIndex { get; }

        public int RowIndex { get; }

        public string CoordinateString => (char)(ColumnIndex + 64) + RowIndex.ToString();

        public Coordinates(int columnIndex, int rowIndex)
        {
            ColumnIndex = columnIndex;
            RowIndex = rowIndex;
        }
    }
}
