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
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
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
        public int productPrice { get; set; }
        public List<ProductModel> showproducttoedit { get; set; }
       public List<ProductModel> ProductList { get; set; }
       public List<ProductModel> UpdatedOrderList { get; set; }
        public List<CustomerModel> CustomerList { get; set; }
        public OrderModel OrderModels { get; set; }
        public List<ProductModel> InsertOrderList { get; set; }
    }
}