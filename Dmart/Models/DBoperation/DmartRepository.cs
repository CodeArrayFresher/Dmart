using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using Dmart.Models;
using System.Data;
using Newtonsoft.Json;

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
                    Quantity = Convert.ToInt32(dr["Quantity"]),
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
                 unitPrice = Convert.ToInt32(dr["Price"]),
                 
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

        public void AddData(OrderModel model, int cust)
        {


            var orderId = model.OrderId;
            var orderDate = model.OrderDate;
            var customerid = cust;
            var output = JsonConvert.SerializeObject(model.OrderList.Select(x => new { x.product.ProductId, x.product.Quantity, x.product.unitPrice }));

            //DataTable dt = new DataTable();
            //DataColumn dc = new DataColumn("productId", typeof(Int32));
            //dt.Columns.Add(dc); 
            //dc = new DataColumn("CustomerId", typeof(Int32));
            //dt.Columns.Add(dc);
            //dc = new DataColumn("UnitPrice", typeof(Int32));
            //dt.Columns.Add(dc);
            //dc = new DataColumn("Quantity", typeof(Int32));
            //dt.Columns.Add(dc);

            //foreach (var item in model.OrderList)
            //{
            //    DataRow dr = dt.NewRow();
            //    dr[0] = item.product.ProductId;
            //    dr[1] = customerid;
            //    dr[2] = item.product.unitPrice; 
            //    dr[3] = item.product.Quantity; 
            //    dt.Rows.Add(dr);
            //}
            connection();
            con.Open();
            using (var command = new SqlCommand("AddDataJson", con))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@JsonData", output);
                command.Parameters.AddWithValue("@customerid", customerid);


                command.ExecuteNonQuery();
                con.Close();
                //    }


            }
        }

        public List<ProductModel> ShowProductToEdit(int id)
        {
            connection();
            List<ProductModel> OrderList = new List<ProductModel>();
            SqlCommand cmd = new SqlCommand("ShowProductToEdit", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                OrderList.Add(new ProductModel()
                {   
                    ProductId = Convert.ToInt32(dr["Id"]),
                    Name = Convert.ToString(dr["productname"]),
                    Quantity = Convert.ToInt32(dr["Quatity"]),
                    unitPrice = Convert.ToInt32(dr["UnitPrice"]),
                    TotalAmount = Convert.ToInt32(dr["Amount"])
                }); 
            }
            return OrderList;
        }

        public List<OrderModel> Invoice (int id)
        {
            connection();
            List<OrderModel> Invoice = new List<OrderModel>();
            SqlCommand cmd = new SqlCommand("Invoice", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                Invoice.Add(new OrderModel()
                {
                    
                   OrderId = Convert.ToInt32(dr["OrderNumber"]),
                    OrderDate = Convert.ToDateTime(dr["OrderDate"]),
                    CustomerName = Convert.ToString(dr["CustomerName"]),
                    SellerName = Convert.ToString(dr["SellerName"]),
                    SellerContact = Convert.ToDouble(dr["ContactNumber"])
                });
            }
            return Invoice;
        }
    }
}
