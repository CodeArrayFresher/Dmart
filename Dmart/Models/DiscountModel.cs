using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dmart.Models
{
    public class DiscountModel
    {
        public int ProductId { get; set; }
        public decimal getdiscountvalue { get; set; }
        public int unitprice { get; set; }
        public string ProductName { get; set; }
        public int DiscountValue { get; set; }
        public int DiscountType { get; set; }
        [DataType(DataType.Date)]
        public DateTime EffectiveFromDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EffectiveToDate { get; set; }
        public int Total_Discount { get; set; }
        public List<DiscountModel> discount_master { get; set; }
        public List<DiscountType> Add_Discount_type = new List<DiscountType>()
        {
            new DiscountType() {id = 0, name = "Number" },
            new DiscountType() {id = 1, name = "Percentage" }
        };
        public List<ProductModel>productList { get; set; }
        public List<DiscountModel> Add_Discount { get; set; }
    }

    public class DiscountType
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}