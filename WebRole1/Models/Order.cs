using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class Order
    {
        [Display(Name = "Reservation Reference No.")]
        public string OrderID { get; set; }

        public string CustPhone { get; set; }
        public string CarRegNo { get; set; }



        [Display(Name = "Total Cost")]
        public decimal TotalCost
        {
            get
            {
                if (Car.DaysOfRental <= 3)
                {
                    return Car.RentPrice * Car.DaysOfRental;
                }
                else if (Car.DaysOfRental <= 10)
                {
                    return (Car.RentPrice - 3) * Car.DaysOfRental;
                }
                else
                {
                    return (Car.RentPrice - 6) * Car.DaysOfRental;
                }
            }
        }

        public virtual Car Car { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
