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

        public List<CustomerModel> GetCustomers()
        {
            connection();
            List<CustomerModel> CustomerList = new List<CustomerModel>();

            SqlCommand cmd = new SqlCommand("GetCustomer", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                CustomerList.Add(new CustomerModel()
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    Name = Convert.ToString(dr["Name"])
                });
            }
            return CustomerList;
        }
        public List<ProductModel> GetAllProducts()
        {
            connection();
            List<ProductModel> ProductList = new List<ProductModel>();

            SqlCommand cmd = new SqlCommand("GetAllProducts", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                ProductList.Add(new ProductModel()
                {
                 ProductId = Convert.ToInt32(dr["Id"]),
                 Name = Convert.ToString(dr["Name"]),
                 unitPrice = Convert.ToInt32(dr["Price"])
                });
            }
            return ProductList;
        }
        public int GetProductPrice(int id)
        {
            connection();
            int price= 0;
            SqlCommand cmd = new SqlCommand("ProductPrice", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            if(dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                price = Convert.ToInt32(row["Price"]);
            }
            return price;
        }

    }
}
