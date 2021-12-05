using fooddelievryapp.Services;
using fooddeliveryapp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodDeliveryAppMVC.Controllers
{
    [Authorize]
    public class FoodController : Controller
    {
        // GET: Food
        public ActionResult Index() // Display all the food for the current user
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new FoodService(userId);
            var model = service.GetFood();
            return View(model);
        }

        // to Get the create view

        public ActionResult Create(FoodCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = CreateFoodService();
            service.CreateFood(model);
            return RedirectToAction("Index");
        }

      
        private FoodService CreateFoodService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new FoodService(userId);
            return service;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult CreateFood(FoodCreate model)
        {
            if (ModelState.IsValid) return View(model);
            var service = CreateFoodService();
            if(service.CreateFood(model))
            {
                TempData["SaveResult"] = "Your food was created";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Note could not be created.");

        
            return View(model);
        }
    }
}