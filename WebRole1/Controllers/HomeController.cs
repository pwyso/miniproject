using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]                                       // GET only
        public ActionResult Register()
        {
            return View();                              // strongly typed view, don't need though to pass in a student object
        }

        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            if (ModelState.IsValid)                     // check server-side validation
            {
                return RedirectToAction("Confirm", customer);
            }
            else
            {
                return View();
            }
        }

        // display details of the customer just registered
        public ActionResult Confirm(Customer customer)
        {
            return View(customer);
        }
        // ../Home/... is default URI              // 
        [HttpGet]                                       // GET only
        public ActionResult AllCars()
        {
            return View();                              // strongly typed view, don't need though to pass in a student object
        }

        [HttpPost]
        public ActionResult BookCar(Car car)
        {
            if (ModelState.IsValid)                     // check server-side validation
            {
                return RedirectToAction("Confirm", car);
            }
            else
            {
                return View();
            }
        }

        // display details of the car just booked
        public ActionResult Confirm(Car car)
        {
            return View(car);
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}