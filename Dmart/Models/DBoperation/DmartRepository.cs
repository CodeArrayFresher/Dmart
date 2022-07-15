using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using Dmart.Models;
using System.Data;

namespace Dmart.Models.DBoperation
{
    public class DmartRepository
    {
        private SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["Dmart"].ToString();
            con = new SqlConnection(constr);
        }
       


        public List<OrderModel> GetOrders ()
        {
            connection();
            List<OrderModel> OrderList = new List<OrderModel>();

            SqlCommand cmd = new SqlCommand("OrderDetailsList",con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                OrderList.Add(new OrderModel()
                {
                    OrderId = Convert.ToInt32(dr["OrderID"]),
                    OrderDate = Convert.ToDateTime(dr["OrderDate"]),
                    CustomerName = Convert.ToString(dr["CustomerName"])    

                });
            }
            return OrderList;
        }

        public List<ProductModel> GetProducts()
        {
            connection();
            List<ProductModel> ProductList = new List<ProductModel>();

            SqlCommand cmd = new SqlCommand("ProductDetailList", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                ProductList.Add(new ProductModel()
                {
          
                  OrderId= Convert.ToInt32(dr["OrderId"]),
                    ProductId = Convert.ToInt32(dr["ProductId"]),
                    Name = Convert.ToString(dr["ProductName"]),
                  //Quantity = Convert.ToInt32(dr["quantity"]),
                  unitPrice = Convert.ToDouble(dr["UnitPrice"]),
                  TotalAmount = Convert.ToDouble(dr["Amount"])

                });
            }
            return ProductList;
        }
    }
}
