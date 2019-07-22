using System;
using Alea_Belli.Core.Network;
using Alea_Belli.Core.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alea_Belli.Test
{
    [TestClass]
    public class UnitTestReferenceData
    {
        private SinglePlayerAleaBelliGame game = new SinglePlayerAleaBelliGame();

        [TestMethod]
        public void TestReferenceData()
        {
        }

        [TestMethod]
        public void TestLoadNations()
        {
            ReferenceDataPersistence.LoadNations(game);
            Assert.IsTrue(game.AllNations.Count > 0, "No nations");

        }
    }
}
