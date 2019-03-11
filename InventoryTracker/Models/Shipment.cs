using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace InventoryTracker.Models
{
  public class Shipment
  {
    private int Id;
    private DateTime Date;

    public Shipment(int id,DateTime date)
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
      return date;
    }
    public void Save()
    {
      MySqlConnection conn = Db.Connection();
      conn.Open();
      MySqlCommand cmd = new MySqlCommand("INSERT INTO shipments (id, date) VALUE (@id, @date)",conn);
      MySqlParameter prId = new MySqlParameter();
      prId.ParameterId = "@id";
      prId.Value = Id;
      cmd.Parameters.Add(prId);
      MySqlParameter prDate = new MySqlParameter();
      prDate.ParameterId = "@date";
      prDate.Value = Date;
      cmd.Parameters.Add(prDate);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn!=null)
       {
        conn.Dispose();
      }

    }
  }
}
