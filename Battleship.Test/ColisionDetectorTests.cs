using BattleshipGameCore;
using BattleshipGameCore.Utilities;

namespace Battleship.Test
{
    [TestClass]
    public class CollisionDetectorTests
    {
        [TestMethod]
        public void NonColliding()
        {
            List<Coordinates> coordinatesShip1 = new List<Coordinates>()
            {
                new Coordinates(3,4),
                new Coordinates(4,4),
                new Coordinates(5,4),
                new Coordinates(6,4),
                new Coordinates(7,4)
            };

            List<Coordinates> coordinatesShip2 = new List<Coordinates>()
            {
                new Coordinates(8,2),
                new Coordinates(8,3),
                new Coordinates(8,4),
                new Coordinates(8,5),
                new Coordinates(8,6)
            };

            var result = CollisionDetector.AreShipsColliding(new List<List<Coordinates>> { coordinatesShip1, coordinatesShip2 });
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CrossingCollision()
        {
            List<Coordinates> coordinatesShip1 = new List<Coordinates>()
            {
                new Coordinates(3,4),
                new Coordinates(4,4),
                new Coordinates(5,4),
                new Coordinates(6,4),
                new Coordinates(7,4)
            };

            List<Coordinates> coordinatesShip2 = new List<Coordinates>()
            {
                new Coordinates(5,2),
                new Coordinates(5,3),
                new Coordinates(5,4),
                new Coordinates(5,5),
                new Coordinates(5,6)
            };

            var result = CollisionDetector.AreShipsColliding(new List<List<Coordinates>> { coordinatesShip1, coordinatesShip2 });
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BothInTheSameRowColliding()
        {
            List<Coordinates> coordinatesShip1 = new List<Coordinates>()
            {
                new Coordinates(1,4),
                new Coordinates(2,4),
                new Coordinates(3,4),
                new Coordinates(4,4),
                new Coordinates(5,4)
            };

            List<Coordinates> coordinatesShip2 = new List<Coordinates>()
            {
                new Coordinates(4,4),
                new Coordinates(5,4),
                new Coordinates(6,4),
                new Coordinates(7,4),
                new Coordinates(8,4)
            };

            var result = CollisionDetector.AreShipsColliding(new List<List<Coordinates>> { coordinatesShip1, coordinatesShip2 });
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BothInTheSameColumnColliding()
        {
            List<Coordinates> coordinatesShip1 = new List<Coordinates>()
            {
                new Coordinates(5,4),
                new Coordinates(5,5),
                new Coordinates(5,6),
                new Coordinates(5,7),
                new Coordinates(5,8)
            };

            List<Coordinates> coordinatesShip2 = new List<Coordinates>()
            {
                new Coordinates(5,6),
                new Coordinates(5,7),
                new Coordinates(5,8),
                new Coordinates(5,9),
                new Coordinates(5,10)
            };

            var result = CollisionDetector.AreShipsColliding(new List<List<Coordinates>> { coordinatesShip1, coordinatesShip2 });
            Assert.IsTrue(result);
        }
    }
}