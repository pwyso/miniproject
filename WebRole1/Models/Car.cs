using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Models
{
    public enum CarSegment { Small, Medium, Big }
    public enum PricePerRentingDays { OverSeven = 35, UpToSeen = 42, UpToThree = 50 }
    public class Car
    {

        [Key]
        //lets you enter the primary key for the CarRegNo rather than having the database generate it
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Registration")]
        [StringLength(11, ErrorMessage = "Max. lenght is 11")]
        //Reg. plates validation - 2/3 digits 1/2 capital letters 1-6 digits, eg. 151WX123456
        [RegularExpression("^[0-9]{2,3}[A-Z]{1,2}[0-9]{1,6}", ErrorMessage = "Invalid format: no spaces, capital letters only.")]
        public string CarRegNo { get; set; }

        [Required]
        [Display(Name = "Car Category")]
        public CarSegment CarCategory { get; set; }

        [Required]
        [Display(Name = "Make")]
        [StringLength(20, ErrorMessage = "Max. lenght is 20")]
        public string CarMake { get; set; }

        [Required]
        [Display(Name = "Model")]
        [StringLength(20, ErrorMessage = "Max. lenght is 20")]
        public string CarModel { get; set; }


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
        public bool IsHired { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}
