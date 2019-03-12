using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using InventoryTracker.Models;

namespace InventoryTracker.Tests
{
    [TestClass]
    public class OrderTest : IDisposable
    {
        public OrderTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root; password=root;port=8889;database=restaurant_test;";
        }
        public void Dispose()
        {
            Ingredient.ClearAll();
            Dish.ClearAll();
           // Shipment.ClearAll();
            Order.ClearAll();
        }

        [TestMethod]
        public void GetId_ReturnsId_Int()
        {
            int id = 0;
            int dishId = 0;
            int quantity = 0;
            DateTime orderDate = Convert.ToDateTime("01/01/2019");
            Order newOrder = new Order(orderDate, dishId, quantity, id);
            int result = newOrder.GetId();
            Assert.AreEqual(result, id);
        }

        [TestMethod]
        public void GetDishId_ReturnsDishId_Int()
        {
            int id = 0;
            int dishId = 0;
            int quantity = 0;
            DateTime orderDate = Convert.ToDateTime("01/01/2019");
            Order newOrder = new Order(orderDate, dishId, quantity, id);
            int result = newOrder.GetDishId();
            Assert.AreEqual(result, dishId);
        }

        [TestMethod]
        public void GetQuantity_ReturnsQuantity_Int()
        {
            int id = 0;
            int dishId = 0;
            int quantity = 0;
            DateTime orderDate = Convert.ToDateTime("01/01/2019");
            Order newOrder = new Order(orderDate, dishId, quantity, id);
            int result = newOrder.GetQuantity();
            Assert.AreEqual(result, quantity);
        }

        [TestMethod]
        public void GetOrderDate_ReturnsOrderDate_DateTime()
        {
            int id = 0;
            int dishId = 0;
            int quantity = 0;
            DateTime orderDate = Convert.ToDateTime("01/01/2019");
            Order newOrder = new Order(orderDate, dishId, quantity, id);
            DateTime result = newOrder.GetOrderDate();
            Assert.AreEqual(result, orderDate);
        }
        
        [TestMethod]
        public void Save_SavesOrderToDatabase_Order()
        {
            int dishId = 0;
            int quantity = 0;
            DateTime orderDate = Convert.ToDateTime("01/01/2019");
            Order newOrder = new Order(orderDate, dishId, quantity);
            newOrder.Save();

            Order testOrder = Order.GetAll()[0];
            Assert.AreEqual(newOrder, testOrder);
        }

        [TestMethod]
        public void Edit_EditsOrderInDatabase_Order()
        {
            int dishId = 0;
            int quantity = 0;
            DateTime orderDate = Convert.ToDateTime("01/01/2019");
            Order newOrder = new Order(orderDate, dishId, quantity);
            newOrder.Save();
            int newDishId = 1;
            int newQuantity = 2;
            DateTime newOrderDate = Convert.ToDateTime("02/02/2019");
            newOrder.Edit(newOrderDate, newDishId, newQuantity);
            
            Order testOrder = Order.GetAll()[0];
            Assert.AreEqual(newOrder, testOrder);
        }

        [TestMethod]
        public void Find_FindsOrderInDatabase_Order()
        {
            int dishId = 0;
            int quantity = 0;
            DateTime orderDate = Convert.ToDateTime("01/01/2019");
            Order newOrder = new Order(orderDate, dishId, quantity);
            newOrder.Save();

            int id = newOrder.GetId();
            Order testOrder = Order.Find(id);
            Assert.AreEqual(newOrder, testOrder);
        }

        [TestMethod]
        public void Delete_DeletesOrderFromDatabase_EmptyList()
        {
            int dishId = 0;
            int quantity = 0;
            DateTime orderDate = Convert.ToDateTime("01/01/2019");
            Order newOrder = new Order(orderDate, dishId, quantity);
            newOrder.Save();
            newOrder.Delete();
            List<Order> testList = new List<Order>{};
            List<Order> result = Order.GetAll();
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void GetAll_ReturnsAllOrdersFromDatabase_OrderList()
        {
            int firstDishId = 0;
            int firstQuantity = 0;
            DateTime firstOrderDate = Convert.ToDateTime("01/01/2019");
            Order firstOrder = new Order(firstOrderDate, firstDishId, firstQuantity);
            firstOrder.Save();
            int secondDishId= 0;
            int secondQuantity = 0;
            DateTime secondOrderDate = Convert.ToDateTime("02/02/2019");
            Order secondOrder = new Order(secondOrderDate, secondDishId, secondQuantity);
            secondOrder.Save();

            List<Order> testList = new List<Order> {firstOrder, secondOrder};
            List<Order> result = Order.GetAll();
            CollectionAssert.AreEqual(testList, result);
        }

    }
}