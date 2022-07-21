using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Dmart.Models
{
    public class Formdata
    {
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> OrderDate { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int Total_Amount { get; set; }
   

    }
}