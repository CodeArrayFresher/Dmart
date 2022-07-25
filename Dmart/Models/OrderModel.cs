using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Dmart.Models;
namespace Dmart.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }


        [DataType(DataType.Date)]
        public Nullable<System.DateTime> OrderDate { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int CreatedBy { get; set; }
        public ProductModel product { get; set; }
        public List<CustomerModel> CustomerList { get; set; }
        public List<ProductModel> ProductList { get; set; }
        public List<OrderModel> OrderList { get; set; }
        public int productPrice { get; set; }
        public List<OrderDetail> showproducttoedit { get; set; }
        public string SellerName { get; set; }
        public double SellerContact { get; set; }
        public List<OrderModel> Invoice { get; set; }
        //public List<CustomerModel> CustomerModelLsit { get; set; }
        //public string allproducts { get; set; }
        //public List<OrderModel> OrderList { get; set; }
        //public List<ProductModel> ProductList { get; set; }
    }
}