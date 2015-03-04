using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CrusadeOfMasymUnitTests
{
    [TestClass]
    public class UtilsTests
    {
        [TestMethod]
        public void TestIsAdjacent()
        {
            Assert.IsTrue(Utils.IsAdjacent(new MapPosition(0, 1), new MapPosition(1, 1)));
            Assert.IsFalse(Utils.IsAdjacent(new MapPosition(1, 1), new MapPosition(1, 1)));
            Assert.IsFalse(Utils.IsAdjacent(new MapPosition(0, 1), new MapPosition(2, 1)));

            Assert.IsTrue(Utils.IsAdjacent(new MapPosition(0, 0), new MapPosition(0, 1)));
            Assert.IsTrue(Utils.IsAdjacent(new MapPosition(0, 0), new MapPosition(1, 0)));
            Assert.IsTrue(Utils.IsAdjacent(new MapPosition(0, 0), new MapPosition(1, 1)));
            Assert.IsFalse(Utils.IsAdjacent(new MapPosition(0, 0), new MapPosition(1, 2)));

            Assert.IsTrue(Utils.IsAdjacent(new MapPosition(1, 1), new MapPosition(2, 1)));
            Assert.IsFalse(Utils.IsAdjacent(new MapPosition(1, 1), new MapPosition(2, 2)));

            Assert.IsFalse(Utils.IsAdjacent(new MapPosition(0, 4), new MapPosition(2, 3)));
        }
    }
}
