using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Barron_Amanda_HW6.Models
{
    public class OrderDetail
    {
        public Int32 OrderDetailID { get; set; }

        [Display(Name = "Order Quantity")]
        [Range (1, 1000, ErrorMessage = "Quantity must be between 1 and 1000")]
        public Int32 OrderQuantity { get; set; }

        [Display(Name = "Order Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal OrderPrice { get; set; }

        [Display(Name = "Extended Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal ExtendedPrice { get; set; }

        /* do i do this? is this readonly? do i do another migration?
         * [Display(Name = "Extended Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]

        public Decimal _decExtendedPrice
        public Decimal ExtendedPrice 
        get {return _decExtendedPrice = Convert.ToDecimal(OrderQuantity)* OrderPrice;}

        */


        //navagation properties

        public Order Order { get; set; }
        public Product Product { get; set; }


    }
}
