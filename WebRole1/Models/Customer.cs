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
        public string CustomerID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string CusFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string CusLastName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string CustAddress { get; set; }


        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string CustEmail { get; set; }

        [Required]
        [Display(Name = "Phone No.")]
        public string CustPhone { get; set; }

        public virtual List<Order> Orders { get; set; }
    }

}
