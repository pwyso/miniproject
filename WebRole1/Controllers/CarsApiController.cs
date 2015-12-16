using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class CarsApiController : ApiController
    {
        private CarRentalContext db = new CarRentalContext();

        // GET ../allcars                          display all cars
        [Route("allcars")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<CarRental.Models.Car>))]
        public IHttpActionResult AllCars()
        {
            var records = db.Cars.ToList();
            if (records.Count() > 0)
            {
                return Ok(records.ToList());
            }
            else { return NotFound(); }
        }

        // GET: ../availablecars                    display all available cars
        [Route("availablecars")]
        [HttpGet]                                    
        public IHttpActionResult AvailableCars()
        {
            var records = db.Cars.OrderBy(r => r.CarMake).Where(r => r.IsHired == false);
            if (records.Count() > 0)
            {
                return Ok(records.ToList());
            }
            else { return NotFound(); }
        }

        // GET: ../hiredcars                        display all hired cars
        [Route("hiredcars")]
        [HttpGet]
        public IHttpActionResult HiredCars()
        {
            var records = db.Cars.OrderBy(r => r.CarMake).Where(r => r.IsHired == true);
            if (records.Count() > 0)
            {
                return Ok(records.ToList());
            }
            else { return NotFound(); }
        }

        // GET: ../availablecars/Honda               display available cars for particular car make
        [Route("availablecars/make/{make}")]
        [HttpGet]
        public IHttpActionResult AvailableCarsByMake(string make)
        {
            var records = db.Cars.OrderBy(r => r.CarMake).Where(r => r.CarMake.ToUpper() == make.ToUpper() && r.IsHired == false );
            if (records.Count() > 0)
            {
                return Ok(records.ToList());
            }
            else { return NotFound(); }
        }

        // GET: ../availablecars/segment/small               display available cars for Small/Medium/Big segment
        [Route("availablecars/segment/{segment}")]
        [HttpGet]
        public IHttpActionResult AvailableCarsBySegment(string segment)
        {
            var records = db.Cars.OrderBy(r => r.CarMake).Where(r => r.IsHired == false &&
            r.CarCategory.ToString().ToUpper() == segment.ToUpper());
            if (records.Count() > 0)
            {
                return Ok(records.ToList());
            }
            else { return NotFound(); }
        }

        // GET: ../findcar/02D2222                    find car with reg. no. 02D2222
        [Route("findcar/regno/{regno}")]
        [HttpGet]
        public IHttpActionResult FindCarByRegNo(string regno)
        {
            Car record = db.Cars.Find(regno);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

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