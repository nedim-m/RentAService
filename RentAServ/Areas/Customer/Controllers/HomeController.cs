using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentAServ.DataAccess.Data.Repository.IRepository;
using RentAServ.Extensions;
using RentAServ.Models;
using RentAServ.Models.ViewModels;
using RentAServ.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RentAServ.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HomeVM homeVM;

        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            homeVM = new HomeVM()
            {
                CategoryList = _unitOfWork.Category.GetAll(),
                ServiceList = _unitOfWork.Service.GetAll(includeProperties:"Frequency")
            };
            return View(homeVM);
        }

        public IActionResult Details(int id)
        {
            var objFromDb = _unitOfWork.Service.GetFirstOrDefault(includeProperties:"Category,Frequency",filter:c=>c.Id==id);
           
            return View(objFromDb);
        }


        public IActionResult AddToCart(int Id)
        {
            List<int> sessionList = new List<int>();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SD.SessionCart)))
            {
                sessionList.Add(Id);
                HttpContext.Session.SetObject(SD.SessionCart, sessionList);
            }
            else
            {
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                if (!sessionList.Contains(Id))
                {
                    sessionList.Add(Id);
                    HttpContext.Session.SetObject(SD.SessionCart, sessionList);
                }
            }
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
