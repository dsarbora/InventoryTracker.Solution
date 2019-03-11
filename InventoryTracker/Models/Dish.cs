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

        public void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM dishes", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Dish> GetAll()
        {
            List<Dish> allDishes = new List<Dish>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM dishes", conn);
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

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO dishes (name) VALUES (@name)", conn);
            MySqlParameter prmName = new MySqlParameter("@name", Name);
            cmd.Parameters.Add(prmName);
            cmd.ExecuteNonQuery();
            Id = (int) cmd.LastInsertedId;
            conn.Close();
            if(conn!=null)
            {
                conn.Dispose();
            }
        }

        public static Dish Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM dishes WHERE id=@id", conn);
            MySqlParameter prmId = new MySqlParameter("@id", id);
            cmd.Parameters.Add(prmId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            
            string name = "";
            while(rdr.Read())
            {
                name = rdr.GetString(1);
            }
            Dish newDish = new Dish(name, id);
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            return newDish;
        }

        public void Edit(string newName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE dishes SET (name=@name) WHERE id=@id", conn);
            MySqlParameter prmName = new MySqlParameter("@name", newName);
            MySqlParameter prmId = new MySqlParameter("@id", Id);
            cmd.Parameters.Add(prmName);
            cmd.Parameters.Add(prmId);
            cmd.ExecuteNonQuery();
            Name = newName;
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM dishes WHERE id=@id", conn);
            MySqlParameter prmId = new MySqlParameter("@id", Id);
            cmd.Parameters.Add(prmId);
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn!=null)
            {
                conn.Dispose();
            }
        }

        public override bool Equals(System.Object otherDish)
        {
            if(!(otherDish is Dish))
            {
                return false;
            }
            else
            {
                Dish newDish = (Dish)otherDish;
                bool nameEquality = this.GetName().Equals(newDish.GetName());
                bool idEquality = this.GetId().Equals(newDish.GetId());
                return (nameEquality && idEquality);
            }
        }
    }

}