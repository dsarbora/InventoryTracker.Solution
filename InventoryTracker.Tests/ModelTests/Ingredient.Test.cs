using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using InventoryTracker.Models;

namespace InventoryTracker.Tests
{
    [TestClass]
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
            TableOrder.ClearAll();
        }

        [TestMethod]
        public void Save_SavesIngredientToDatabase_IngredientList()
        {
            Ingredient newIngredient = new Ingredient("celery");
            newIngredient.Save();
            List<Ingredient> testList = Ingredient.GetAll();
            List<Ingredient> allIngredients = new List<Ingredient>{newIngredient};
            CollectionAssert.AreEqual(testList, allIngredients);
        }

        [TestMethod]
        public void Find_FindsCorrectIngredient_True()
        {
            Ingredient newIngredient = new Ingredient("celery");
            newIngredient.Save();
            Ingredient newIngredient2 = new Ingredient("carrots");
            newIngredient2.Save();
            Ingredient foundIngredient = Ingredient.Find(newIngredient.GetId());
            Assert.AreEqual(newIngredient, foundIngredient);
        }

        [TestMethod]
        public void Edit_ChangesNameAndQuantity_True()
        {
            Ingredient newIngredient = new Ingredient("celry");
            newIngredient.Save();
            string newName = "celery";
            int newQuantity = 10;
            newIngredient.Edit(newName, newQuantity);
            Ingredient manualIngredient = new Ingredient(newName, newQuantity, newIngredient.GetId());
            Assert.AreEqual(manualIngredient, newIngredient);
        }

        [TestMethod]
        public void Delete_DeletesIngredientFromDatabase_EmptyList()
        {
            Ingredient newIngredient = new Ingredient("celery");
            newIngredient.Save();
            newIngredient.Delete();
            List<Ingredient> allIngredients = new List<Ingredient>{};
            List<Ingredient> testList = Ingredient.GetAll();
            CollectionAssert.AreEqual(allIngredients, testList);
        }
        [TestMethod]
        public void GetShipments_GetsAllIngredientShipments_ShipmentList()
        {
            DateTime orderDate = Convert.ToDateTime("2/26/2019");
            DateTime orderDate2 = Convert.ToDateTime("3/5/2019");
            DateTime orderDate3 = Convert.ToDateTime("3/12/2019");
            Shipment shipment = new Shipment(orderDate);
            Shipment shipment2 = new Shipment(orderDate2);
            Shipment shipment3 = new Shipment(orderDate3);
            shipment.Save();
            shipment2.Save();
            shipment3.Save();
            Ingredient newIngredient = new Ingredient("celery");
            newIngredient.Save();
            shipment.AddIngredient(newIngredient.GetId());
            shipment2.AddIngredient(newIngredient.GetId());
            List<Shipment> shipmentsWithIngredient = new List<Shipment>{shipment, shipment2};
            List<Shipment> testList = newIngredient.GetShipments();
            CollectionAssert.AreEqual(shipmentsWithIngredient, testList);
        }

        [TestMethod]
        public void GetShipments_OrdersByDate_ShipmentList()
        {
            DateTime orderDate = Convert.ToDateTime("2/26/2019");
            DateTime orderDate2 = Convert.ToDateTime("3/5/2019");
            DateTime orderDate3 = Convert.ToDateTime("3/12/2019");
            Shipment shipment = new Shipment(orderDate);
            Shipment shipment2 = new Shipment(orderDate2);
            Shipment shipment3 = new Shipment(orderDate3);
            shipment.Save();
            shipment2.Save();
            shipment3.Save();
            Ingredient newIngredient = new Ingredient("celery");
            newIngredient.Save();
            shipment.AddIngredient(newIngredient.GetId());
            shipment2.AddIngredient(newIngredient.GetId());
            List<Shipment> shipmentsWithIngredient = new List<Shipment>{shipment2, shipment};
            List<Shipment> testList = newIngredient.GetShipments();
            CollectionAssert.AreNotEqual(shipmentsWithIngredient, testList);
        }

        public void GetDishes_ReturnsIngredientDishes_DishList()
        {
            Ingredient newIngredient = new Ingredient("celery");
            Dish newDish = new Dish("stew");
            newIngredient.Save();
            newDish.Save();
            newDish.AddIngredient(newIngredient.GetId(), 5);
            Dish otherDish = new Dish("beans and rice");
            otherDish.Save();
            List<Dish> allCeleryDish = new List<Dish>{newDish};
            List<Dish> testList = newIngredient.GetDishes();
            CollectionAssert.AreEqual(allCeleryDish, testList);
        }
    }
}