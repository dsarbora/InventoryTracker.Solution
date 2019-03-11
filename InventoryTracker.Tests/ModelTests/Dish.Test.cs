using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using InventoryTracker.Models;

namespace InventoryTracker.Tests
{
    [TestClass]
    public class DishTest : IDisposable
    {
        public DishTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root; password=root;port=8889;database=restaurant_test;";
        }
        public void Dispose()
        {
            Ingredient.ClearAll();
            Dish.ClearAll();
            //Shipment.ClearAll();
            //Order.ClearAll();
        }
        [TestMethod]
        public void GetName_ReturnsName_String()
        {
            string name = "Eggs and bacon";
            Dish newDish = new Dish(name);
            string result = newDish.GetName();
            Assert.Equals(result, name);
        }
    }
}