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
        public string OrderID { get; set; }

        [Display(Name = "Phone No.")]
        [RegularExpression("[0-9]{1,11}", ErrorMessage = "No spaces allowed, max. 11 digits.")]    // numbers only, lenght 9-11
        public string CustPhone { get; set; }

        [Display(Name = "Registration")]
        //[StringLength(11, ErrorMessage = "Max. lenght is 11")]
        //Reg. plates validation - 2/3 digits 1/2 capital letters 1-6 digits, eg. 151WX123456
        //[RegularExpression("^[0-9]{2,3}[A-Z]{1,2}[0-9]{1,6}", ErrorMessage = "Invalid format: no spaces, capital letters only.")]
        public string CarRegNo { get; }

        //[Required]
        [Display(Name = "Car Category")]
        public CarSegment CarCategory { get; }

        //[Required]
        [Display(Name = "Make")]
        //[StringLength(20, ErrorMessage = "Max. lenght is 20")]
        public string CarMake { get;}

        //[Required]
        [Display(Name = "Model")]
        //[StringLength(20, ErrorMessage = "Max. lenght is 20")]
        public string CarModel { get; }


        [Display(Name = "Price per Day")]
        public decimal RentPrice
        {
            get
            {
                if (CarCategory == CarSegment.Small)
                {
                    return 35;
                }
                else if (CarCategory == CarSegment.Medium)
                {
                    return 45;
                }
                else
                {
                    return 55;
                }
            }
        }


        [Display(Name = "Availability")]
        public bool IsHired { get;}

        [Display(Name = "Days of rent")]
        [Range(1, 30)]
        public int DaysOfRental { get; set; }

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

        public virtual Car Car { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
