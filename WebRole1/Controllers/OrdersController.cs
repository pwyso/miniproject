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
        public ActionResult Details(string id)
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

        // GET: Orders/Create
        //public ActionResult Create()
        //{
        //    ViewBag.CarRegNo = new SelectList(db.Cars, "CarRegNo", "CarMake");
        //    ViewBag.CustPhone = new SelectList(db.Customers, "CustPhone", "CustFirstName");
        //    return View();
        //}

        [HttpGet]
        public ActionResult Create(string id)
        {

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Order car = db.Orders.Find(id);
            //if (car == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.Car = car;
            //return View();

            ViewBag.Car = new SelectList(db.Cars.Select(c => c.CarRegNo == id), "CarCategory", "CarRegNo", "CarMake");
            ViewBag.CustPhone = new SelectList(db.Customers, "CustPhone", "CustFirstName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,CustPhone,CarRegNo,DaysOfRental")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("AllOrders");
            }

            ViewBag.CarRegNo = new SelectList(db.Cars, "CarRegNo", "CarMake", order.CarRegNo);
            ViewBag.CustPhone = new SelectList(db.Customers, "CustPhone", "CustFirstName", order.CustPhone);
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
        public ActionResult Edit([Bind(Include = "OrderID,CustPhone,CarRegNo,DaysOfRental")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
            return RedirectToAction("Index");
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
