using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dmart.Models
{
    public class OrderDetail
    {
        public int ID { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quatity { get; set; }
        public decimal UnitPrice { get; set; }
        public int cust_id { get; set; }
        public string ProductName { get; set; }
        public decimal ProductAmount { get; set; }
    }
 
}