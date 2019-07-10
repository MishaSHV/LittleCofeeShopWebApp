using LittleCofeeShopWebApp.Domain.Abstract;
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
    }
}