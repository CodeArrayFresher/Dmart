using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Dmart.Models
{
    public class ProductModel
    {
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> OrderDate { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; } 
        public int OrderId { get; set; }
        public string Name { get; set; } 
        public int Quantity { get; set; }
        public double unitPrice { get; set; }
        public double TotalAmount { get; set; }
        public List<ProductModel> showproducttoedit { get; set; }
       public List<ProductModel> ProductList { get; set; }
       public List<ProductModel> UpdatedOrderList { get; set; } //this also shows updated product price
        public List<CustomerModel> CustomerList { get; set; }
        public List<ProductModel> InsertOrderList { get; set; }
        public int UpdatedPrice { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> FromDate { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ToDate { get; set; }
    }
}