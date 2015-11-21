using System;
using System.Collections;
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

        [Key]
        //lets you enter the primary key for the CarRegNo rather than having the database generate it
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        [Display(Name = "Registration")]
        [RegularExpression("[0-9A-Z]")]      // numbers and capital letters only
        [StringLength(10, ErrorMessage = "Max. lenght is 10")]
        public string CarRegNo { get; set; }

        [Required]
        [Display(Name = "Car Category")]
        public CarSegment CarCategory { get; set; }

        [Required]
        [Display(Name = "Make")]
        public string CarMake { get; set; }

        [Required]
        [Display(Name = "Model")]
        public string CarModel { get; set; }

        [Display(Name = "Days of rent")]
        [Range(1, 30)]
        public int DaysOfRental { get; set; }

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
        }

        
        [Display(Name = "Availability")]
        public bool IsHired { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}
