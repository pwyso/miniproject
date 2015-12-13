using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Models
{
    // class for creating order using GenerateOrder.cshtml view 
    // displayed selected car and form fields to put in customer details  
    public class CarOrder
    {
        public Car SelectedCar { get; set; }
        public Order CustOrder { get; set; }
        public Customer Customer { get; set; }
    }
}
