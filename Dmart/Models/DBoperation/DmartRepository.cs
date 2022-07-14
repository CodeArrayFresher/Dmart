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
        public List<OrderDetail> GetOrderDetails()
        {
            connection();
            List<OrderDetail> OrderList = new List<OrderDetail>();
            SqlCommand cmd = new SqlCommand("OrderDetail", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                OrderList.Add(
                    new OrderDetail()
                    {
                        Id = Convert.ToInt32(dr["ID"]),
                        OrderDate = Convert.ToDateTime(dr["OrderDate"]),
                        CustomerName = Convert.ToString(dr["Name"]),
                        Quanity = Convert.ToInt32(dr["Quatity"]),
                        TotalAmount = Convert.ToInt32(dr["Amount"]),
                          ProductId = Convert.ToInt32(dr["Product_id"]),
                              ProductName = Convert.ToString(dr["ProductName"]),
         UnitPrice = Convert.ToInt32(dr["UnitPrice"]),
                    }
                    );
            }
            return OrderList;
        }

        public List<Customer> GetCustomer()
        {
            connection();
            List<Customer> Customerlist = new List<Customer>();
            SqlCommand cmd = new SqlCommand("Order", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                Customerlist.Add(
                    new Customer()
                    {
                        Id = Convert.ToInt32(dr["order_id"]),
                        OrderDate = Convert.ToDateTime(dr["OrderDate"]),
                       customer.CustomerName = Convert.ToString(dr["Name"]),
                    }
                    );
            }
            return Customerlist;
        }
    }
}
