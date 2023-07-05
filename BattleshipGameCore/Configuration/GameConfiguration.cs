using BattleshipGameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGameCore.Configuration
{
    public class GameConfiguration : IGameConfiguration
    {
        public int RowCount { get; }

        public int ColumnCount { get; }


        public GameConfiguration(int ColumnCount, int RowCount) 
        {
            this.ColumnCount = ColumnCount;
            this.RowCount = RowCount;
        }
    }
}
