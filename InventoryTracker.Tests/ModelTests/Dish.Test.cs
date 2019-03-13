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
            TableOrder.ClearAll();
        }
        [TestMethod]
        public void GetName_ReturnsName_String()
        {
            string name = "Eggs and bacon";
            int id = 0;
            Dish newDish = new Dish(name, id);
            string result = newDish.GetName();
            Assert.AreEqual(result, name);
        }

        [TestMethod]
        public void GetId_ReturnsId_Int()
        {
            string name = "Eggs and bacon";
            int id = 0;
            Dish newDish = new Dish(name, id);
            int result = newDish.GetId();
            Assert.AreEqual(result, id);
        }

        [TestMethod]
        public void Equals_ChecksIfDishesAreTheSame_Ture()
        {
            string name = "Eggs and bacon";
            int id = 1;
            Dish firstDish = new Dish(name, id);
            Dish secondDish = new Dish(name, id);
            Assert.AreEqual(firstDish, secondDish);
        }

        [TestMethod]
        public void Save_SavesDishToDatabase_Dish()
        {
            string name = "Eggs and bacon";
            int id = 1;
            Dish newDish = new Dish(name, id);
            newDish.Save();

            List<Dish> testList = new List<Dish>{newDish};
            List<Dish> result = Dish.GetAll();
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Find_FindsDishInDatabase_Dish()
        {
            string name = "Eggs and bacon";
            Dish newDish = new Dish(name);
            newDish.Save();
            int id = newDish.GetId();

            Dish testDish = Dish.Find(id);
            Assert.AreEqual(newDish, testDish);
        }

        [TestMethod]
        public void Edit_EditsDishInDatabase_Dish()
        {
            string name = "Eggs and bacon";
            Dish newDish = new Dish(name);
            newDish.Save();

            string newName = "Eggs";
            newDish.Edit(newName);

            Dish testDish = Dish.GetAll()[0];
            Assert.AreEqual(newDish, testDish);
        }

        [TestMethod]
        public void Delete_DeletesDishFromDatabase_EmptyList()
        {
            string name = "Eggs";
            Dish newDish = new Dish(name);
            newDish.Save();
            newDish.Delete();
            List<Dish> testList = new List<Dish> {};
            List<Dish> result = Dish.GetAll();
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void AddIngredient_AddsIngredientToDish_IngredientList()
        {
            Ingredient newIngredient = new Ingredient("celery");
            newIngredient.Save();
            Dish newDish = new Dish("stew");
            newDish.Save();
            newDish.AddIngredient(newIngredient.GetId(), 5);
            List<Ingredient> allIngredients = new List<Ingredient>{newIngredient};
            List<Ingredient> testList = newDish.AllIngredients();
            CollectionAssert.AreEqual(allIngredients, testList);
        }

        [TestMethod]
        public void DeleteIngredient_DeletesIngredientFromDish_EmptyList()
        {
            Ingredient newIngredient = new Ingredient("celery");
            newIngredient.Save();
            Dish newDish = new Dish("stew");
            newDish.Save();
            newDish.AddIngredient(newIngredient.GetId(), 5);
            newDish.DeleteIngredient(newIngredient.GetId());
            List<Ingredient> allIngredients = new List<Ingredient>{newIngredient};
            List<Ingredient> testList = newDish.AllIngredients();
            CollectionAssert.AreEqual(allIngredients, testList);
        }

    }
}