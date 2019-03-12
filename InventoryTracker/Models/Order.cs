using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace InventoryTracker.Models
{
    public class Order
    {
        int Quantity;
        DateTime Date;
        int MealId;
        int Id;
        public Order(int quantity=0, int mealId=0, int id=0)
        {
            Quantity = quantity;
            //Date = date;
            MealId=mealId;
            Id=id;
        }
        public int GetId()
        { return Id; }

        public int GetQuantity()
        { return Quantity; }

        public DateTime GetDate()
        { return Date; }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            conn.Close();
            if(conn!=null)
            {
                conn.Dispose();
            }
        }
        public static List<Order> GetAll()
        {
            List<Order> allOrders = new List<Order>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            conn.Close();
            if(conn!=null)
            {
                conn.Dispose();
            }
            return allOrders;
        }
        public static Order Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            Order foundOrder = new Order(0);
            conn.Close();
            if(conn!=null)
            {
                conn.Dispose();
            }
            return foundOrder;
        }

        public void Edit(int mealId, int quantity)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            conn.Close();
            if(conn!=null)
            {
                conn.Dispose();
            }
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            conn.Close();
            if(conn!=null)
            {
                conn.Dispose();
            }
        }
        public void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            conn.Close();
            if(conn!=null)
            {
                conn.Dispose();
            }
        }

        public override bool Equals(System.Object otherOrder)
        {
            if(!(otherOrder is Order))
            { return false; }
            
            else
            {
                Order newOrder = (Order)otherOrder;
                bool idEquality = this.GetId().Equals(newOrder.GetId());
                bool dateEquality = this.GetDate().Equals(newOrder.GetDate());
                bool quantityEquality = this.GetQuantity().Equals(newOrder.GetQuantity());
                return (idEquality && dateEquality && quantityEquality);
            }
        }
    }
}