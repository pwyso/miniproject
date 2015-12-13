using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class CarsApiController : ApiController
    {
        private CarRentalContext db = new CarRentalContext();

        [Route("cars/all")]
        // GET cars/all
        [ResponseType(typeof(IEnumerable<CarRental.Models.Car>))]
        public IHttpActionResult GetCars()
        {
            return Ok(db.Cars);                   // 200 OK, listings serialized in response body
        }


        // GET: api/Cars1/5
        [ResponseType(typeof(Car))]
        public async Task<IHttpActionResult> GetCarById(string id)
        {
            Car car = await db.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        // methods here to browse cars, chose car, book car
        // then Client part that will use above methods 


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarExists(string id)
        {
            return db.Cars.Count(e => e.CarRegNo == id) > 0;
        }
    }
}