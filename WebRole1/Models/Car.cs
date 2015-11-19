using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Models
{
    public enum CarSegment { Small, Medium, Big }
    //public enum PricePerRentingDays { UpToThree = 50, UpToSeen = 42, OverSeven = 35}
    public class Car
    {
        //[Key]
        //public string CarID { get; set; }

        [Key]
        //lets you enter the primary key for the CarRegNo rather than having the database generate it
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        [Display(Name = "Registration")]
        public string CarRegNo { get; set; }

        [Required]
        [Display(Name = "Car Category")]
        public CarSegment CarCategory { get; set; }

        [Required]
        [Display(Name = "Price per Day")]
        public decimal RentPrice
        {
            get
            {
                if (DaysOfRental <= 3)
                {
                    return 50;
                }
                else if (DaysOfRental <= 7)
                {
                    return 43;
                }
                else
                {
                    return 35;
                }
            }
            set
            {

            }
        }

        [Display(Name = "Days of rent")]
        public int DaysOfRental { get; set; }

        
        [Display(Name = "Availability")]
        public bool IsHired { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
