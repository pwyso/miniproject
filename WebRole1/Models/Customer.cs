using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Models
{
    public class Customer
    {
        // Primary Key
        [Key]
        //lets you enter the primary key for the CustPhone rather than having the database generate it
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        // validate phone number: only digits allowed 8 to 12, eg. 018268043, 035386112233
        [RegularExpression("[0-9]{8,12}", ErrorMessage = "No spaces allowed, max. 12 digits.")]    // numbers only, lenght 9-11
        [Display(Name = "Phone No.")]
        public string CustPhone { get; set; }

        // null not allowed
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
        // validate input format for email
        [DataType(DataType.EmailAddress)]
        public string CustEmail { get; set; }

        // Foreign Key of the associative Order table/model class
        public virtual ICollection<Order> Orders { get; set; }
    }

}
