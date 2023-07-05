using System.Reflection;
using System.Runtime.CompilerServices;
using BattleshipGameCore.Configuration;
using BattleshipGameCore.Enums;
using BattleshipGameCore.Interfaces;
using BattleshipGameCore.Utilities;

namespace BattleshipGameCore
{
    public class Board
    {
        private int _rowCount;

        private int _columnCount;

        private IGameConfiguration _gameConfiguration;
        private Dictionary<string ,Field> _fields;

        public Board(IGameConfiguration gameConfiguration)
        {
            this._gameConfiguration = gameConfiguration;
            this._rowCount = gameConfiguration.RowCount;
            this._columnCount = gameConfiguration.ColumnCount;
            InitializeFields();
        }

        public void PlaceShips(IShipsConfiguration shipsConfiguration, IShipsLocationProvider shipsLocationProvider)
        {
            this.InitializeFields();
            this.PlaceShipsAtGivenLocations(shipsLocationProvider.GetShipsLocations(this._gameConfiguration, shipsConfiguration));
        }

        public void CheckField(Coordinates coordinates)
        {
            Field field = this.GetFieldByCoordinates(coordinates);
            if(field.FieldState==FieldStateEnum.NotCheckedYet)
            {
                FieldStateEnum checkResult = field.DiscoverFieldState();
                if (checkResult == FieldStateEnum.Hit)
                {
                    this.CheckForSunkShip(field.ShipId);
                }
            }
        }

        public bool AreAllShipsSunk()
        {
            if (this._fields.Any(field => field.Value.ShipId > 0 && field.Value.FieldState != FieldStateEnum.Sunk))
            {
                return false;
            }
            return true;
        }

        public FieldStateEnum GetFieldStateByCoordinates(Coordinates coordinates)
        {
            return this.GetFieldByCoordinates(coordinates).FieldState;
        }


        private void InitializeFields()
        {
            this._fields = new Dictionary<string, Field>();
            for (int columnIndex = 1; columnIndex <= this._columnCount; columnIndex++)
            {
                for (int rowIndex = 1; rowIndex <= this._rowCount; rowIndex++)
                {
                    Coordinates coordinates = new Coordinates(columnIndex, rowIndex);
                    this._fields.Add(coordinates.CoordinateString, new Field());
                }
            }
        }

        private Field GetFieldByCoordinates(Coordinates coordinates)
        {
            if (this._fields.ContainsKey(coordinates.CoordinateString))
            {
                return this._fields[coordinates.CoordinateString];
            }
            throw new ArgumentOutOfRangeException($"Board doesn't contain field with provided coordinate: [{coordinates.CoordinateString}] !");
        }

        private void PlaceShipsAtGivenLocations(List<List<Coordinates>> shipsCoordinates)
        {
            int shipId = 1;
            foreach (var shipCoordinates in shipsCoordinates)
            {
                foreach (var coordinates in shipCoordinates)
                {
                    this. GetFieldByCoordinates(coordinates).ShipId = shipId;
                }
                shipId++;
            }
        }

        private void CheckForSunkShip(int shipId)
        {
            if (shipId <= 0) return;
            var shipFields = this._fields.Where(field=>field.Value.ShipId == shipId);
            if (shipFields.All(field => field.Value.FieldState == FieldStateEnum.Hit))
            {
                shipFields.ToList().ForEach(Field=>Field.Value.FieldState = FieldStateEnum.Sunk);
            }

        }

        
    }
}