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
        model["dishes"] = foundOrder.GetAllDishes();
        return View(model);
    }
  }
}
