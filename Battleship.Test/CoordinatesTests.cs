using BattleshipGameCore;
using BattleshipGameCore.Utilities;

namespace Battleship.Test
{
    [TestClass]
    public class CoordinatesTests
    {
        [TestMethod]
        public void GetCoordinatesString()
        {
            Coordinates coordinates = new Coordinates(1, 1);
            Assert.AreEqual("A1", coordinates.CoordinateString);

            coordinates = new Coordinates(5, 7);
            Assert.AreEqual("E7", coordinates.CoordinateString);
        }
    }
}