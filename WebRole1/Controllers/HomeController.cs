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
        [HttpDelete]
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

        // display details of the customer just added 
        public ActionResult ConfirmCustomer(Customer customer)
        {
            return View(customer);
        }


        // display details of the car just added
        [HttpGet]
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
        [HttpDelete]
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

        // display details of the order just added
        [HttpGet]
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