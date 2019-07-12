using LittleCofeeShopWebApp.Domain.Abstract;
using LittleCofeeShopWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LittleCofeeShopWebApp.Controllers
{
    public class AdminController : Controller
    {
        private ICofeeRepository repository;
        public AdminController(ICofeeRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int cofeeId)
        {
            Cofee cofee = repository.Products
            .FirstOrDefault(p => p.CofeeId == cofeeId);
            return View(cofee);
        }

        [HttpPost]
        public ActionResult Edit(Cofee cofee)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(cofee);
                TempData["message"] = string.Format("{0} has been saved", cofee.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(cofee);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Cofee());
        }

        [HttpPost]
        public ActionResult Delete(int cofeeId)
        {
            Cofee deletedProduct = repository.DeleteProduct(cofeeId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted",
                deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }
}