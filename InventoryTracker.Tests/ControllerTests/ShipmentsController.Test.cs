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
            Shipment.ClearAll();
            TableOrder.ClearAll();
        }

        [TestMethod]
        public void Index_ReturnsViewResult_True()
        {
            ShipmentsController controller = new ShipmentsController();
            ViewResult indexView = controller.Index() as ViewResult;
            Assert.IsInstanceOfType(indexView, typeof(ViewResult));
        }
        [TestMethod]
        public void Index_HasCorrectModelType_ShipmentList()
        {
            ShipmentsController controller = new ShipmentsController();
            ViewResult indexView = controller.Index() as ViewResult;
            var result = indexView.ViewData.Model;
            Assert.IsInstanceOfType(result, typeof(List<Shipment>));
        }
        [TestMethod]
        public void NewView_ReturnsViewResult_True()
        {
            ShipmentsController controller = new ShipmentsController();
            ViewResult newView = controller.New() as ViewResult;
            Assert.IsInstanceOfType(newView, typeof(ViewResult));
        }
        [TestMethod]
        public void EditView_ReturnsViewResult_True()
        {
            Shipment shipment = new Shipment(DateTime.Now);
            ShipmentsController controller = new ShipmentsController();
            ViewResult editView = controller.Edit(shipment.GetId()) as ViewResult;
            Assert.IsInstanceOfType(editView, typeof(ViewResult));
        }
        [TestMethod]
        public void EditView_HasCorrectModelType_Shipment()
        {
            Shipment shipment = new Shipment(DateTime.Now);
            ShipmentsController controller = new ShipmentsController();
            ViewResult editView = controller.Edit(shipment.GetId()) as ViewResult;
            var result = editView.ViewData.Model;
            Assert.IsInstanceOfType(result, typeof(Shipment));
        }
        [TestMethod]
        public void ShowView_ReturnsViewResult_True()
        {
            Shipment shipment = new Shipment(DateTime.Now);
            ShipmentsController controller = new ShipmentsController();
            ViewResult showView = controller.Show(shipment.GetId()) as ViewResult;
            Assert.IsInstanceOfType(showView, typeof(ViewResult));
        }
        [TestMethod]
        public void ShowView_HasCorrectModelType_DictionaryStringObject()
        {
            Shipment shipment = new Shipment(DateTime.Now);
            ShipmentsController controller = new ShipmentsController();
            ViewResult showView = controller.Show(shipment.GetId()) as ViewResult;
            var result = showView.ViewData.Model;
            Assert.IsInstanceOfType(result, typeof(Dictionary<string, object>));
        }
    }

}