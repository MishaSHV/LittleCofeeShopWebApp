using LittleCofeeShopWebApp.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LittleCofeeShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        // Flag: Has Dispose already been called?
        bool disposed = false;
        ICofeeRepository repository;

        public HomeController(ICofeeRepository cofeeRepository)
        {
            repository = cofeeRepository;
        }
        // GET: Home
        public ActionResult Index()
        {
            return View(repository.Products);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                repository.Dispose();
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
            base.Dispose(disposing);
        }
    }
}