using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using InventoryTracker.Controllers;
using InventoryTracker.Models;

namespace InventoryTracker.Tests
{
    [TestClass]
    public class DishesControllerTest : IDisposable
    {
        public DishesControllerTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root; password=root;port=8889;database=restaurant_test;";
        }
        public void Dispose()
        {
            Ingredient.ClearAll();
            Dish.ClearAll();
            Shipment.ClearAll();
            TableOrder.ClearAll();
        }
        [TestMethod]
        public void Index_ReturnsViewResult_ViewResult()
        {
            DishesController controller = new DishesController();
            ViewResult indexView = controller.Index() as ViewResult;
            Assert.IsInstanceOfType(indexView, typeof(ViewResult));
        }
        [TestMethod]
        public void Index_HasCorrectModelType_DishList()
        {
            DishesController controller = new DishesController();
            ViewResult indexView = controller.Index() as ViewResult;
            var result = indexView.ViewData.Model;
            Assert.IsInstanceOfType(result, typeof(List<Dish>));
        }
        [TestMethod]
        public void DishesNew_ReturnsViewResult_True()
        {
            DishesController controller = new DishesController();
            ViewResult newView = controller.New() as ViewResult;
            Assert.IsInstanceOfType(newView, typeof(ViewResult));
        }
        [TestMethod]
        public void Edit_ReturnsViewResult_True()
        {
            Dish dish = new Dish("spaghetti and meatballs");
            DishesController controller = new DishesController();
            ViewResult editView = controller.Edit(dish.GetId()) as ViewResult;
            Assert.IsInstanceOfType(editView, typeof(ViewResult));
        }
        [TestMethod]
        public void Edit_HasCorrectModelType_Dish()
        {
            Dish dish = new Dish("spaghetti and meatballs");
            DishesController controller = new DishesController();
            ViewResult editView = controller.Edit(dish.GetId()) as ViewResult;
            var result = editView.ViewData.Model;
            Assert.IsInstanceOfType(result, typeof(Dish));
        }
        [TestMethod]
        public void Show_ReturnViewResult_True()
        {
            Dish dish = new Dish("spaghetti and meatballs");
            DishesController controller = new DishesController();
            ViewResult showView = controller.Show(dish.GetId()) as ViewResult;
            Assert.IsInstanceOfType(showView, typeof(ViewResult));
        }
        [TestMethod]
        public void Show_HasCorrectModelType_DictionaryStringObject()
        {
            Dish dish = new Dish("spaghetti and meatballs");
            DishesController controller = new DishesController();
            ViewResult showView = controller.Show(dish.GetId()) as ViewResult;
            var result = showView.ViewData.Model;
            Assert.IsInstanceOfType(result, typeof(Dictionary<string, object>));
        }
    }
}
