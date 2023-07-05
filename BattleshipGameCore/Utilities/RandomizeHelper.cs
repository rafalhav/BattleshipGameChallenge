using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BattleshipGameCore.Enums;

namespace BattleshipGameCore.Utilities
{
    public static class RandomizeHelper
    {
        public static List<Coordinates> GetRandomCoordinates(int shipSize, int columnCount, int rowCount)
        {
            ShipOrientationEnum shipOrientation = GetRandomShipOrientation();
            if (shipOrientation == ShipOrientationEnum.Horizontal)
            {
                return GetRandomHorizontalCoordinates(shipSize, columnCount, rowCount);
            }
            else if (shipOrientation == ShipOrientationEnum.Vertical)
            {
                return GetRandomVerticalCoordinates(shipSize, columnCount, rowCount);
            }
            
            throw new ArgumentOutOfRangeException(nameof(shipOrientation), "Ship orientation provided was incorrect!");
        }

        private static ShipOrientationEnum GetRandomShipOrientation()
        {
            Random r = new Random();
            switch (r.Next(0, 1))
            {
                case 0:
                    return ShipOrientationEnum.Horizontal;
                default:
                    return ShipOrientationEnum.Vertical;
            }
        }

        private static List<Coordinates> GetRandomHorizontalCoordinates(int shipSize, int columnCount, int rowCount)
        {
            List<Coordinates> result = new List<Coordinates>();

            Random r = new Random();
            int rowIndex = r.Next(1, rowCount);
            int startColumnIndex = r.Next(1, columnCount+1-shipSize);
            
            for(int i=0;i<shipSize;i++)
            {
                result.Add(new Coordinates(startColumnIndex + i, rowIndex));
            }

            return result;
        }

        private static List<Coordinates> GetRandomVerticalCoordinates(int shipSize, int columnCount, int rowCount)
        {
            List<Coordinates> result = new List<Coordinates>();

            Random r = new Random();
            int startRowIndex = r.Next(1, rowCount+1-shipSize);
            int columnIndex = r.Next(1, columnCount);

            for (int i = 0; i < shipSize; i++)
            {
                result.Add(new Coordinates(columnIndex, startRowIndex+i));
            }

            return result;
        }

        
    }
}
