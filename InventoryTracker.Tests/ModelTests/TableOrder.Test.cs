using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using InventoryTracker.Models;

namespace InventoryTracker.Tests
{
    [TestClass]
    public class TableOrderTest : IDisposable
    {
        public TableOrderTest()
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
        public void GetId_ReturnsId_Int()
        {
            int id = 0;
            string tableNumber = "4";
            DateTime orderDate = Convert.ToDateTime("01/01/2019");
            TableOrder newTableOrder = new TableOrder(tableNumber, orderDate, id);
            int result = newTableOrder.GetId();
            Assert.AreEqual(result, id);
        }

        [TestMethod]
        public void GetTableNumber_ReturnsTableNumber_String()
        {
            int id = 0;
            string tableNumber = "4";
            DateTime orderDate = Convert.ToDateTime("01/01/2019");
            TableOrder newTableOrder = new TableOrder(tableNumber, orderDate, id);
            string result = newTableOrder.GetTableNumber();
            Assert.AreEqual(result, tableNumber);
        }

        [TestMethod]
        public void GetTableOrderDate_ReturnsTableOrderDate_DateTime()
        {
            int id = 0;
            string tableNumber = "4"; 
            DateTime orderDate = Convert.ToDateTime("01/01/2019");
            TableOrder newOrder = new TableOrder(tableNumber, orderDate, id);
            DateTime result = newOrder.GetOrderDate();
            Assert.AreEqual(result, orderDate);
        }
        
        [TestMethod]
        public void Equals_ChecksIfTwoOrdersAreTheSame_True()
        {
            string tableNumber = "4";
            DateTime orderDate = Convert.ToDateTime("01/01/2019");
            TableOrder firstOrder = new TableOrder(tableNumber, orderDate);
            TableOrder secondOrder = new TableOrder(tableNumber, orderDate);
            Assert.AreEqual(firstOrder, secondOrder);
        }

        [TestMethod]
        public void Save_SavesOrderToDatabase_Order()
        {
            string tableNumber = "4";
            DateTime orderDate = Convert.ToDateTime("01/01/2019");
            TableOrder newOrder = new TableOrder(tableNumber, orderDate);
            newOrder.Save();

            TableOrder testOrder = TableOrder.GetAll()[0];
            Assert.AreEqual(newOrder, testOrder);
        }

        [TestMethod]
        public void Edit_EditsOrderInDatabase_Order()
        {
            string tableNumber = "4";
            DateTime orderDate = Convert.ToDateTime("01/01/2019");
            TableOrder newOrder = new TableOrder(tableNumber, orderDate);
            newOrder.Save();
            string newTableNumber = "5";
            DateTime newOrderDate = Convert.ToDateTime("02/02/2019");
            newOrder.Edit(newTableNumber, newOrderDate);
            
            TableOrder testOrder = TableOrder.GetAll()[0];
            Assert.AreEqual(newOrder, testOrder);
        }

        [TestMethod]
        public void Find_FindsOrderInDatabase_Order()
        {
            string tableNumber = "4";
            DateTime orderDate = Convert.ToDateTime("01/01/2019");
            TableOrder newOrder = new TableOrder(tableNumber, orderDate);
            newOrder.Save();

            int id = newOrder.GetId();
            TableOrder testOrder = TableOrder.Find(id);
            Assert.AreEqual(newOrder, testOrder);
        }

        [TestMethod]
        public void Delete_DeletesOrderFromDatabase_EmptyList()
        {
            string tableNumber = "4"; 
            DateTime orderDate = Convert.ToDateTime("01/01/2019");
            TableOrder newOrder = new TableOrder(tableNumber, orderDate);
            newOrder.Save();
            newOrder.Delete();
            List<TableOrder> testList = new List<TableOrder>{};
            List<TableOrder> result = TableOrder.GetAll();
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void GetAll_ReturnsAllOrdersFromDatabase_OrderList()
        {
            string firstTableNumber = "4";
            DateTime firstOrderDate = Convert.ToDateTime("01/01/2019");
            TableOrder firstOrder = new TableOrder(firstTableNumber, firstOrderDate);
            firstOrder.Save();
            string secondTableNumber = "5";
            DateTime secondOrderDate = Convert.ToDateTime("02/02/2019");
            TableOrder secondOrder = new TableOrder(secondTableNumber, secondOrderDate);
            secondOrder.Save();

            List<TableOrder> testList = new List<TableOrder> {firstOrder, secondOrder};
            List<TableOrder> result = TableOrder.GetAll();
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void GetNumberOfDishEntries_ReturnsDishQuantityfromToJointTable_Dish()
        {
            string name = "Eggs and Bacon";
            string tableNumber = "4";
            DateTime orderDate = Convert.ToDateTime("01/01/2019");
            Dish newDish = new Dish(name);
            newDish.Save();
            int dishId = newDish.GetId();
            TableOrder newTableOrder = new TableOrder(tableNumber, orderDate);
            newTableOrder.Save();
            newTableOrder.AddDish(dishId, 2);

            int countDishEntries = 2;
            int result = newTableOrder.GetDishQuantity(dishId);

            Assert.AreEqual(countDishEntries, result);
        }

        [TestMethod]
        public void UpdateDish_UpdatesDishQuantityInJointTable_Dish()
        {
            string name = "Eggs and Bacon";
            string tableNumber = "4";
            DateTime orderDate = Convert.ToDateTime("01/01/2019");
            Dish newDish = new Dish(name);
            newDish.Save();
            int dishId = newDish.GetId();
            TableOrder newTableOrder = new TableOrder(tableNumber, orderDate);
            newTableOrder.Save();
            newTableOrder.AddDish(dishId, 2);
            newTableOrder.UpdateDish(dishId, 3);

            List<DishQuantity> testList = new List<DishQuantity>{ new DishQuantity(newDish, 3) };
            List<DishQuantity> result = newTableOrder.GetAllDishes();

            CollectionAssert.AreEqual(testList, result);
        }
        
        [TestMethod]
        public void GetAllDishes_ReturnsOrder_DishQuantityList()
        {
            Dish newDish = new Dish("Eggs and bacon");
            newDish.Save();
            int dishId = newDish.GetId();
            Dish newDish2 = new Dish("Cesar Salad");
            newDish2.Save();
            int dishId2 = newDish2.GetId();
            TableOrder newTableOrder = new TableOrder("4", Convert.ToDateTime("01/01/2019"));
            newTableOrder.Save();
            newTableOrder.AddDish(dishId, 2);
            newTableOrder.AddDish(dishId2, 1);

            List<DishQuantity> testList = new List<DishQuantity>{ new DishQuantity(newDish, 2), new DishQuantity(newDish2, 1) };
            List<DishQuantity> result = newTableOrder.GetAllDishes();
            Console.WriteLine("{0} {1}", testList[0].GetQuantity(), result[0].GetQuantity());
            Console.WriteLine("{0} {1}", testList[1].GetQuantity(), result[1].GetQuantity());
            CollectionAssert.AreEqual(testList, result);
            //Assert.AreEqual(testList.Count, result.Count);
            //Assert.AreEqual(2, result[0].GetQuantity());
        }
    }
}