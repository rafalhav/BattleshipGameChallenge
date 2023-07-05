using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGameCore.Interfaces
{
    public interface IShipsLocationProvider
    {
        public List<List<Coordinates>> GetShipsLocations(IGameConfiguration gameConfiguration, IShipsConfiguration shipsConfiguration);
    }
}
