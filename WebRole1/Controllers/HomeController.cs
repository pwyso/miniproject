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
        private CarRentalContext db = new CarRentalContext();

        // GET ../Home/Index    default controller
        public ActionResult Index()
        {
            return View();
        }

        // GET  ../Home/allcustomers  displays all customers
        [HttpGet]                                       // GET only
        public ActionResult AllCustomers()
        {
            return View();                              // strongly typed view, don't need though to pass in a customer object
        }

        // POST  ../Home/addcustomer   add customer to the list
        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
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

        // DELETE  ../Home/deletecustomer   delete customer from the list
        [HttpPost]
        public ActionResult DeleteCustomer(Customer customer)
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
        // display details of the customer just added/deleted/booked 
        public ActionResult ConfirmCustomer(Customer customer)
        {
            return View(customer);
        }

        // GET  ../Home/allcars   displays all cars
        [HttpGet]                                       // GET only
        public ActionResult AllCars()
        {
            var records = db.Cars.OrderBy(r => r.CarMake);
            
            return View(records);                              // strongly typed view, don't need though to pass in a car object
        }

        // GET  ../Home/availablecars   displays available cars only
        [HttpGet]                                       // GET only
        public ActionResult AvailableCars()
        {
            //todo
            return View();                              
        }

        // POST  ../Home/addcar   add car to the list
        [HttpPost]
        public ActionResult AddCar(Car car)
        {
            if (ModelState.IsValid)                     // check server-side validation
            {
                var record = db.Cars.FirstOrDefault(c => c.CarRegNo == car.CarRegNo);
                if (record == null)
                {
                    db.Cars.Add(car);
                    db.SaveChanges();

                    return RedirectToAction("Confirm", car);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        // DELETE  ../Home/deletecar   delete car from the list
        [HttpPost]
        public ActionResult DeleteCar(Car car)
        {
            if (ModelState.IsValid)                     // check server-side validation
            {
                var record = db.Cars.FirstOrDefault(c => c.CarRegNo == car.CarRegNo);
                if (record == null)
                {
                    db.Cars.Remove(record);
                    db.SaveChanges();

                    return RedirectToAction("Confirm", car);
                }
                else
                {
                    return View();
                }

            }
            else
            {
                return View();
            }
        }

        // display details of the car just added/deleted/booked 
        public ActionResult ConfirmCar(Car car)
        {
            return View(car);
        }

        // GET  ../Home/allorders   displays all arders
        [HttpGet]                                       // GET only
        public ActionResult AllOrders()
        {
            return View();                              // strongly typed view, don't need though to pass in a car object
        }

        // POST  ../Home/addorder   add order to the list
        [HttpPost]
        public ActionResult NewOrder(Order order)
        {
            if (ModelState.IsValid)                     // check server-side validation
            {
                return RedirectToAction("Confirm", order);
            }
            else
            {
                return View();
            }
        }
        // DELETE  ../Home/deleteorder   delete order from the list
        [HttpPost]
        public ActionResult DeleteOrder(Order order)
        {
            if (ModelState.IsValid)                     // check server-side validation
            {
                return RedirectToAction("Confirm", order);
            }
            else
            {
                return View();
            }
        }

        // display details of the order just added/deleted/booked 
        public ActionResult ConfirmOrder(Order order)
        {
            return View(order);
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