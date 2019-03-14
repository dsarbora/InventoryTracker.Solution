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
    [HttpGet("/ingredients/{id}")]
    public ActionResult Show(int id)
    {
      Ingredient ingredient = Ingredient.Find(id);
      List<Dish> ingredientDishes = ingredient.GetDishes();
      List<Shipment> ingredientShipments = ingredient.GetShipments();
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("ingredient", ingredient);
      model.Add("ingredientDishes", ingredientDishes);
      model.Add("ingredientShipments", ingredientShipments);
      return View(model);
    }

    [HttpGet("/ingredients/edit")]
    public ActionResult Edit(int id)
    {
      Ingredient ingredient = Ingredient.Find(id);
      return View(ingredient);
    }

    [HttpPost("/ingredients/{ingredientId}")]
    public ActionResult Update(int ingredientId, string name, int quantity)
    {
      Ingredient ingredient = Ingredient.Find(ingredientId);
      ingredient.Edit(name, quantity);
      return RedirectToAction("Show", new {id=ingredientId});
    }

    [HttpPost("/ingredients/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Ingredient ingredient = Ingredient.Find(id);
      ingredient.Delete();
      return RedirectToAction("Index");
    }
  }
}
