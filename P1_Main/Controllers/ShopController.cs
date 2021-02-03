using LogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer;
using ModelLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace P1_Main.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        private readonly LogicClass _logicClass;
        public ShopController(ILogger<ShopController> logger, LogicClass logicClass)
        {
            _logicClass = logicClass;
            _logger = logger;
        }
        // GET: ShopInventoryController

        public ActionResult Shop()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (claimsIdentity == null || claimsIdentity.IsAuthenticated == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = _logicClass.GetCurrentUser(claimsIdentity);
            List<LocationInventoryViewModel> livmList = _logicClass.DisplayLocationInventory(user);
            return View("ShopView", livmList);
        }

        // GET: ShopInventoryController/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Details")]
        public ActionResult Details(int id)
        {
            int productId = id;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (claimsIdentity == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<LocationInventoryViewModel> livmList = _logicClass.DisplayLocationInventory(_logicClass.GetCurrentUser(claimsIdentity));
            LocationInventoryViewModel livm = livmList.FirstOrDefault(x => x.ProductId == productId);
            return View("ItemDetailView", livm);
        }

        // GET: ShopInventoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("AddToCart")]
        public ActionResult AddToCart(LocationInventoryViewModel livm)
        {
            if (!ModelState.IsValid)
            {
                return View("ShopView");
            }
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (claimsIdentity == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = _logicClass.GetCurrentUser(claimsIdentity);
            List<LocationInventoryViewModel> livmList = _logicClass.DisplayLocationInventory(user);
            livmList = _logicClass.AddToCart(livmList, livm, user);
            return RedirectToAction("Shop");
        }

        // POST: ShopInventoryController/Create
        //[HttpGet]
        //[ValidateAntiForgeryToken]
        [ActionName("EmptyCart")]
        public ActionResult EmptyCart()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (claimsIdentity == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = _logicClass.GetCurrentUser(claimsIdentity);
            _logicClass.EmptyCurrentCart(user);
            return RedirectToAction("Shop");
        }

        // POST: ShopInventoryController/Edit/5
        [ActionName("CurrentCart")]
        public ActionResult CurrentCart()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (claimsIdentity == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = _logicClass.GetCurrentUser(claimsIdentity);
            var ivmList = _logicClass.CreateInvoiceList(user);
            decimal total = ivmList.Sum(x => x.LineTotal);
            ViewBag.CartTotal = total;
            return View("CurrentCartView", ivmList);
        }

        // GET: ShopInventoryController/Delete/5
        [ActionName("CheckOut")]
        public ActionResult CheckOut()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (claimsIdentity == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = _logicClass.GetCurrentUser(claimsIdentity);
            //var ivmList = _logicClass.CreateInvoiceList(user);
            var order = _logicClass.PlaceOrder(user);
            var oivmList = _logicClass.CreateOrderInvoiceList(user, order);
            decimal total = oivmList.Sum(x => x.LineTotal);
            ViewBag.OrderDate = order.OrderTime;
            ViewBag.OrderTotal = total;
            return View("OrderView", oivmList);
        }
        [ActionName("OrderDetails")]
        public ActionResult OrderDetails(Guid id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (claimsIdentity == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = _logicClass.GetCurrentUser(claimsIdentity);
            //var ivmList = _logicClass.CreateInvoiceList(user);
            var order = _logicClass.GetOrderById(id);
            var oivmList = _logicClass.CreateOrderInvoiceList(user, order);
            decimal total = oivmList.Sum(x => x.LineTotal);

            ViewBag.OrderDate = order.OrderTime;
            ViewBag.OrderTotal = total;
            return View("OrderView", oivmList);
        }
        // POST: ShopInventoryController/Delete/5
        [ActionName("UserOrderHistory")]
        public ActionResult UserOrderHistory()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (claimsIdentity == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = _logicClass.GetCurrentUser(claimsIdentity);
            //var ivmList = _logicClass.CreateInvoiceList(user);
            //var oivmList = _logicClass.CreateOrderInvoiceList(user);
            //decimal total = oivmList.Sum(x => x.LineTotal);
            //var order = _logicClass.GetOrderById(oivmList.FirstOrDefault(x => x.Id != null).OrderId);
            var orders = _logicClass.GetUserOrderHistory(user);
            //ViewBag.OrderDate = order.OrderTime;
            //ViewBag.OrderTotal = total;
            return View("UserOrderHistoryView", orders);
        }
        [ActionName("LocationOrderHistory")]
        public ActionResult LocationOrderHistory()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (claimsIdentity == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = _logicClass.GetCurrentUser(claimsIdentity);
            //var ivmList = _logicClass.CreateInvoiceList(user);
            //var oivmList = _logicClass.CreateOrderInvoiceList(user);
            //decimal total = oivmList.Sum(x => x.LineTotal);
            //var order = _logicClass.GetOrderById(oivmList.FirstOrDefault(x => x.Id != null).OrderId);
            var orders = _logicClass.GetLocationOrderHistory(user);
            //ViewBag.OrderDate = order.OrderTime;
            //ViewBag.OrderTotal = total;
            return View("LocationOrderHistoryView", orders);
        }
    }
}
