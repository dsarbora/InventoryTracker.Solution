using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace InventoryTracker.Models
{
    public class TableOrder
    {
        string TableNumber;
        DateTime OrderDate;
        int DishId;
        int Id;
        public TableOrder(string tableNumber, DateTime orderDate, int id = 0)
        {
            TableNumber = tableNumber;
            OrderDate = orderDate;
            Id = id;
        }
        public int GetId()
        { return Id; }

        public string GetTableNumber()
        { return TableNumber; }

        public DateTime GetOrderDate()
        { return OrderDate; }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO table_orders (table_number, order_date) VALUES (@table_number, @order_date)", conn);
            MySqlParameter prmTableNumber = new MySqlParameter("@table_number", TableNumber);
            cmd.Parameters.Add(prmTableNumber);
            MySqlParameter prmOrderDate = new MySqlParameter("@order_date", OrderDate);
            cmd.Parameters.Add(prmOrderDate);
            cmd.ExecuteNonQuery();
            Id = (int)cmd.LastInsertedId;
            conn.Close();
            if(conn!=null)
            {
                conn.Dispose();
            }
        }
        public static List<TableOrder> GetAll()
        {
            List<TableOrder> allOrders = new List<TableOrder>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM table_orders", conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            int id = 0;
            string tableNumber = "";
            DateTime orderDate = DateTime.Now;
            while (rdr.Read())
            {
                id = rdr.GetInt32(0);
                tableNumber = rdr.GetString(1);
                orderDate = rdr.GetDateTime(2);
                TableOrder newOrder = new TableOrder(tableNumber, orderDate, id);
                allOrders.Add(newOrder);
            }
            conn.Close();
            if(conn!=null)
            {
                conn.Dispose();
            }
            return allOrders;
        }
        public static TableOrder Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM table_orders WHERE id=@id", conn);
            MySqlParameter prmId = new MySqlParameter("@id", id);
            cmd.Parameters.Add(prmId);
            MySqlDataReader rdr = cmd.ExecuteReader();
            string tableNumber = "";
            DateTime orderDate = DateTime.Now;
            while(rdr.Read())
            {
                tableNumber = rdr.GetString(1);
                orderDate = rdr.GetDateTime(2);
            }
            TableOrder foundOrder = new TableOrder(tableNumber, orderDate, id);
            conn.Close();
            if(conn!=null)
            {
                conn.Dispose();
            }
            return foundOrder;
        }

        public void Edit(string tableNumber, DateTime orderDate)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE table_orders SET table_number=@table_number, order_date=@date WHERE id=@id", conn);
            MySqlParameter prmTableNumber = new MySqlParameter("@table_number", tableNumber);
            cmd.Parameters.Add(prmTableNumber);
            MySqlParameter prmDate = new MySqlParameter("@date", orderDate);
            cmd.Parameters.Add(prmDate);
            MySqlParameter prmId = new MySqlParameter("@id", Id);
            cmd.Parameters.Add(prmId);
            cmd.ExecuteNonQuery();
            TableNumber = tableNumber;
            OrderDate = orderDate;
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
            MySqlCommand cmd = new MySqlCommand("DELETE FROM table_orders WHERE id=@id", conn);
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
            MySqlCommand cmd = new MySqlCommand("DELETE FROM table_orders; DELETE FROM orders; DELETE FROM dishes", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn!=null)
            {
                conn.Dispose();
            }
        }

        public override bool Equals(System.Object otherOrder)
        {
            if(!(otherOrder is TableOrder))
            { return false; }
            
            else
            {
                TableOrder newOrder = (TableOrder)otherOrder;
                bool idEquality = this.GetId().Equals(newOrder.GetId());
                bool dateEquality = this.GetOrderDate().Equals(newOrder.GetOrderDate());
                bool tableNumberEquality = this.GetTableNumber().Equals(newOrder.GetTableNumber());
                return (idEquality && dateEquality && tableNumberEquality);
            }
        }

        public void AddDish(int dishId, int quantity)
        {   
            MySqlConnection conn = DB.Connection();
            conn.Open();
            // 
            MySqlCommand cmd = new MySqlCommand("INSERT INTO orders (dish_id, table_order_id, dish_quantity) VALUES (@dish_id, @id, @quantity); UPDATE ingredients ing INNER JOIN (SELECT i_d.ingredient_id AS ingredient_id, i_d.ingredient_quantity * (dish_quantity) AS total FROM ingredients_dishes i_d JOIN orders ON i_d.dish_id=orders.dish_id WHERE table_order_id=@id and orders.dish_id=@dish_id) ord ON ing.id=ord.ingredient_id SET ing.quantity = ing.quantity - ord.total;", conn);
            MySqlParameter prmDishId = new MySqlParameter("@dish_id", dishId);
            cmd.Parameters.Add(prmDishId);
            MySqlParameter prmId = new MySqlParameter("@id", Id);
            cmd.Parameters.Add(prmId);
            MySqlParameter prmQuantity = new MySqlParameter("@quantity", quantity);
            cmd.Parameters.Add(prmQuantity);
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }

        public void DeleteDish(int dishId)
        {   
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE ingredients ing INNER JOIN (SELECT i_d.ingredient_id AS ingredient_id, i_d.ingredient_quantity * (-dish_quantity) AS total FROM ingredients_dishes i_d JOIN orders ON i_d.dish_id=orders.dish_id WHERE table_order_id=@id and orders.dish_id=@dish_id) ord ON ing.id=ord.ingredient_id SET ing.quantity = ing.quantity - ord.total; DELETE FROM orders WHERE dish_id=@dish_id and table_order_id=@id", conn);
            MySqlParameter prmDishId = new MySqlParameter("@dish_id", dishId);
            cmd.Parameters.Add(prmDishId);
            MySqlParameter prmId = new MySqlParameter("@id", Id);
            cmd.Parameters.Add(prmId);
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }

        public void UpdateDish(int dishId, int quantity)
        {   
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE ingredients ing INNER JOIN (SELECT i_d.ingredient_id AS ingredient_id, i_d.ingredient_quantity * (@quantity - dish_quantity) AS total FROM ingredients_dishes i_d JOIN orders ON i_d.dish_id=orders.dish_id WHERE table_order_id=@id and orders.dish_id=@dish_id) ord ON ing.id=ord.ingredient_id SET ing.quantity = ing.quantity - ord.total; UPDATE orders SET dish_quantity=@quantity WHERE dish_id=@dish_id and table_order_id=@id", conn);
            MySqlParameter prmDishId = new MySqlParameter("@dish_id", dishId);
            cmd.Parameters.Add(prmDishId);
            MySqlParameter prmId = new MySqlParameter("@id", Id);
            cmd.Parameters.Add(prmId);
            MySqlParameter prmQuantity = new MySqlParameter("@quantity", quantity);
            cmd.Parameters.Add(prmQuantity);
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }

        public int GetDishQuantity(int dishId)
        {
            int count = 0;
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT dish_quantity FROM orders WHERE dish_id=@dish_id and table_order_id=@id", conn);
            MySqlParameter prmDishId = new MySqlParameter("@dish_id", dishId);
            cmd.Parameters.Add(prmDishId);
            MySqlParameter prmId = new MySqlParameter("@id", Id);
            cmd.Parameters.Add(prmId);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                count = rdr.GetInt32(0);
            }
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            
            return count;
        }

        public List<DishQuantity> GetAllDishes()
        {
            List<DishQuantity> order = new List<DishQuantity>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            //MySqlCommand cmd = new MySqlCommand("SELECT dishes.* FROM orders JOIN dishes ON (orders.dish_id=dishes.id) WHERE orders.table_order_id=@id", conn);
            MySqlCommand cmd = new MySqlCommand("SELECT dishes.*, orders.dish_quantity FROM orders JOIN dishes ON (orders.dish_id=dishes.id) WHERE orders.table_order_id=@id", conn);
            cmd.Parameters.Add(new MySqlParameter("@id", Id));
            MySqlDataReader rdr = cmd.ExecuteReader();
            string name = "";
            int dishId = 0;
            int quantity = 0;
            while(rdr.Read())
            {
                dishId = rdr.GetInt32(0);
                name = rdr.GetString(1);
                quantity = rdr.GetInt32(2);
                Dish newDish = new Dish(name, dishId);
                DishQuantity newDishQuantity = new DishQuantity(newDish, quantity);
                order.Add(newDishQuantity);
            }
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }

            return order;
        }

        public List<Dish> GetPotentialDishes()
        {
            List<Dish> allDishes = new List<Dish>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM dishes WHERE id NOT IN (SELECT dish_id FROM orders WHERE table_order_id=@id)", conn);
            cmd.Parameters.Add(new MySqlParameter("@id", Id));
            MySqlDataReader rdr = cmd.ExecuteReader();
            string name = "";
            int id = 0;
            while(rdr.Read())
            {
                name = rdr.GetString(1);
                id = rdr.GetInt32(0);
                Dish newDish = new Dish(name, id);
                allDishes.Add(newDish); 
            }
            conn.Close();
            if(conn!=null)
            {
                conn.Dispose();
            }
            return allDishes;
        }
    }
}