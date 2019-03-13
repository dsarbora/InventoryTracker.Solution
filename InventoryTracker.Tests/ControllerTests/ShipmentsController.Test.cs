using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using InventoryTracker.Controllers;
using InventoryTracker.Models;

namespace InventoryTracker.Tests
{
    [TestClass]
    public class ShipmentsControllerTest : IDisposable
    {
        public void Dispose()
        {
            Ingredient.ClearAll();
            Dish.ClearAll();
           // Shipment.ClearAll();
           // Order.ClearAll();
        }
        [TestMethod]
        public void Index_HasCorrectModelType_ShipmentList()
        {
            ShipmentsController controller = new ShipmentsController();
            ViewResult indexView = controller.Index() as ViewResult;
            var result = indexView.ViewData.Model;
            Assert.IsInstanceOfType(result, typeof(List<Shipment>));
        }
    }

}