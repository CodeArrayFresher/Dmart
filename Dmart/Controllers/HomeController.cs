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
            //var result = new OrderDetail();
            //result.or = repo.GetOrderDetails();
            //return View(model);
            //var model = repo.GetOrders();
            var model = new OrderModel();

            model.ProductList = repo.GetProducts();

            model.OrderList = repo.GetOrders();
            return View(model);
            
        }
        public ActionResult Jqeury()
        {
            return View();  
        }

        public  ActionResult Insert()
        {
            var model = new OrderModel();
            model.CustomerList= repo.GetCustomers();
            model.ProductList = repo.GetAllProducts();
            return View(model);
        }

        [HttpPost]
        public ActionResult InsertData(OrderModel model)
        {
            var customer = model.CustomerID;
            repo.AddData(model,customer);
            return RedirectToAction("Index");
        }

        public int getUnitPrice(int id)
        {
            var model = new OrderModel();
            model.productPrice = repo.GetProductPrice(id);
            return model.productPrice;
        }

        public ActionResult _TableRow()
        {
            var model = new OrderModel();
            model.CustomerList = repo.GetCustomers();
            model.ProductList = repo.GetAllProducts();
            return PartialView("_TableRow",model);
        }

        public ActionResult Edit(OrderModel models,int id)
        {
            var model = new OrderModel();
            var orderDetail = new OrderDetail();
            model.showproducttoedit = repo.ShowProductToEdit(orderDetail,id);
       
            model.ProductList = repo.GetAllProducts();
            //var model = new OrderModel();
            //model.CustomerList = repo.GetCustomers();
            //model.ProductList = repo.GetAllProducts();
            return View(model);

        }
    }
}