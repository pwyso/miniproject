using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class OrdersController : Controller
    {
        private CarRentalContext db = new CarRentalContext();

        // GET: Orders
        public ActionResult AllOrders()
        {
            var orders = db.Orders.Include(o => o.Car).Include(o => o.Customer);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        //GET: generates the orders page
        public ActionResult GenerateOrder(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            TempData["ChosenCar"] = car;
            CarOrder co = new CarOrder();
            co.SelectedCar = car;
            return View(co);
        }



        [HttpPost, ActionName("CreateCustForOrder")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustForOrder([Bind(Include = "CustPhone,CustFirstName,CustLastName,CustAddress,CustEmail")] Customer customer)
        {
            TempData["CustomerOrder"] = customer;
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Create", "Orders");
            }

            return RedirectToAction("Create" , "Orders");
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            Car car = (Car)TempData["ChosenCar"];
            Customer cus = (Customer)TempData["CustomerOrder"];
            TempData.Keep("ChosenCar");
            TempData.Keep("CustomerOrder");
            Order co = new Order();
            co.CarRegNo = car.CarRegNo;
            co.CustPhone = cus.CustPhone;
            co.CarCategory = car.CarCategory;
            co.RentPrice = car.RentPrice;
            TempData["Order"] = co;
            return View(co);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,CarRegNo,DaysOfRental,TotalCost,CustPhone")] Order order)
        {
            Car car = (Car)TempData["ChosenCar"];
            TempData.Keep("ChosenCar");
            Customer cus = (Customer)TempData["CustomerOrder"];
            order.CarRegNo = car.CarRegNo;
            order.CustPhone = cus.CustPhone;
            order.RentPrice = car.RentPrice;
            TempData["Order"] = order;


            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                car.IsHired = true;
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ConfirmOrder");
            }

            return View(order);
        }

        [HttpGet]
        public ActionResult UpdateStatus()
        {
            Car c = (Car)TempData["ChosenCar"];
            TempData.Keep("ChosenCar");
            Car car = db.Cars.Find(c.CarRegNo);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View();
        }


        // GET: Orders
        public ActionResult ConfirmOrder()
        {
            Order ordr = (Order)TempData["Order"];
            var order = db.Orders.Find(ordr.OrderID);
            return View(order);
        }


        // GET: Orders/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarRegNo = new SelectList(db.Cars, "CarRegNo", "CarMake", order.CarRegNo);
            ViewBag.CustPhone = new SelectList(db.Customers, "CustPhone", "CustFirstName", order.CustPhone);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,CarRegNo,DaysOfRental,CustPhone,CustFirstName,CustLastName,CustAddress,CustEmail")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllOrders");
            }
            ViewBag.CarRegNo = new SelectList(db.Cars, "CarRegNo", "CarMake", order.CarRegNo);
            ViewBag.CustPhone = new SelectList(db.Customers, "CustPhone", "CustFirstName", order.CustPhone);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("AllOrders");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
