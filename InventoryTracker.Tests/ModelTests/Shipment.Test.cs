using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using InventoryTracker.Models;

namespace InventoryTracker.Tests
{
    [TestClass]
    public class ShipmentTest : IDisposable
    {
        public ShipmentTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root; password=root;port=8889;database=restaurant_test;";
        }
        public void Dispose()
        {
            Ingredient.ClearAll();
            Dish.ClearAll();
            Shipment.ClearAll();
            Order.ClearAll();
        }

        [TestMethod]
        public void Find()
        {
            DateTime orderDate = Convert.ToDateTime("3/12/2019");
            Shipment shipment = new Shipment(orderDate);
            shipment.Save();
            Shipment newShipment = Shipment.Find(shipment.GetId());
            Assert.AreEqual(shipment, newShipment);


            
        }
    }
}