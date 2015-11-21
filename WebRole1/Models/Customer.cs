using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Models
{
    public class Customer
    {
        [Key]
        //lets you enter the primary key for the CarRegNo rather than having the database generate it
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("[0-9]")]      // numbers and capital letters only
        [StringLength(10, ErrorMessage = "Max. lenght is 10")]
        [Display(Name = "Phone No.")]
        public string CustPhone { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string CustFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string CustLastName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string CustAddress { get; set; }


        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string CustEmail { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }

}
