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
           // Shipment.ClearAll();
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
    }
}