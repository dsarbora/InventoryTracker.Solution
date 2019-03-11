using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace InventoryTracker.Models
{
  public class Shipment
  {
    private int Id;
    private DateTime Date;

    public Shipment(int id, DateTime date)
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
      MySqlCommand cmd = new MySqlCommand("INSERT INTO shipments (date) VALUE (@date)", conn);
      MySqlParameter prmDate = new MySqlParameter();
      prmDate.ParameterName = "@date";
      prmDate.Value = Date;
      cmd.Parameters.Add(prmDate);
      cmd.ExecuteNonQuery();
      Id=(int)cmd.LastInsertedId();
      conn.Close();
      if (conn!=null)
       {
        conn.Dispose();
      }

    }
  }
}
