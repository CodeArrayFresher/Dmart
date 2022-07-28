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

        public ActionResult _IndexProductList(int id)
        {
            
            var model = new OrderModel();
            model.ProductList = repo.GetProducts(id);
            return PartialView("_IndexProductList", model);
        }


        public  ActionResult Insert()
        {
            var model = new ProductModel();
            model.CustomerList= repo.GetCustomers();
            model.ProductList = repo.GetAllProducts();
     
            return View(model);
        }


        public ActionResult _TableRow()
        {
            var model = new ProductModel();
            model.ProductList = repo.GetAllProducts();
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
        public int GetUnitPrice(int id)
        {
            int price = repo.GetProductPrice(id);
            //var model = new ProductModel();
            //model.productPrice = repo.GetProductPrice(id);
            return price;
        }

      

        public ActionResult Edit(int id)
        {
            var model = new ProductModel();
            
            model.showproducttoedit = repo.ShowProductToEdit(id);

            var temp = repo.GetAllProducts();
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
            var model = new ProductModel();
        
           
                model.ProductList = repo.GetAllProducts();
          
            return PartialView("_TableRowEdit",model);
        }



        public ActionResult Invoice(int id)
        {
            var model = new OrderModel();
  
              model.Invoice =repo.Invoice(id);
            model.showproducttoedit = repo.ShowProductToEdit(id);
            return View(model);
        }
        //public ActionResult _TableRowEdit()
        //{
        //    var model = new OrderModel();
        //    model.CustomerList = repo.GetCustomers();
        //    model.ProductList = repo.GetAllProducts();
        //    return PartialView("_TableRowEdit", model);
        //}
        //[HttpGet]
        //public string test(int id)
        //{
        //    return "done"+id;
        //}
    }
}