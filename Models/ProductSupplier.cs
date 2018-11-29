using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Barron_Amanda_HW6.Models
{
    public class ProductSupplier
    {
        public Int32 ProductSupplierID { get; set; }

        //navagation properties
        public Product Product { get; set; }
        public Supplier Supplier { get; set; }
    }
}
