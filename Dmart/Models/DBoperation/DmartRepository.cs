﻿using System;
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

        public List<ProductModel> GetProducts(string[] id)
        {
            string IDS = "";
            foreach (var ids in id)
            {
                IDS += ids.ToString() + ',';
            }
            var a = IDS.Remove(IDS.Length - 1, 1);
            connection();
            List<ProductModel> ProductList = new List<ProductModel>();

            SqlCommand cmd = new SqlCommand("ProductDetailList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ids", a);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                ProductList.Add(new ProductModel()
                {
                    OrderId = Convert.ToInt32(dr["OrderId"]),
                    ProductId = Convert.ToInt32(dr["ProductId"]),
                    Name = Convert.ToString(dr["Name"]),
                    Quantity = Convert.ToInt32(dr["Quantity"]),
                    unitPrice = Convert.ToDouble(dr["UnitPrice"]),
                

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
        public List<ProductModel> GetAllProducts(DateTime date)
        {
            connection();
            List<ProductModel> ProductList = new List<ProductModel>();

            SqlCommand cmd = new SqlCommand("GetAllProducts", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dates", date);
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
        public DiscountModel GetProductPrice(int id, DateTime date)
        {
            connection();

            var res = new DiscountModel();
            SqlCommand cmd = new SqlCommand("ProductPrice", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@dates", date);

            cmd.Parameters.Add("@unitpricee", SqlDbType.Int);
            cmd.Parameters.Add("@discounttypeout", SqlDbType.Int);
            cmd.Parameters.Add("@discountvalueout", SqlDbType.Int);
            cmd.Parameters["@unitpricee"].Direction = ParameterDirection.Output;
            cmd.Parameters["@discounttypeout"].Direction = ParameterDirection.Output;
            cmd.Parameters["@discountvalueout"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            res.DiscountType = (cmd.Parameters["@discounttypeout"].Value) is DBNull  ? false : Convert.ToBoolean(cmd.Parameters["@discounttypeout"].Value);
            res.getdiscountvalue = (cmd.Parameters["@discountvalueout"].Value) is DBNull ? 0 : Convert.ToInt32(cmd.Parameters["@discountvalueout"].Value);
            res.unitprice = Convert.ToInt32(cmd.Parameters["@unitpricee"].Value);
            con.Close();

            return res;
        }

       


        public void AddData(ProductModel model, int cust)
        {
            var orderId = model.OrderId;
            var orderDate = model.OrderDate;
            var customerid = cust;
            var output = JsonConvert.SerializeObject(model.InsertOrderList.Select(x => new { x.ProductId, x.Quantity, x.unitPrice }));
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

        public void EditData(ProductModel model,int id)
        {
            var orderid = id;
            var JsonData = JsonConvert.SerializeObject(model.UpdatedOrderList.Select(x => new {orderid, x.ProductId, x.Quantity, x.unitPrice, }));
            connection();
            con.Open();
            using (var command = new SqlCommand("UpdateData", con))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@JsonData", JsonData);
                command.ExecuteNonQuery();
                con.Close();
            }

            //var output = JsonConvert.SerializeObject(model.OrderList.Select(x => new { x.product.ProductId, x.product.Quantity, x.product.unitPrice }));
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


        public void UpdatePrice(ProductModel model)
        {
            var dates = model.FromDate;
            var obj = new 
            {
                ProductId = model.ProductId,
               UpdatedPrice = model.UpdatedPrice,
                FromDate = model.FromDate
            };
          
            //var output = JsonConvert.SerializeObject(model(x => new { x.ProductId, x.UpdatedPrice, x.FromDate}));
            var output = JsonConvert.SerializeObject(obj);
            connection();
            con.Open();
            using (var command = new SqlCommand("UpdateProductPrice", con))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@JsonData", output);
                command.Parameters.AddWithValue("@Dates", dates);
                command.ExecuteNonQuery();
                con.Close();
                
            }
        }
        public List<ProductModel> ProductHistory(string[] id)
        {
            string IDS = "";
            foreach (var ids in id)
            {
                IDS += ids.ToString() + ',';
            }
            var a = IDS.Remove(IDS.Length - 1, 1);
            connection();
            List<ProductModel> ProductList = new List<ProductModel>();

            SqlCommand cmd = new SqlCommand("ProductHistory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ids", a);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                ProductList.Add(new ProductModel()
                {
                    ProductId = Convert.ToInt32(dr["ProductId"]),
                    unitPrice = Convert.ToDouble(dr["Price"]),
                    FromDate = Convert.ToDateTime(dr["EffectiveFromDate"]),
                    TODATES = Convert.ToString(dr["EffectiveToDate"])

                });
            }
            return ProductList;
        }

        public void AddDiscount(DiscountModel model)
        {
            //var orderid = id;
            var JsonData = JsonConvert.SerializeObject(model.Add_Discount.Select(x => new { x.ProductId,x.DiscountValue, x.DiscountType, x.EffectiveFromDate, x.EffectiveToDate}));
            connection();
            con.Open();
            using (var command = new SqlCommand("AddDiscount", con))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@JsonData", JsonData);
                command.ExecuteNonQuery();
                con.Close();
            }

            //var output = JsonConvert.SerializeObject(model.OrderList.Select(x => new { x.product.ProductId, x.product.Quantity, x.product.unitPrice }));
        }

        public List<DiscountModel> DiscountHome()
        {
            connection();
            List<DiscountModel> DiscountList = new List<DiscountModel>();
            SqlCommand cmd = new SqlCommand("GetDiscountPrice", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                DiscountList.Add(new DiscountModel()
                {
                    ProductName = Convert.ToString(dr["Name"]),
                    DiscountValue = Convert.ToDecimal(dr["Discount_value"]),
                    DiscountType = Convert.ToBoolean(dr["Discount_type"]),
                    EffectiveFromDate = Convert.ToDateTime(dr["EffectiveFromDate"]),
                    EffectiveToDate = Convert.ToDateTime(dr["EffectiveToDate"])

                });
            }
            return DiscountList;
        }


    }
}
