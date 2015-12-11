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
        //lets you enter the primary key for the CustPhone rather than having the database generate it
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[StringLength(10, ErrorMessage = "Max. lenght is 10")]
        //[RegularExpression("[0-9]{1,11}", ErrorMessage = "No spaces allowed, max. 11 digits.")]    // numbers only, lenght 9-11
        [Display(Name = "Phone No.")]
        public string CustPhone { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(20, ErrorMessage = "Max. lenght is 20")]
        public string CustFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(20, ErrorMessage = "Max. lenght is 20")]
        public string CustLastName { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(50, ErrorMessage = "Max. lenght is 50")]
        public string CustAddress { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string CustEmail { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }

}
