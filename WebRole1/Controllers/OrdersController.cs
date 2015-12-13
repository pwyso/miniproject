using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using CarRental.Models;
using System.Threading.Tasks;

namespace CarRental.Controllers
{
    public class OrdersController : Controller
    {
        private CarRentalContext db = new CarRentalContext();

        // GET: Orders
        public async Task<ActionResult> AllOrders()
        {
            var orders = db.Orders.Include(o => o.Car).Include(o => o.Customer);
            return View(await orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        //GET: generates the orders page
        public async Task<ActionResult> GenerateOrder(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = await db.Cars.FindAsync(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            TempData["ChosenCar"] = car;                             // store Car object for use in Create method
            CarOrder co = new CarOrder();
            co.SelectedCar = car;
            return View(co);
        }



        [HttpPost, ActionName("CreateCustForOrder")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCustForOrder([Bind(Include = "CustPhone,CustFirstName,CustLastName,CustAddress,CustEmail")] Customer customer)
        {
            TempData["CustomerOrder"] = customer;                   // store Customer object for use in Create method
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                await db.SaveChangesAsync();
                return RedirectToAction("Create", "Orders");
            }

            return RedirectToAction("Create" , "Orders");
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            Car car = (Car)TempData["ChosenCar"];                   // Car object retrieved from GeneratOrder method
            Customer cus = (Customer)TempData["CustomerOrder"];     // Customer object retrieved from CreateCustForOrder method
            TempData.Keep("ChosenCar");                             // keeps Car object for later use (TempData is single use)
            TempData.Keep("CustomerOrder");                         // keeps Customer object for later use (TempData is single use)

            // stores information about chosen car to be displayed in Create view - as a Dynamic view
            ViewBag.SelectedCar = car.CarMake + "  " + car.CarModel + " , price " + car.RentPrice;
            Order co = new Order();
            co.CarRegNo = car.CarRegNo;
            co.CustPhone = cus.CustPhone;
            co.CarCategory = car.CarCategory;
            co.RentPrice = car.RentPrice;
            return View(co);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrderID,CarRegNo,DaysOfRental,TotalCost,CustPhone")] Order order)
        {
            Car car = (Car)TempData["ChosenCar"];                   // Car object retrieved from Create method
            TempData.Keep("ChosenCar");                             // keeps Car object for later use (TempData is single use)
            Customer cus = (Customer)TempData["CustomerOrder"];     // Customer object retrieved from Create method
            order.CarRegNo = car.CarRegNo;
            order.CustPhone = cus.CustPhone;
            order.RentPrice = car.RentPrice;
            TempData["Order"] = order;                              // store Order object for use in ConfirmOrder method

            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                await db.SaveChangesAsync();
                car.IsHired = true;
                db.Entry(car).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("ConfirmOrder");
            }

            return View(order);
        }


        // GET: Orders
        public async Task<ActionResult> ConfirmOrder()
        {
            Order ordr = (Order)TempData["Order"];                  // Order object retrieved from Create [POST] method
            var order = await db.Orders.FindAsync(ordr.OrderID);
            return View(order);
        }


        // GET: Orders/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
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
        public async Task<ActionResult> Edit([Bind(Include = "OrderID,CarRegNo,DaysOfRental,CustPhone,CustFirstName,CustLastName,CustAddress,CustEmail")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("AllOrders");
            }
            ViewBag.CarRegNo = new SelectList(db.Cars, "CarRegNo", "CarMake", order.CarRegNo);
            ViewBag.CustPhone = new SelectList(db.Customers, "CustPhone", "CustFirstName", order.CustPhone);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Order order = await db.Orders.FindAsync(id);
            db.Orders.Remove(order);
            await db.SaveChangesAsync();
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
