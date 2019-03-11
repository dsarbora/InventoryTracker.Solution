using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using InventoryTracker;
using InventoryTracker.Models;

namespace InventoryTracker.Tests
{
    public class ShipmentsControllerTest : IDisposable
    {
        public void Dispose()
        {
            Ingredient.ClearAll();
            Dish.ClearAll();
            Shipment.ClearAll();
            Order.ClearAll();
        }
    }
}