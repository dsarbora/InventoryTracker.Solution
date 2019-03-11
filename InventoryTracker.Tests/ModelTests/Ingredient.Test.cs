using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Sytem.Collections.Generic;
using InventoryTracker.Models;

namespace InventoryTracker.Tests
{
    public class IngredientTest : IDisposable
    {
        public IngredientTest()
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