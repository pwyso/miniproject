using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Models
{
    public class CarOrder
    {
        public Car SelectedCar { get; set; }
        public Order CustOrder { get; set; }
        public Customer Customer { get; set; }
    }
}
