using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using InventoryTracker.Models;

namespace InventoryTracker.Tests
{
    [TestClass]
    public class ShipmentTest : IDisposable
    {
        public ShipmentTest()
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
        public void Find_GetsById_Shipment()
        {
            DateTime orderDate = Convert.ToDateTime("3/12/2019");
            Shipment shipment = new Shipment(orderDate);
            shipment.Save();
            Shipment newShipment = Shipment.Find(shipment.GetId());
            Assert.AreEqual(shipment, newShipment);            
        }

        [TestMethod]
        public void ShipmentConstructor_AssignsDate_DateTime()
        {
            DateTime orderDate = Convert.ToDateTime("3/12/2019");
            Shipment shipment = new Shipment(orderDate);
            shipment.Save();
            Assert.AreEqual(orderDate, shipment.GetDate());
        }
        [TestMethod]
        public void GetAll_GetsAllShipments_ShipmentList()
        {
            DateTime orderDate = Convert.ToDateTime("3/5/2019");
            DateTime orderDate2 = Convert.ToDateTime("3/12/2019");
            Shipment shipment = new Shipment(orderDate);
            shipment.Save();
            Shipment shipment2 = new Shipment(orderDate);
            shipment2.Save();
            List<Shipment> allShipments = new List<Shipment>{shipment, shipment2};
            List<Shipment> testList = Shipment.GetAll();
            CollectionAssert.AreEqual(allShipments, testList);
        }

        [TestMethod]
        public void AddIngredient_AddsIngredientToShipment_IngredientList()
        {
            DateTime orderDate = Convert.ToDateTime("3/12/2019");
            Shipment shipment = new Shipment(orderDate);
            shipment.Save();
            Ingredient newIngredient = new Ingredient("celery");
            newIngredient.Save();
            Ingredient newIngredient2 = new Ingredient("carrots");
            newIngredient2.Save();
            shipment.AddIngredient(newIngredient.GetId(), 5);
            shipment.AddIngredient(newIngredient2.GetId(), 5);
            List<IngredientQuantity> allIngredients = new List<IngredientQuantity>{new IngredientQuantity(newIngredient, 5), new IngredientQuantity(newIngredient2, 5)};
            List<IngredientQuantity> testList = shipment.GetAllIngredients();
            Console.WriteLine("{0} {1}", allIngredients[0].GetQuantity(), testList[0].GetQuantity());
            CollectionAssert.AreEquivalent (allIngredients, testList);
        }

        [TestMethod]
        public void GetShipmentIngredients_GetsIngredients_IngredientList()
        {
            DateTime orderDate = Convert.ToDateTime("3/12/2019");
            Shipment shipment = new Shipment(orderDate);
            shipment.Save();
            Ingredient newIngredient = new Ingredient("celery");
            newIngredient.Save();
            Ingredient newIngredient2 = new Ingredient("carrots");
            newIngredient2.Save();
            IngredientQuantity celery = new IngredientQuantity(newIngredient, 5);
            IngredientQuantity carrots = new IngredientQuantity(newIngredient2, 5);
            shipment.AddIngredient(newIngredient.GetId(), 5);
            shipment.AddIngredient(newIngredient2.GetId(), 5);
            List<IngredientQuantity> allIngredients = new List<IngredientQuantity>{carrots, celery};
            List<IngredientQuantity> testList = shipment.GetAllIngredients();
            foreach(IngredientQuantity ing in allIngredients)
            {
                Console.WriteLine("allingredients {0} type {1} ingredient {2} quantity {3}", allIngredients.Count, ing.GetIngredient().GetType(), ing.GetIngredient().GetName(), ing.GetQuantity());
            }
            foreach(IngredientQuantity ing in testList)
            {
                Console.WriteLine("testList {0} type {1} ingredient {2} quantity {3}", testList.Count, ing.GetIngredient().GetType(), ing.GetIngredient().GetName(), ing.GetQuantity());
            }
            CollectionAssert.AreEqual (allIngredients, testList);
        }

        [TestMethod]
        public void DeleteIngredients_DeletesIngredientFromShipment_EmptyList()
        {
            DateTime orderDate = Convert.ToDateTime("3/12/2019");
            Shipment shipment = new Shipment(orderDate);
            shipment.Save();
            Ingredient newIngredient = new Ingredient("celery");
            newIngredient.Save();
            shipment.AddIngredient(newIngredient.GetId(), 5);
            shipment.DeleteIngredient(newIngredient.GetId());
            List<IngredientQuantity> allIngredients = new List<IngredientQuantity>{};
            List<IngredientQuantity> testList = shipment.GetAllIngredients();
            CollectionAssert.AreEqual(testList, allIngredients);
        }

        [TestMethod]
        public void GetPotentialIngredients_GetsOnlyPotentialIngredients_IngredientList()
        {
            Shipment shipment = new Shipment(Convert.ToDateTime("3/12/2019"));
            Ingredient one = new Ingredient("celery");
            Ingredient two = new Ingredient("carrots");
            Ingredient three = new Ingredient("juice");
            Ingredient four = new Ingredient("carrot juice");
            shipment.Save(); 
            one.Save(); 
            two.Save(); 
            three.Save(); 
            four.Save();
            shipment.AddIngredient(one.GetId(), 5);
            shipment.AddIngredient(two.GetId(), 5);
            List<Ingredient> potentialIngredients = new List<Ingredient>{three, four};
            List<Ingredient> testList = shipment.GetPotentialIngredients();
            CollectionAssert.AreEquivalent(potentialIngredients, testList);


            
        }

    }
}