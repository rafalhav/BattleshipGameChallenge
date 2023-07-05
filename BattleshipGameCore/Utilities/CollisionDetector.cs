using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGameCore.Utilities
{
    public static class CollisionDetector
    {
        public static bool AreShipsColliding(List<List<Coordinates>> shipsCoordinates) 
        {
            List<Coordinates> flatCoordinateList = new List<Coordinates>();
            
            foreach (var shipCoordinates in shipsCoordinates) 
            {
                flatCoordinateList.AddRange(shipCoordinates);
            }

            foreach(var shipCoordinates in flatCoordinateList) 
            {
                if(flatCoordinateList.Where(x => x.CoordinateString == shipCoordinates.CoordinateString).Count() > 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
