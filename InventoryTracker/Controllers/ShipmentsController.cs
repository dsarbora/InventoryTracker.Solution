using Microsoft.AspNetCore.Mvc;
using InventoryTracker.Models;
using System.Collections.Generic;
using System;

namespace InventoryTracker.Controllers
{
  public class ShipmentsController : Controller
  {

          [HttpGet("/shipments")]
          public ActionResult Index(){
          List <Shipment> allShipments = Shipment.GetAll();
          return View(allShipments);
        }
        
        [HttpGet("/shipments/new")]
        public ActionResult New()
        {
          return View();
        }

        [HttpGet("/shipments/{id}")]
        public ActionResult Show(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Shipment newShipment = Shipment.Find(id);
            List<Ingredient> allIngredients = Ingredient.GetAll();
            List<IngredientQuantity> shipmentIngredients = newShipment.GetAllIngredients();
            model.Add("shipment", newShipment);
            model.Add("allIngredients", allIngredients);
            model.Add("shipmentIngredients", shipmentIngredients);
            return View(model);
        }

        [HttpGet("shipments/{id}/edit")]
        public ActionResult Edit(int id)
        {
          Shipment shipment = Shipment.Find(id);
            return View(shipment);
        }

        [HttpPost("/shipments/create")]
        public ActionResult Create(DateTime date)
        {
            Shipment newShipment = new Shipment(date);
            newShipment.Save();
            return RedirectToAction("Show", new {id=newShipment.GetId()});
        }
        
        [HttpPost("/shipments/{shipmentId}/add_ingredient")]
        public ActionResult AddIngredient(int shipmentId, int ingredientId)
        {
          Shipment newShipment = Shipment.Find(shipmentId);
          newShipment.AddIngredient(ingredientId, 0);
          return RedirectToAction("Show", new {id=newShipment.GetId()});
        }

        [HttpPost("/shipments/{shipmentId}/ingredients/{ingredientId}/edit")]
        public ActionResult UpdateIngredient(int shipmentId, int ingredientId, int quantity)
        {
          Shipment newShipment = Shipment.Find(shipmentId);
          newShipment.EditIngredientQuantity(ingredientId, quantity);
          return RedirectToAction("Show", new {id=newShipment.GetId()});
        }

        [HttpPost("/shipments/{shipmentId}/delete")]
        public ActionResult Delete(int shipmentId)
        {
          Shipment newShipment = Shipment.Find(shipmentId);
          newShipment.Delete();
          return RedirectToAction("Index");
        }
  }
}
