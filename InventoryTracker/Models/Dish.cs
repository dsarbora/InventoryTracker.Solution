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

        public static void ClearAll()
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

        public static List<Dish> GetAll()
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
            MySqlCommand cmd = new MySqlCommand("UPDATE dishes SET name=@name WHERE id=@id", conn);
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

        public void AddIngredient(int ingredientId, int quantity)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO ingredients_dishes (ingredient_id, dish_id, ingredient_quantity) VALUES (@ingredientId, @dishId, @quantity);",conn);
            cmd.Parameters.Add(new MySqlParameter("@ingredientId", ingredientId));
            cmd.Parameters.Add(new MySqlParameter("@dishId", Id));
            cmd.Parameters.Add(new MySqlParameter("@quantity", quantity));
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn!=null)
            {
                conn.Dispose();
            }
        }

        public void UpdateIngredient(int ingredientId, int quantity)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE ingredients_dishes SET ingredient_quantity=@quantity WHERE ingredient_id=@ingredientId and dish_id=@dishId;",conn);
            cmd.Parameters.Add(new MySqlParameter("@ingredientId", ingredientId));
            cmd.Parameters.Add(new MySqlParameter("@dishId", Id));
            cmd.Parameters.Add(new MySqlParameter("@quantity", quantity));
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn!=null)
            {
                conn.Dispose();
            }
        }

        public void DeleteIngredient(int ingredientId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM ingredients_dishes WHERE ingredient_id = @ingredient_id AND dish_id = @dish_id", conn);
            cmd.Parameters.Add(new MySqlParameter("@ingredient_id", ingredientId));
            cmd.Parameters.Add(new MySqlParameter("@dish_id", Id));
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

        public List<IngredientQuantity> GetAllIngredients()
        {
            List<IngredientQuantity> allIngredients = new List<IngredientQuantity>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT ingredients.*, in_d.ingredient_quantity FROM ingredients JOIN ingredients_dishes in_d ON (ingredients.id = in_d.ingredient_id) WHERE in_d.dish_id=@id", conn);
            cmd.Parameters.Add(new MySqlParameter("@id", Id));
            MySqlDataReader rdr = cmd.ExecuteReader();
            string name = "";
            int ingredientId = 0;
            int inventoryQuantity = 0;
            int dishQuantity = 0;
            while(rdr.Read())
            {
                ingredientId = rdr.GetInt32(0);
                name = rdr.GetString(1);
                inventoryQuantity = rdr.GetInt32(2);
                dishQuantity = rdr.GetInt32(3);
                Ingredient ingredient = new Ingredient(name, inventoryQuantity, ingredientId);
                IngredientQuantity ingredientQuantity = new IngredientQuantity(ingredient, dishQuantity);
                allIngredients.Add(ingredientQuantity);
            }
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            return allIngredients;
        }
    }
}