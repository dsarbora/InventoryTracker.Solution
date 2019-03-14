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

    [HttpPost("/dishes")]
    public ActionResult Create(string name)
    {
      Dish newDish = new Dish(name);
      newDish.Save();
      return RedirectToAction("Show", new { dishId = newDish.GetId() });
    }

    [HttpPost("/dishes/{dishId}")]
    public ActionResult Update(int dishId, string name)
    {
      Dish foundDish = Dish.Find(dishId);
      foundDish.Edit(name);
      return RedirectToAction("Show");
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

    [HttpPost("/dishes/{dishId}/ingredients")]
    public ActionResult CreateIngredient(int dishId, int ingredientId, int quantity)
    {
      Dish foundDish = Dish.Find(dishId);
      foundDish.AddIngredient(ingredientId, quantity);
      return RedirectToAction("Show");
    }

    [HttpPost("/dishes/{dishId}/ingredients/{ingredientId}")]
    public ActionResult Update(int dishId, int ingredientId, int quantity)
    {
      Dish foundDish = Dish.Find(dishId);
      foundDish.UpdateIngredient(ingredientId, quantity);
      return RedirectToAction("Show");
    }    
      
    [HttpPost("/dishes/{dishId}/ingredients/{ingredientId}/delete")]
    public ActionResult Delete(int dishId, int ingredientId)
    {
      Dish foundDish = Dish.Find(dishId);
      foundDish.DeleteIngredient(ingredientId);
      return RedirectToAction("Show");
    } 
    [HttpGet("/dishes/{dishId}/edit")]
      public ActionResult Edit(int dishId)
      {
        Dish foundDish = Dish.Find(dishId);
        return View(foundDish);
      }

    }
}
