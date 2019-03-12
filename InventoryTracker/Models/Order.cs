using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace InventoryTracker.Models
{
    public class Order
    {
        int Quantity;
        DateTime Date;
        int DishId;
        int Id;
        public Order(DateTime date, int quantity, int dishId, int id=0)
        {
            Quantity = quantity;
            Date = date;
            DishId = dishId;
            Id = id;
        }
        public int GetId()
        { return Id; }

        public int GetQuantity()
        { return Quantity; }

        public DateTime GetDate()
        { return Date; }

        public int GetDishId()
        { return DishId; }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO orders (dish_id, quantity, order_date) VALUES (@dish_id, @quantity, @order_date)", conn);
            MySqlParameter prmDishId = new MySqlParameter("@dish_id", DishId);
            cmd.Parameters.Add(prmDishId);
            MySqlParameter prmQuantity = new MySqlParameter("@quantity", Quantity);
            cmd.Parameters.Add(prmQuantity);
            MySqlParameter prmDate = new MySqlParameter("@order_date", Date);
            cmd.Parameters.Add(prmDate);
            cmd.ExecuteNonQuery();
            Id = (int)cmd.LastInsertedId;
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
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM orders", conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            int id = 0;
            int dishId = 0;
            int quantity = 0;
            DateTime orderDate = DateTime.Now;
            while (rdr.Read())
            {
                id = rdr.GetInt32(0);
                dishId = rdr.GetInt32(1);
                quantity = rdr.GetInt32(2);
                orderDate = rdr.GetDateTime(3);
                Order newOrder = new Order(orderDate, quantity, dishId, id);
                allOrders.Add(newOrder);
            }
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
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM orders WHERE id=@id", conn);
            MySqlParameter prmId = new MySqlParameter("@id", id);
            cmd.Parameters.Add(prmId);
            MySqlDataReader rdr = cmd.ExecuteReader();
            int dishId = 0;
            int quantity = 0;
            DateTime orderDate = DateTime.Now;
            while(rdr.Read())
            {
                dishId = rdr.GetInt32(1);
                quantity = rdr.GetInt32(2);
                orderDate = rdr.GetDateTime(3);
            }
            Order foundOrder = new Order(orderDate, quantity, dishId, id);
            conn.Close();
            if(conn!=null)
            {
                conn.Dispose();
            }
            return foundOrder;
        }

        public void Edit(int dishId, int quantity, DateTime orderDate)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE orders SET dish_id=@dish_id, quantity=@quantity, date=@date WHERE id=@id", conn);
            MySqlParameter prmDishId = new MySqlParameter("@dish_id", dishId);
            cmd.Parameters.Add(prmDishId);
            MySqlParameter prmQuantity = new MySqlParameter("@quantity", quantity);
            cmd.Parameters.Add(prmQuantity);
            MySqlParameter prmDate = new MySqlParameter("@order_date", orderDate);
            cmd.Parameters.Add(prmDate);
            MySqlParameter prmId = new MySqlParameter("@id", Id);
            cmd.Parameters.Add(prmId);
            cmd.ExecuteNonQuery();
            DishId = dishId;
            Quantity = quantity;
            Date = orderDate;
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
            MySqlCommand cmd = new MySqlCommand("DELETE FROM orders WHERE id=@id", conn);
            MySqlParameter prmId = new MySqlParameter("@id", Id);
            cmd.Parameters.Add(prmId);
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn!=null)
            {
                conn.Dispose();
            }
        }
        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM orders", conn);
            cmd.ExecuteNonQuery();
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
                bool dishEquality = this.GetDishId().Equals(newOrder.GetDishId());
                bool quantityEquality = this.GetQuantity().Equals(newOrder.GetQuantity());
                return (idEquality && dateEquality && quantityEquality && dishEquality);
            }
        }
    }
}