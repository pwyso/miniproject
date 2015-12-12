using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CarRental.Models
{
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
        [StringLength(20, ErrorMessage = "Max. length is 20")]
        public string CarMake { get; set; }

        [Required]
        [Display(Name = "Model")]
        [StringLength(20, ErrorMessage = "Max. length is 20")]
        public string CarModel { get; set; }


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


        [Display(Name = "Availability")]
        public bool IsHired { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}
