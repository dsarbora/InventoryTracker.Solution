using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace InventoryTracker.Models
{
    public class Ingredient
    {
        private int Id;
        private string Name;
        private int Quantity;

        public Ingredient(string name, int quantity=0, int id=0)
        {
            Name=name;
            Quantity=quantity;
            Id=id;
        }

        public string GetName() { return Name; }
        public int GetQuantity() { return Quantity; }
        public int GetId() { return Id; }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO ingredients (name, quantity) VALUES (@name, @quantity);",conn);
            MySqlParameter prmName = new MySqlParameter();
            prmName.ParameterName = "@name";
            prmName.Value = Name;
            cmd.Parameters.Add(prmName);
            MySqlParameter prmQuantity = new MySqlParameter();
            prmQuantity.ParameterName = "@quantity";
            prmQuantity.Value = Quantity;
            cmd.Parameters.Add(prmQuantity);
            cmd.ExecuteNonQuery();
            conn.Close();            
            if(conn!=null)
            {
                conn.Dispose();
            }
        }

        public static Ingredient Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM ingredients WHERE id=@id;", conn);
            MySqlParameter prmId = new MySqlParameter();
            prmId.ParameterName = "@id";
            prmId.Value = id;
            cmd.Parameters.Add(prmId);
            MySqlDataReader rdr = cmd.ExecuteReader();
            string name="";
            int quantity=0;
            while(rdr.Read())
            {
                name=rdr.GetString(1);
                quantity=rdr.GetInt32(2);
            }
            Ingredient newIngredient = new Ingredient(name, quantity, id);
            conn.Close();
            if(conn!=null)
            {
                conn.Dispose();
            }
            return newIngredient;
        }

        public static List<Ingredient> GetAll()
        {

        }

        public void Edit()
        {

        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM ingredients WHERE id=@id; DELETE FROM ingredients_dishes WHERE ingredient_id = @id;", conn);
            MySqlParameter prmId = new MySqlParameter();
            prmId.ParameterName = "@id";
            prmId.Value = Id;
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
            MySqlCommand cmd = new MySqlCommand("DELETE FROM ingredients; DELETE FROM ingredients_dishes; DELETE FROM ingredients_shipments;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn!=null)
            {
                conn.Dispose();
            }            
        }
    }
}