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
      MySqlCommand cmd = new MySqlCommand("INSERT INTO shipments (date) VALUES (@date)", conn);
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

    public static Shipment Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = new MySqlCommand("SELECT * FROM shipments WHERE id = @id;", conn);
      MySqlParameter prmId = new MySqlParameter();
      prmId.ParameterName = "@id";
      prmId.Value = id;
      cmd.Parameters.Add(prmId);
      MySqlDataReader rdr = cmd.ExecuteReader();
      DateTime date = DateTime.Now;
      while(rdr.Read())
      {
        date = rdr.GetDateTime(1);
      }
      Shipment newShipment = new Shipment(date, id);
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
