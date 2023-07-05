using BattleshipGameCore;
using BattleshipGameCore.Configuration;
using BattleshipGameCore.Interfaces;
using BattleshipGameCore.Utilities;

namespace Battleship.Test
{
    [TestClass]
    public class ShipsRandomLocationProviderTests
    {
        [TestMethod]
        public void CheckShipsLocationCountAndSizes()
        {
            ShipsRandomLocationProvider provider = new ShipsRandomLocationProvider();
            IShipsConfiguration shipsConfiguration = new ShipsConfiguration(new List<int>() { 3, 4, 5 });
            IGameConfiguration gameConfiguration = new GameConfiguration(10, 10);

            List<List<Coordinates>> result = provider.GetShipsLocations(gameConfiguration, shipsConfiguration);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(3, result[0].Count);
            Assert.AreEqual(4, result[1].Count);
            Assert.AreEqual(5, result[2].Count);
        }
    }
}