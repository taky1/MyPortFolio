using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entites;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Owner> _owner;
        private readonly IUnitOfWork<ProtfolioItem> _profolio;

        public HomeController(IUnitOfWork<Owner> owner,
              IUnitOfWork<ProtfolioItem> profolio )
        {
            _owner = owner;
            _profolio = profolio;
        }
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                Owner = _owner.Entity.GetAll().First(),
                Portfolioitems = _profolio.Entity.GetAll().ToList()
            };
            return View(homeViewModel);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}