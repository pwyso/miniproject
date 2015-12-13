﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class Order
    {
        // Primary Key - automaticly generated by DB, starts from 1, increments by 1
        [Display(Name = "Reserv. No.")]
        public int OrderID { get; set; }

        [Display(Name = "Time")]
        public DateTime OrderTime { get { return DateTime.Now; } }

        // Foreign Key of Car table/model class
        [Display(Name = "Registration")]
        public string CarRegNo { get; set; }

        [Display(Name = "Days of rent")]
        [Range(1, 30)]
        public int DaysOfRental { get; set; }

        [Display(Name = "Car Category")]
        public CarSegment CarCategory { get; set; }

        // prices set in appSettings in Web.config
        // passed to CarPrices.cs model class
        [Display(Name = "Price per Day")]
        public decimal RentPrice
        {
            get
            {
                if (CarCategory == CarSegment.Small)
                {
                    return CarPrices.CarPriceSmall;
                }
                else if (CarCategory == CarSegment.Medium)
                {
                    return CarPrices.CarPriceMedium;
                }
                else
                {
                    return CarPrices.CarPriceBig;
                }
            }
            set { }
        }

        // prices set in appSettings in Web.config
        // passed to CarPrices.cs model class
        [Display(Name = "Total Cost")]
        public decimal TotalCost
        {
            get
            {
                if (DaysOfRental <= 3)
                {
                    return RentPrice * DaysOfRental;
                }
                else if (DaysOfRental <= 10)
                {
                    return (RentPrice - CarPrices.DiscountThreeToFiveDays) * DaysOfRental;
                }
                else
                {
                    return (RentPrice - CarPrices.DiscountOverTenDays) * DaysOfRental;
                }
            }
        }

        // Foreign Key of Customer table/modell class
        [Display(Name = "Phone No.")]
        public string CustPhone { get; set; }

        // create associative tables for (Car table)One<=>Many(Order table)Many<=>One(Customer table)
        public virtual Car Car { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
