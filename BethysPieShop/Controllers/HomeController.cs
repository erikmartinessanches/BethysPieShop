using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethysPieShop.Models;
using BethysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethysPieShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;

        /* Thanks to DI, we can expect an IPieRepository at construction time,
         * but we specified in Startup that whenever an IPieRepository is 
         * required an IMockRepository will be provided to this constructor.
         * This is called constructino injection. 
         */
        public HomeController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        public IActionResult Index()
        {
            var pies = _pieRepository.GetAllPies().OrderBy(p => p.Name);
            var homeViewModel = new HomeViewModel()
            {
                Title = "Welcome to Bethany's Pie Shop",
                Pies = pies.ToList()
            };
            return View(homeViewModel);
        }

        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound(); //404

            return View(pie);
        }
    }
}