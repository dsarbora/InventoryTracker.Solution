using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using InventoryTracker.Controllers;
using InventoryTracker.Models;

namespace InventoryTracker.Tests
{
    [TestClass]
    public class IngredientsControllerTest : IDisposable
    {
        public IngredientsControllerTest()
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
        public void Index_ReturnsViewResult_True()
        {
            IngredientsController controller = new IngredientsController();
            ViewResult indexView = controller.Index() as ViewResult;
            Assert.IsInstanceOfType(indexView, typeof(ViewResult));
        }

        [TestMethod]
        public void Index_HasCorrectModelType_IngredientList()
        {
            IngredientsController controller = new IngredientsController();
            ViewResult indexView = controller.Index() as ViewResult;
            var result = indexView.ViewData.Model;
            Assert.IsInstanceOfType(result, typeof(List<Ingredient>));

        }

        [TestMethod]
        public void New_ReturnsViewResult_True()
        {
            IngredientsController controller = new IngredientsController();
            ViewResult newView = controller.New() as ViewResult;
            Assert.IsInstanceOfType(newView, typeof(ViewResult));
        }

        [TestMethod]
        public void Show_ReturnsViewResult_True()
        {
            IngredientsController controller = new IngredientsController();
            Ingredient newIngredient = new Ingredient("celery", 5);
            newIngredient.Save();
            int id = newIngredient.GetId();
            ViewResult showView = controller.Show(id) as ViewResult;
            Assert.IsInstanceOfType(showView, typeof(ViewResult));
        }

        [TestMethod]
        public void Show_HasCorrectModelType_DictionaryStringObject()
        {
            IngredientsController controller = new IngredientsController();
            Ingredient newIngredient = new Ingredient("celery", 5);
            newIngredient.Save();
            int id = newIngredient.GetId();
            ViewResult showView = controller.Show(id) as ViewResult;
            var result = showView.ViewData.Model;
            Assert.IsInstanceOfType(result, typeof(Dictionary<string, object>));
        }



    }
}
