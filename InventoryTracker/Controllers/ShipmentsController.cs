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

        [HttpGet("/shipments/show")]
        public ActionResult Show()
        {
            return View();
        }

        [HttpGet("shipments/edit")]
        public ActionResult Edit()
        {
            return View();
        }
  }
}
