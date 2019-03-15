using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using InventoryTracker;
using InventoryTracker.Models;

namespace InventoryTracker.Tests
{
    [TestClass]
    public class OrdersControllerTest : IDisposable
    {
        public OrdersControllerTest()
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
    }
}
