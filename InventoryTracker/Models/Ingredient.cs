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
            Id=(int)cmd.LastInsertedId;
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
            List<Ingredient> allIngredients = new List<Ingredient>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM ingredients;", conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name=rdr.GetString(1);
                int quantity = rdr.GetInt32(2);
                Ingredient ingredient = new Ingredient(name, quantity, id);
                allIngredients.Add(ingredient);
            }
            conn.Close();
            if(conn!=null)
            {
                conn.Dispose();
            }
            return allIngredients;
        }

        public void Edit(string name, int quantity)
        {
            MySqlConnection conn=DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE ingredients SET name=@name, quantity=@quantity WHERE id = @id;", conn);
            MySqlParameter prmName = new MySqlParameter();
            prmName.ParameterName = "@name";
            prmName.Value = name;
            cmd.Parameters.Add(prmName);
            MySqlParameter prmQuantity = new MySqlParameter();
            prmQuantity.ParameterName = "@quantity";
            prmQuantity.Value = quantity;
            cmd.Parameters.Add(prmQuantity);
            MySqlParameter prmId = new MySqlParameter();
            prmId.ParameterName = "@id";
            prmId.Value = Id;
            cmd.Parameters.Add(prmId);
            cmd.ExecuteNonQuery();
            Name = name;
            Quantity = quantity;
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
        
        public override bool Equals(System.Object otherIngredient)
        {
            if(!(otherIngredient is Ingredient))
            {
                return false;
            }
            else
            {
                Ingredient newIngredient = (Ingredient)otherIngredient;
                bool idEquality = this.GetId().Equals(newIngredient.GetId());
                bool nameEquality = this.GetName().Equals(newIngredient.GetName());
                bool quantityEquality = this.GetQuantity().Equals(newIngredient.GetQuantity());
                return (idEquality && nameEquality && quantityEquality);
            }
        }
    }
}