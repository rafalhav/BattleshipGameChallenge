using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGameCore.Interfaces
{
    public interface IGameConfiguration
    {
        public int RowCount { get; }

        public int ColumnCount { get; }
    }
}
