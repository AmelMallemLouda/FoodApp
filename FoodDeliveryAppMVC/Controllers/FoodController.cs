using fooddeliveryapp.Models;
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
        public ActionResult Index()
        {
            var model = new FoodListItem[0];
            return View(model);
        }

        // to Get the create view

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(FoodCreate model)
        {
            if(ModelState.IsValid)
            {

            }
            return View(model);
        }
    }
}