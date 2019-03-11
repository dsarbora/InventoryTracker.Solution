using Microsoft.AspNetCore.Mvc;
using InventoryTracker.Models;
using System.Collections.Generic;
using System;

namespace InventoryTracker.Controllers
{
  public class IngredientController : Controller
  {
      [HttpGet("/ingredients")]
      public ActionResult Index()
      {
      List <Ingredient> allIngredient = Ingredient.GetAll();
      return View(allIngredient);
    }
    [HttpGet("/ingredients/new")]
    public ActionResult New()
    {
      return View();
    }
  }
}
