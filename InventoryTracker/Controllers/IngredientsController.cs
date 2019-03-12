using Microsoft.AspNetCore.Mvc;
using InventoryTracker.Models;
using System.Collections.Generic;
using System;

namespace InventoryTracker.Controllers
{
  public class IngredientsController : Controller
  {
      [HttpGet("/ingredients")]
      public ActionResult Index()
      {
      List <Ingredient> allIngredients = Ingredient.GetAll();
      return View(allIngredients);
    }
    [HttpGet("/ingredients/new")]
    public ActionResult New()
    {
      return View();
    }
    [HttpGet("/ingredients/show")]
    public ActionResult Show()
    {
      return View();
    }

    [HttpGet("/ingredients/edit")]
    public ActionResult Edit()
    {
      return View();
    }
  }
}
