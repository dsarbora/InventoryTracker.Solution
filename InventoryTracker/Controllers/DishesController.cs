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
      List<Dish> allDishes = Dish.GetAll();
      return View(allDishes);
    }
    [HttpGet("/dishes/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/dishes/new")]
    public ActionResult Create(string name)
    {
      Dish newDish = new Dish(name);
      newDish.Save();
      return RedirectToAction("Show", new { dishId = newDish.GetId() });
    }

    [HttpGet("/dishes/{dishId}")]
    public ActionResult Show(int dishId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Dish foundDish = Dish.Find(dishId);
      model["dish"] = foundDish;
      List<IngredientQuantity> dishIngredients = foundDish.GetAllIngredients();
      model["dish_ingredients"] = dishIngredients;
      List<Ingredient> allIngredients = Ingredient.GetAll();
      model["all_ingredients"] = allIngredients;
      return View(model);
    }

    [HttpPost("/dishes/{dishId}/add_ingredient")]
    public ActionResult CreateIngredient(int dishId, int ingredientId, int quantity)
    {
      Dish foundDish = Dish.Find(dishId);
      foundDish.AddIngredient(ingredientId, quantity);
      return RedirectToAction("Show");
    }

    [HttpPost("/dishes/{dishId}/ingredients/{ingredientId}/update")]
    public ActionResult UpdateIngredient(int dishId, int ingredientId, int quantity)
    {
      Dish foundDish = Dish.Find(dishId);
      foundDish.UpdateIngredient(ingredientId, quantity);
      return RedirectToAction("Show");
    }    
      
    [HttpPost("/dishes/{dishId}/ingredients/{ingredientId}/delete")]
    public ActionResult DeleteIngredient(int dishId, int ingredientId)
    {
      Console.WriteLine(" Delete: {0} {1}", dishId, ingredientId);
      Dish foundDish = Dish.Find(dishId);
      foundDish.DeleteIngredient(ingredientId);
      return RedirectToAction("Show");
    } 
    [HttpGet("/dishes/edit")]
  
      public ActionResult Edit()
      {
        return View();
      }

    }
}
