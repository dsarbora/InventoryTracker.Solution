using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace InventoryTracker.Models
{
  public class Shipment
  {
    private int Id;
    private DateTime Date;

    public Shipment(DateTime date, int id=0)
    {
      Id = id;
      Date = date;
    }
    public int GetId()
    {
      return Id;
    }
    public DateTime GetDate()
    {
      return Date;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = new MySqlCommand("INSERT INTO shipments (shipment_date) VALUES (@date)", conn);
      MySqlParameter prmDate = new MySqlParameter();
      prmDate.ParameterName = "@date";
      prmDate.Value = Date;
      cmd.Parameters.Add(prmDate);
      cmd.ExecuteNonQuery();
      Id=(int)cmd.LastInsertedId;
      conn.Close();
      if (conn!=null)
       {
        conn.Dispose();
      }
    }

    public static Shipment Find(int ingredientId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = new MySqlCommand("SELECT * FROM shipments WHERE id = @id;", conn);
      MySqlParameter prmId = new MySqlParameter();
      prmId.ParameterName = "@id";
      prmId.Value = ingredientId;
      cmd.Parameters.Add(prmId);
      MySqlDataReader rdr = cmd.ExecuteReader();
      DateTime date = DateTime.Now;
      while(rdr.Read())
      {
        date = rdr.GetDateTime(1);
      }
      Shipment newShipment = new Shipment(date, ingredientId);
      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
      return newShipment;
    }

    public static List<Shipment> GetAll()
    {
      List<Shipment> allShipments = new List<Shipment>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = new MySqlCommand("SELECT * FROM shipments", conn);
      MySqlDataReader rdr= cmd.ExecuteReader();
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        DateTime date = rdr.GetDateTime(1);
        Shipment newShipment = new Shipment(date, id);
        allShipments.Add(newShipment);
      }
      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
      return allShipments;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = new MySqlCommand("DELETE FROM shipments; DELETE FROM ingredients_shipments;", conn);
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
      MySqlCommand cmd = new MySqlCommand("INSERT INTO ingredients_shipments (ingredient_id, shipment_id, quantity) VALUES (@ingredient_id, @shipment_id, @quantity);", conn);
      cmd.Parameters.Add(new MySqlParameter("@shipment_id", Id));
      cmd.Parameters.Add(new MySqlParameter("@ingredient_id", ingredientId));
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
      MySqlCommand cmd = new MySqlCommand("DELETE FROM ingredients_shipments WHERE shipment_id = @shipment_id AND ingredient_id=@ingredient_id;", conn);
      cmd.Parameters.Add(new MySqlParameter("@shipment_id", Id));
      cmd.Parameters.Add(new MySqlParameter("@ingredient_id", ingredientId));
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
    }

    public void EditIngredientQuantity(int ingredientId, int quantity)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = new MySqlCommand("UPDATE ingredients_shipments SET quantity=@quantity WHERE shipment_id=@shipment_id AND ingredient_id=@ingredient_id;", conn);
      cmd.Parameters.Add(new MySqlParameter("@quantity", quantity));
      cmd.Parameters.Add(new MySqlParameter("@shipment_id", Id));
      cmd.Parameters.Add(new MySqlParameter("@ingredient_id", ingredientId));
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
    }

    public List<IngredientQuantity> GetAllIngredients()
    {
      List<IngredientQuantity> allIngredients = new List<IngredientQuantity>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = new MySqlCommand("SELECT ingredients.*, i_s.quantity FROM ingredients_shipments i_s JOIN ingredients ON (ingredients.id = i_s.ingredient_id) WHERE i_s.shipment_id=@shipment_id", conn);
      cmd.Parameters.Add(new MySqlParameter("@shipment_id", Id));
      MySqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name=rdr.GetString(1);
        int quantity = rdr.GetInt32(2);
        int shipmentQuantity = rdr.GetInt32(3);
        Ingredient newIngredient = new Ingredient(name, quantity, id);
        allIngredients.Add(new IngredientQuantity(newIngredient, shipmentQuantity));
      }
      conn.Close();
      if(conn!=null)
      {
        conn.Dispose();
      }
      return allIngredients;
    }

    public override bool Equals(System.Object otherShipment)
    {
      if(!(otherShipment is Shipment))
      {
        return false;
      }
      else
      {
        Shipment newShipment = (Shipment)otherShipment;
        bool idEquality = this.GetId().Equals(newShipment.GetId());
        bool dateEquality = this.GetDate().Equals(newShipment.GetDate());
        return (idEquality && dateEquality);
      }
    }
  }
}
