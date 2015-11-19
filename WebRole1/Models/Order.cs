﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class Order
    {
        [Display(Name = "Reservation Reference No.")]
        public string OrderID { get; set; }
        public string CustomerID { get; set; }
        public string CarRegNo { get; set; }

        [Display(Name = "Total Cost")]
        public decimal TotalCost { get; set; }

        public virtual Car Car { get; set; }
        public virtual Customer Customer { get; set; }
    }
}