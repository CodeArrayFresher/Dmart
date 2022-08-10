using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dmart.Models;
using Dmart.Models.DBoperation;
namespace Dmart.Controllers
{
    public class HomeController : Controller
    {
        DmartRepository repo = null;
        public HomeController()
        {
            repo = new DmartRepository();
        }
        // GET: Home
        public ActionResult Index()
        {
           
            var model = new OrderModel();

            //model.ProductList = repo.GetProducts();
           
            model.OrderList = repo.GetOrders();
            return View(model);
            
        }
        //public ActionResult Jqeury()
        //{
        //    return View();  
        //}

        public ActionResult _IndexProductList(string[] id)
        {
            
            var model = new OrderModel();
            model.ProductList = repo.GetProducts(id);
            return PartialView("_IndexProductList", model);
        }

        

        public  ActionResult Insert()
        {
            DateTime date = DateTime.Now;
            var model = new ProductModel();
            model.CustomerList= repo.GetCustomers();
            model.ProductList = repo.GetAllProducts(date);
     
            return View(model);
        }


        public ActionResult _TableRow()
        {
            DateTime date = DateTime.Now;
            var model = new ProductModel();
            model.ProductList = repo.GetAllProducts(date);
            //model.Quantity = 1;
            return PartialView("_TableRow", model);
        }

        [HttpPost]
        public ActionResult InsertData(ProductModel model)
        {
            var customer = model.CustomerId;
            repo.AddData(model, customer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult GetUnitPrice(int id)
        {
            DateTime curr_date = DateTime.Now;
            var price = repo.GetProductPrice(id, curr_date);
            int unitprice = price.unitprice;
            decimal discount_value = price.getdiscountvalue;
            bool discount_type = price.DiscountType;
            decimal amount= 0;
            if (discount_type)
            {
                amount = unitprice - (discount_value / 100) * unitprice;
            }
            else
            {
                amount = unitprice - discount_value;
            }
            return Json(new { productprice = unitprice, product_discount_value = discount_value, discounttype = discount_type, amount = amount }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Edit(int id)
        {
            var model = new ProductModel();
            
            model.showproducttoedit = repo.ShowProductToEdit(id);
            DateTime date = DateTime.Now;
            var temp = repo.GetAllProducts(date);
            foreach (var item in model.showproducttoedit)
            {
                item.ProductList = temp;
            }

            return View(model);

        }



        [HttpPost]
        public ActionResult Edit(ProductModel model, int id)
        {
            var orderid = id;
            repo.EditData(model,orderid);
            return RedirectToAction("Index");
        }



        public ActionResult _TableRowEdit()
        {
            DateTime date = DateTime.Now;
            var model = new ProductModel();
            model.ProductList = repo.GetAllProducts(date);
            return PartialView("_TableRowEdit",model);
        }



        public ActionResult Invoice(int id)
        {
            var model = new OrderModel();
  
              model.Invoice =repo.Invoice(id);
            model.showproducttoedit = repo.ShowProductToEdit(id);
            return View(model);
        }
    
        public ActionResult ChangeProductPrice()
        {
            DateTime date = DateTime.Now;
            var model = new ProductModel();
           
            model.ProductList = repo.GetAllProducts(date);

            return View(model);
        }

        public ActionResult _UpdateProductPrice(int id)
        {
            DateTime date = DateTime.Now;
            var model = new ProductModel();
            model.ProductList = repo.GetAllProducts(date);
            model.ProductId = id;
            return PartialView("_UpdateProductPrice",model);
        }

        [HttpPost]
        public ActionResult UpdateProductPrice(ProductModel model)
        {
            DateTime curr_date = DateTime.Now;
            repo.UpdatePrice(model);
            return RedirectToAction("ChangeProductPrice");

        }
        [HttpGet]

        public ActionResult _ProductHistory(string[] id)
        {

            var model = new ProductModel();
            model.ProductList = repo.ProductHistory(id);
            return PartialView("_ProductHistory", model);
        }

        public ActionResult Discount_Master()
        {
            var model = new DiscountModel();
            model.discount_master = repo.DiscountHome();
            return View(model);
        }

        public ActionResult Add_Discount()
        {
            return View();
        }

        public ActionResult _discount()
        {
            var model = new DiscountModel();
         
            model.productList = repo.GetAllProducts(DateTime.Now);
            return PartialView("_discount",model);
        }

        [HttpPost]
        public ActionResult InsertDiscount(DiscountModel model)
        {
          
            repo.AddDiscount(model);
            return RedirectToAction("Discount_Master");
        }

        //public decimal GetDiscountPrice(int id)
        //{
        //    DateTime curr_date = DateTime.Now;
        //    decimal price = repo.GetDiscountPrice(id, curr_date);
        //    return price;
        //}

    }
}