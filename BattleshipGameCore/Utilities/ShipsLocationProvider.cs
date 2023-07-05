using BattleshipGameCore.Configuration;
using BattleshipGameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGameCore.Utilities
{
    public class ShipsRandomLocationProvider : IShipsLocationProvider
    {
        public List<List<Coordinates>> GetShipsLocations(IGameConfiguration gameConfiguration, IShipsConfiguration shipsConfiguration)
        {
            List<List<Coordinates>> randomShipsLocations = new List<List<Coordinates>>();
            int safetySwitch = 0;

            bool shipCollision;
            do
            {
                foreach (var shipSize in shipsConfiguration.ShipsSizes)
                {
                    randomShipsLocations.Add(RandomizeHelper.GetRandomCoordinates(shipSize, gameConfiguration.ColumnCount, gameConfiguration.RowCount));
                }
                shipCollision = CollisionDetector.AreShipsColliding(randomShipsLocations);
                if (shipCollision)
                {
                    randomShipsLocations.Clear();
                }
                if (++safetySwitch == 100)
                {
                    throw new Exception($"Getting random ships locations failed after {safetySwitch} attempts!");
                }
            } 
            while (shipCollision);

            return randomShipsLocations;
        }
    }
}
