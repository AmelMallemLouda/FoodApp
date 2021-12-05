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

        public ActionResult Details(int id)
        {
            var srv = CreateFoodService();
            var model = srv.GetFoodById(id);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var srv = CreateFoodService();
            var detail = srv.GetFoodById(id);
            var model = new FoodEdit
            {
                FoodId = detail.FoodId,
                FoodName = detail.FoodName,
                FoodPrice = detail.FoodPrice,
                FoodIngridients = detail.FoodIngridients
            };
            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditFood(int id,  FoodEdit model)
        //{
        //    if (ModelState.IsValid) return View(model);
        //    if(model.FoodId!= id)
        //    {
        //        ModelState.AddModelError("", "Id Mismatch");
        //        return View(model);
        //    }

        //    var service = CreateFoodService();
        //    if (service.UpdateFood(model))
        //    {
        //        TempData["SaveResult"] = "Your note was Updated.";
        //        return RedirectToAction("Index");
        //    }
        //    ModelState.AddModelError("", "Your Food could not be updated");

        //    return View(model);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FoodEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.FoodId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateFoodService();

            if (service.UpdateFood(model))
            {
                TempData["SaveResult"] = "Your note was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your note could not be updated.");
            return View();
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateFoodService();
            var model = svc.GetFoodById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFood(int id)
        {
            var service = CreateFoodService();

            service.DeleteFood(id);

            TempData["SaveResult"] = "Your food was deleted";

            return RedirectToAction("Index");
        }
        private FoodService CreateFoodService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new FoodService(userId);
            return service;
        }
    }
}