using Microsoft.AspNetCore.Mvc;
using InventoryTracker.Models;
using System.Collections.Generic;
using System;

namespace InventoryTracker.Controllers
{
    public class DishController : Controller
    {
      [HttpGet("/dishes")]
      public ActionResult Index()
      {
      List <Dish> allDish = Dish.GetAll();
      return View(allDish);
    }
    }
}
