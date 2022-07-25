using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dmart.Models
{
    public class Seller
    {
       public int Id { get; set; }
        public string Name { get; set; }
        public double Contact { get; set; }
        public CustomerModel Customer { get; set; }
    }
}