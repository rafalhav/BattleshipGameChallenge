using BattleshipGameCore;
using BattleshipGameCore.Configuration;
using BattleshipGameCore.Interfaces;
using BattleshipGameCore.Utilities;
using Moq;

namespace Battleship.Test
{
    [TestClass]
    public class BoardTests
    {
        IGameConfiguration gameConfiguration = new GameConfiguration(10, 10);

        [TestMethod]
        public void CheckBoardInitialization()
        {

            Board board = new Board(gameConfiguration);

            Assert.AreEqual(FieldStateEnum.NotCheckedYet, board.GetFieldStateByCoordinates(new Coordinates(1, 1)));
            Assert.AreEqual(FieldStateEnum.NotCheckedYet, board.GetFieldStateByCoordinates(new Coordinates(1, 10)));
            Assert.AreEqual(FieldStateEnum.NotCheckedYet, board.GetFieldStateByCoordinates(new Coordinates(10, 1)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => board.GetFieldStateByCoordinates(new Coordinates(11, 1)));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => board.GetFieldStateByCoordinates(new Coordinates(1, 11)));
        }

        [TestMethod]
        public void BoardShipsPlacedCorrectly()
        {
            Board board = new Board(gameConfiguration);

            List<Coordinates> ship1Coordinates = new List<Coordinates>()
            {
                new Coordinates(1, 1),
                new Coordinates(2, 1),
                new Coordinates(3, 1),
                new Coordinates(4, 1)
            };
            List<Coordinates> ship2Coordinates = new List<Coordinates>()
            {
                new Coordinates(5, 5),
                new Coordinates(5, 6),
                new Coordinates(5, 7),
                new Coordinates(5, 8)
            };

            List<List<Coordinates>> shipsLocation = new List<List<Coordinates>>();
            shipsLocation.Add(ship1Coordinates);
            shipsLocation.Add(ship2Coordinates);

            Mock<IShipsLocationProvider> shipsLocationProviderMock = new Mock<IShipsLocationProvider>();
            shipsLocationProviderMock
                .Setup(s => s.GetShipsLocations(It.IsAny<IGameConfiguration>(), It.IsAny<IShipsConfiguration>()))
                .Returns(shipsLocation);
            IShipsConfiguration shipsConfiguration = new ShipsConfiguration(new List<int> { 4, 4 });

            board.PlaceShips(shipsConfiguration, shipsLocationProviderMock.Object);

            for (int columnIndex = 1; columnIndex <= 10; columnIndex++)
            {
                for (int rowIndex = 1; rowIndex <= 10; rowIndex++)
                {
                    Coordinates coordinates = new Coordinates(columnIndex, rowIndex);
                    board.CheckField(coordinates);
                    if (ship1Coordinates.Any(c => c.CoordinateString == coordinates.CoordinateString)
                        || ship2Coordinates.Any(c => c.CoordinateString == coordinates.CoordinateString))
                    {
                        bool isHitOrSunk =
                            board.GetFieldStateByCoordinates(coordinates) == FieldStateEnum.Hit
                            || board.GetFieldStateByCoordinates(coordinates) == FieldStateEnum.Sunk;
                        Assert.IsTrue(isHitOrSunk);
                    }
                    else
                    {
                        Assert.AreEqual(FieldStateEnum.Miss, board.GetFieldStateByCoordinates(coordinates));
                    }
                }
            }
        }

        [TestMethod]
        public void BoardSetSunkStateCorrectly()
        {
            Board board = new Board(gameConfiguration);

            List<Coordinates> ship1Coordinates = new List<Coordinates>()
            {
                new Coordinates(4, 6),
                new Coordinates(5, 6)
            };

            List<List<Coordinates>> shipsLocation = new List<List<Coordinates>>();
            shipsLocation.Add(ship1Coordinates);

            Mock<IShipsLocationProvider> shipsLocationProviderMock = new Mock<IShipsLocationProvider>();
            shipsLocationProviderMock
                .Setup(s => s.GetShipsLocations(It.IsAny<IGameConfiguration>(), It.IsAny<IShipsConfiguration>()))
                .Returns(shipsLocation);
            IShipsConfiguration shipsConfiguration = new ShipsConfiguration(new List<int> { 2 });

            board.PlaceShips(shipsConfiguration, shipsLocationProviderMock.Object);

            Assert.AreEqual(FieldStateEnum.NotCheckedYet, board.GetFieldStateByCoordinates(new Coordinates(4, 6)));
            Assert.AreEqual(FieldStateEnum.NotCheckedYet, board.GetFieldStateByCoordinates(new Coordinates(5, 6)));

            board.CheckField(new Coordinates(4, 6));

            Assert.AreEqual(FieldStateEnum.Hit, board.GetFieldStateByCoordinates(new Coordinates(4, 6)));
            Assert.AreEqual(FieldStateEnum.NotCheckedYet, board.GetFieldStateByCoordinates(new Coordinates(5, 6)));

            board.CheckField(new Coordinates(5, 6));

            Assert.AreEqual(FieldStateEnum.Sunk, board.GetFieldStateByCoordinates(new Coordinates(4, 6)));
            Assert.AreEqual(FieldStateEnum.Sunk, board.GetFieldStateByCoordinates(new Coordinates(5, 6)));
        }

        [TestMethod]
        public void BoardDetectAllShipsSunkCorrectly()
        {
            Board board = new Board(gameConfiguration);

            List<Coordinates> ship1Coordinates = new List<Coordinates>()
            {
                new Coordinates(4, 6),
                new Coordinates(5, 6)
            };

            List<List<Coordinates>> shipsLocation = new List<List<Coordinates>>();
            shipsLocation.Add(ship1Coordinates);

            Mock<IShipsLocationProvider> shipsLocationProviderMock = new Mock<IShipsLocationProvider>();
            shipsLocationProviderMock
                .Setup(s => s.GetShipsLocations(It.IsAny<IGameConfiguration>(), It.IsAny<IShipsConfiguration>()))
                .Returns(shipsLocation);
            IShipsConfiguration shipsConfiguration = new ShipsConfiguration(new List<int> { 2 });

            board.PlaceShips(shipsConfiguration, shipsLocationProviderMock.Object);

            Assert.IsFalse(board.AreAllShipsSunk());

            board.CheckField(new Coordinates(4, 6));

            Assert.IsFalse(board.AreAllShipsSunk());

            board.CheckField(new Coordinates(5, 6));

            Assert.IsTrue(board.AreAllShipsSunk());
        }
    }
}