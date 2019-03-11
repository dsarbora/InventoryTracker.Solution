using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Sytem.Collections.Generic;
using InventoryTracker.Models;

namespace InventoryTracker.Tests
{
    public class OrderTest : IDisposable
    {
        public OrderTest()
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
    }
}