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
        [Display(Name = "Reservation No.")]
        public int OrderID { get; set; }

        [Display(Name = "Time")]
        public DateTime OrderTime { get { return DateTime.Now; } }

        [Display(Name = "Registration")]
        public string CarRegNo { get; set; }

        [Display(Name = "Days of rent")]
        [Range(1, 30)]
        public int DaysOfRental { get; set; }

        [Display(Name = "Car Category")]
        public CarSegment CarCategory { get; set; }

        [Display(Name = "Price per Day")]
        public decimal RentPrice
        {
            get
            {

                if (CarCategory == CarSegment.Small)
                {
                    return (decimal)CarPrice.Small;
                }
                else if (CarCategory == CarSegment.Medium)
                {
                    return (decimal)CarPrice.Medium;
                }
                else
                {
                    return (decimal)CarPrice.Big;
                }
            }
            set { }
        }

        [Display(Name = "Total Cost")]
        public decimal TotalCost
        {
            get
            {

                if (DaysOfRental <= 3)
                {
                    return RentPrice * DaysOfRental;
                }
                else if (DaysOfRental <= 10)
                {
                    return (RentPrice - 3) * DaysOfRental;
                }
                else
                {
                    return (RentPrice - 6) * DaysOfRental;
                }
            }
        }


        [Display(Name = "Phone No.")]
        public string CustPhone { get; set; }

        public virtual Car Car { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
