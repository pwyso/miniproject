using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class CarsController : Controller
    {
        private CarRentalContext db = new CarRentalContext();

        // GET  ../Cars/allcars   displays all cars, no Edit/Details/Delete in view
        [HttpGet]                                       // GET only
        public ActionResult AllCars()
        {
            var records = db.Cars.OrderBy(r => r.CarMake);
            return View(records);                       // strongly typed view, don't need though to pass in a car object
        }

        // GET  ../Cars/AvailableCars    displays available cars only
        [HttpGet]                                       // GET only
        public ActionResult AvailableCars()
        {
            var records = db.Cars.OrderBy(r => r.CarMake).Where(r => r.IsHired == false);
            return View(records);
        }

        // GET  ../Cars/ManageCars       displays all cars with Edit/Details/Delete in view
        [HttpGet]                                       // GET only
        public ActionResult ManageCars()
        {
            var records = db.Cars.OrderBy(r => r.CarMake);
            return View(records);                       // strongly typed view, don't need though to pass in a car object
        }

        // GET  ..Cars/Details/5
        [HttpGet]
        public ActionResult Details(string id)
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
            return View(car);
        }

        // GET  ..Cars/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST  ..Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarRegNo,CarCategory,CarMake,CarModel,IsHired")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("AllCars");
            }
            return View(car);
        }

        // GET  ..Cars/Edit/5
        public ActionResult Edit(string id)
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
            return View(car);
        }

        // POST  ..Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarRegNo,CarCategory,CarMake,CarModel,DaysOfRental,IsHired")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllCars");
            }
            return View(car);
        }

        // GET  ..Cars/Delete/5
        [HttpGet]
        public ActionResult Delete(string id)
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
            return View(car);
        }

        // POST  ..Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
            db.SaveChanges();
            return RedirectToAction("AllCars");
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
