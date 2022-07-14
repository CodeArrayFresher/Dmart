using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dmart.Models
{
    public class OrderDetail
    {

        public int ProductId { get; set; }
       

        public string ProductName { get; set; }
        public int Quanity { get; set; }
        public int TotalAmount { get; set; }
        public int UnitPrice { get; set; }

        public List<Order> OrderList { get; set; }

    }
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

    }
}