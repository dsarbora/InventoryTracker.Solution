// using Microsoft.AspNetCore.Mvc;
// using InventoryTracker.Models;
// using System.Collections.Generic;
// using System;
//
// namespace InventoryTracker.Controllers
// {
//   public class ShipmentController : Controller
//   {
//
//           [HttpGet("/shipments")]
//           public ActionResult Index(){
//           List <Shipment> allShipment = Shipment.Getall();
//           return View(allShipment);
//         }
        [HttpGet("/shipments/new")]
        public ActionResult New()
        {
          return View();
        }
//
//   }
// }
