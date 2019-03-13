using Microsoft.AspNetCore.Mvc;
using InventoryTracker.Models;
using System.Collections.Generic;
using System;

namespace InventoryTracker.Controllers
{
  public class TableOrdersController : Controller
  {

    [HttpGet("/orders")]
    public ActionResult Index()
    {
        List <TableOrder> allOrders = TableOrder.GetAll();
        return View(allOrders);
    }
    [HttpGet("/orders/new")]
    public ActionResult New()
    {
        return View();
    }

    [HttpPost("/orders/new")]
    public ActionResult Update(string tableNumber, DateTime orderDate)
    {
        TableOrder newOrder = new TableOrder(tableNumber, orderDate);
        newOrder.Save();
        //TODO: redirect to add dish
        return RedirectToAction("Index");
    }

    [HttpGet("/orders/{tableOrderId}")]
    public ActionResult Show(int tableOrderId)
    {
        TableOrder foundOrder = TableOrder.Find(tableOrderId);
        Dictionary<string, object> model = new Dictionary<string, object>();
        model["order"] = foundOrder;
        model["order_dishes"] = foundOrder.GetAllDishes();
        model["all_dishes"] = Dish.GetAll();
        return View(model);
    }

    [HttpPost("/orders/{tableOrderid}/add_dish")]
    public ActionResult Create(int tableOrderId, int dishId, int quantity)
    {
      TableOrder foundOrder = TableOrder.Find(tableOrderId);
      foundOrder.AddDish(dishId, quantity);
      return RedirectToAction("Show");
    }

    [HttpPost("/orders/{tableOrderid}/dishes/{dishId}/update")]
    public ActionResult Update(int tableOrderId, int dishId, int quantity)
    {
      TableOrder foundOrder = TableOrder.Find(tableOrderId);
      foundOrder.UpdateDish(dishId, quantity);
      return RedirectToAction("Show");
    }    
      
    [HttpPost("/orders/{tableOrderid}/dishes/{dishId}/delete")]
    public ActionResult Delete(int tableOrderId, int dishId)
    {
      Console.WriteLine(" Delete: {0} {1}", tableOrderId, dishId);
      TableOrder foundOrder = TableOrder.Find(tableOrderId);
      foundOrder.DeleteDish(dishId);
      return RedirectToAction("Show");
    }     
  }
}
