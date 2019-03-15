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

    [HttpPost("/orders")]
    public ActionResult Create(string tableNumber, DateTime orderDate)
    {
        TableOrder newOrder = new TableOrder(tableNumber, orderDate);
        newOrder.Save();
        return RedirectToAction("Show", new { tableOrderId = newOrder.GetId() });
    }

    [HttpPost("/orders/{tableOrderId}/delete")]
    public ActionResult Delete(int tableOrderId)
    {
        TableOrder foundOrder = TableOrder.Find(tableOrderId);
        foundOrder.Delete();
        return RedirectToAction("Index");
    }

    [HttpGet("/orders/{tableOrderId}")]
    public ActionResult Show(int tableOrderId)
    {
        TableOrder foundOrder = TableOrder.Find(tableOrderId);
        Dictionary<string, object> model = new Dictionary<string, object>();
        model["order"] = foundOrder;
        model["order_dishes"] = foundOrder.GetAllDishes();
        model["all_dishes"] = foundOrder.GetPotentialDishes();//Dish.GetAll();
        return View(model);
    }

    [HttpPost("/orders/{tableOrderid}/dishes")]
    public ActionResult Create(int tableOrderId, int dishId, int newDishQuantity)
    {
      TableOrder foundOrder = TableOrder.Find(tableOrderId);
      foundOrder.AddDish(dishId, newDishQuantity);
      return RedirectToAction("Show");
    }

    [HttpPost("/orders/{tableOrderid}/dishes/{dishId}")]
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

     [HttpGet("/orders/{tableOrderId}/edit")]
    public ActionResult Edit(int tableOrderId)
    {
      TableOrder foundTableOrder = TableOrder.Find(tableOrderId);
      return View(foundTableOrder);
    }

    [HttpPost("/orders/{tableOrderId}")]
    public ActionResult Update(int tableOrderId, string tableNumber, DateTime orderDate)
    {
      TableOrder foundTableOrder = TableOrder.Find(tableOrderId);
      foundTableOrder.Edit(tableNumber, orderDate);
      return RedirectToAction("Show");
    }
  }
}
