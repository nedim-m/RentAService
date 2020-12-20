using Microsoft.AspNetCore.Mvc;
using RentAServ.DataAccess.Data.Repository;
using RentAServ.DataAccess.Data.Repository.IRepository;
using RentAServ.Extensions;
using RentAServ.Models;
using RentAServ.Models.ViewModels;
using RentAServ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentAServ.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfwork;

        [BindProperty]
        public CartViewModel CartVM { get; set; }
        
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfwork = unitOfWork;
            CartVM = new CartViewModel()
            {
                OrderHeader=new OrderHeader(),
                ServiceList= new List<Service>()
            };
        }

        public IActionResult Index()
        {
           if (HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                foreach(int serviceId in sessionList)
                {
                    CartVM.ServiceList.Add(_unitOfwork.Service.GetFirstOrDefault(u => u.Id == serviceId,includeProperties:"Frequency,Category"));

                }
            }
            return View(CartVM);
        }


        public IActionResult Summary()
        {
            if (HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                foreach (int serviceId in sessionList)
                {
                    CartVM.ServiceList.Add(_unitOfwork.Service.GetFirstOrDefault(u => u.Id == serviceId, includeProperties: "Frequency,Category"));

                }
            }
            return View(CartVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public IActionResult SummaryPOST()
        {
            if (HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                foreach (int serviceId in sessionList)
                {
                    CartVM.ServiceList.Add(_unitOfwork.Service.GetFirstOrDefault(u => u.Id == serviceId, includeProperties: "Frequency,Category"));

                }
            }
            if (ModelState.IsValid)
            {
                return View(CartVM);
            }
            else
            {
                CartVM.OrderHeader.OrderDate = DateTime.Now;
                CartVM.OrderHeader.Status = SD.StatusSubmitted;
                CartVM.OrderHeader.ServiceCount = CartVM.ServiceList.Count;
                _unitOfwork.OrderHeader.Add(CartVM.OrderHeader);
                _unitOfwork.Save();

                foreach (var item in CartVM.ServiceList)
                {
                    OrderDetails orderDetails = new OrderDetails
                    {
                        ServiceId = item.Id,
                        OrderHeaderId = CartVM.OrderHeader.Id,
                        ServiceName = item.Name,
                        Price=item.Price
                    };
                    _unitOfwork.OrderDetails.Add(orderDetails);
                    

                   
                }
                _unitOfwork.Save();
                HttpContext.Session.SetObject(SD.SessionCart, new List<int>());
                return RedirectToAction("OrderConfrimation", "Cart", new { id = CartVM.OrderHeader.Id });
            }
        }

        public IActionResult OrderConfrimation(int id)
        {
            return View(id);
        }

        public IActionResult Remove(int id)
        {
            List<int> sessionList = new List<int>();
            sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
            sessionList.Remove(id);

            HttpContext.Session.SetObject(SD.SessionCart, sessionList);

            return RedirectToAction(nameof(Index));

        }
    }
}
