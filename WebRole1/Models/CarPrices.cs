using System;
using System.Configuration;

namespace CarRental.Models
{
    // enums used in Order.cs and Car.cs model classes
    // forcing usage only these three car segment types
    public enum CarSegment { Small, Medium, Big }

    // all prices values are set in appSettings in Web.config
    // pased to CarPrices properties which are used in Order.cs and Car.cs model classes
    public class CarPrices
    {
        public static decimal CarPriceSmall
        {
            get { return Convert.ToDecimal(ConfigurationManager.AppSettings["CarPriceSmall"].ToString()); }
        }
        public static decimal CarPriceMedium
        {
            get { return Convert.ToDecimal(ConfigurationManager.AppSettings["CarPriceMedium"].ToString()); }
        }
        public static decimal CarPriceBig
        {
            get { return Convert.ToDecimal(ConfigurationManager.AppSettings["CarPriceBig"].ToString()); }
        }

        public static decimal DiscountThreeToFiveDays
        {
            get { return Convert.ToDecimal(ConfigurationManager.AppSettings["Discount-3-10-days"].ToString()); }
        }
        public static decimal DiscountOverTenDays
        {
            get { return Convert.ToDecimal(ConfigurationManager.AppSettings["Discount-over-10-days"].ToString()); }
        }
    }
}
