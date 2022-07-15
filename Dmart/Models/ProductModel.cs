using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dmart.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; } 
        public int OrderId { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateUpdatef { get; set; }
        public int UpdatedBy { get; set; }
        public int Quantity { get; set; }
        public double unitPrice { get; set; }
        public double TotalAmount { get; set; }

    }
}