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
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM ingredients ORDER BY name;", conn);
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

        public List<Shipment> GetShipments()
        {
            List<Shipment> allIngredientShipments = new List<Shipment>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT shipments.* FROM ingredients JOIN ingredients_shipments i_s ON (i_s.ingredient_id=ingredients.id) JOIN shipments ON (shipments.id=i_s.shipment_id) WHERE ingredients.id = @id ORDER BY shipment_date;", conn);
            cmd.Parameters.Add(new MySqlParameter("@id", Id));
            MySqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                DateTime date = rdr.GetDateTime(1);
                Shipment newShipment = new Shipment(date, id);
                allIngredientShipments.Add(newShipment);
            }
            return allIngredientShipments;
        }

        public List<Dish> GetDishes()
        {
            List<Dish> allIngredientDishes = new List<Dish>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT dishes.* FROM ingredients JOIN ingredients_dishes i_d ON( i_d.ingredient_id = ingredients.id) JOIN dishes ON(dishes.id=i_d.dish_id) WHERE ingredients.id=@id ORDER BY dishes.name;", conn);
            cmd.Parameters.Add(new MySqlParameter("@id", Id));
            MySqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name= rdr.GetString(1);
                Dish newDish = new Dish(name, id);
                allIngredientDishes.Add(newDish);
            }
            conn.Close();
            if(conn!=null)
            {
                conn.Dispose();
            }
            return allIngredientDishes;
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
        public override int GetHashCode()
        {
            return this.GetId().GetHashCode();
        }
    }
}
