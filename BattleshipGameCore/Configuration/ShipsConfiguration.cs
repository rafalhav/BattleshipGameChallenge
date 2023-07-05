using BattleshipGameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGameCore.Configuration
{
    public class ShipsConfiguration : IShipsConfiguration
    {
        public List<int> ShipsSizes { get; }

        public ShipsConfiguration(List<int> shipSizes) 
        {
            ShipsSizes = shipSizes;
        }
    }
}
