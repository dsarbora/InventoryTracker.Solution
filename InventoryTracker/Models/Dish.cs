using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace InventoryTracker.Models
{
    public class Dish
    {
        private int Id;
        private string Name;

        public Dish(string name, int id = 0)
        {
            Name = name;
            Id = id;
        }

        public string GetName() 
        {
            return Name; 
        }
        public int GetId()
        {
            return Id;
        }

        public void DeleteAll()
        {
            MySqlCommand conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = MySqlCommand("DELETE FROM dishes", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }
    }

}