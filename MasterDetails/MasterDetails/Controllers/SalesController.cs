using MasterDetails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MasterDetails.Controllers
{
    public class SalesController : Controller
    {
        private MasterTiendaContext db = new MasterTiendaContext();
        // GET: Sales
        public ActionResult NewOrder()
        {
            OrderView orderview = new OrderView();
            orderview.Customer = new CustomerOrder();
            orderview.Products = new List<ProductOrder>();
            Session["OrderView"] = orderview;
            var list = db.Customers.ToList();
            ViewBag.Customerid = new SelectList(list,"Customerid","CompanyName");
            return View(orderview);
        }
        [HttpPost]
        public ActionResult NewOrder(OrderView orderview)
        {
            orderview = Session["OrderView"] as OrderView;
            int idcustomers = Convert.ToInt32(Request["Customerid"]);
            DateTime dateorder = Convert.ToDateTime(Request["Customer.Orderdate"]);
            Order neworder = new Order()
            {
                Customerid = idcustomers,
                OrderDate = dateorder
            };
            db.Orders.Add(neworder);
            db.SaveChanges();
            int lastorderid = db.Orders.ToList().Select(o => o.Orderid).Max();
            foreach(ProductOrder Item in orderview.Products)
            {
                var detail = new Orderdetail()
                {
                    Orderid = lastorderid,
                    Productid = Item.Productid,
                    Quantity = Item.quantity,
                    unitprice = Item.unitprice
                };
                db.Orderdetails.Add(detail);
            }
            db.SaveChanges();
            orderview = Session["OrderView"] as OrderView;
            var list = db.Customers.ToList();
            ViewBag.Customerid = new SelectList(list, "Customerid", "CompanyName");
            return View(orderview);
        }

        [HttpGet]
        public ActionResult AddProdut()
        {
            var listp = db.Products.ToList();
            ViewBag.Productid = new SelectList(listp, "Productid", "productname");
            return View();
        }

        [HttpPost]
        public ActionResult AddProdut(ProductOrder productorder)
        {
            var orderview = Session["OrderView"] as OrderView;
            var productid = Convert.ToInt32(Request["Productid"]);
            var product = db.Products.Find(productid);
            productorder = new ProductOrder()
            {
                Productid = product.Productid,
                productname = product.productname,
                unitprice = product.unitprice,
                quantity = Convert.ToInt32(Request["quantity"])
            };
            orderview.Products.Add(productorder);
            var list = db.Customers.ToList();
            ViewBag.Customerid = new SelectList(list, "Customerid", "CompanyName");
            var listp = db.Products.ToList();
            ViewBag.Productid = new SelectList(listp, "Productid", "productname");
            return View("NewOrder",orderview);
        }       
    }
}