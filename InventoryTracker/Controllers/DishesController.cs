using Microsoft.AspNetCore.Mvc;
using InventoryTracker.Models;
using System.Collections.Generic;
using System;

namespace InventoryTracker.Controllers
{
    public class DishesController : Controller
    {
      [HttpGet("/dishes")]
      public ActionResult Index()
      {
      List <Dish> allDishes = Dish.GetAll();
      return View(allDishes);
    }
    [HttpGet("/dishes/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpGet("/dishes/show")]
    public ActionResult Show()
    {
      return View();
    }

    [HttpGet("/dishes/edit")]
    {
      public ActionResult Edit()
      {
        return View();
      }
    }







    }
}
