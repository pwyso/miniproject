using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class Cars1Controller : ApiController
    {
        private CarRentalContext db = new CarRentalContext();

        // GET: api/Cars1
        public IQueryable<Car> GetCars()
        {
            return db.Cars;
        }

        // GET: api/Cars1/5
        [ResponseType(typeof(Car))]
        public async Task<IHttpActionResult> GetCar(string id)
        {
            Car car = await db.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        // PUT: api/Cars1/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCar(string id, Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != car.CarRegNo)
            {
                return BadRequest();
            }

            db.Entry(car).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Cars1
        [ResponseType(typeof(Car))]
        public async Task<IHttpActionResult> PostCar(Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cars.Add(car);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CarExists(car.CarRegNo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = car.CarRegNo }, car);
        }

        // DELETE: api/Cars1/5
        [ResponseType(typeof(Car))]
        public async Task<IHttpActionResult> DeleteCar(string id)
        {
            Car car = await db.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            db.Cars.Remove(car);
            await db.SaveChangesAsync();

            return Ok(car);
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