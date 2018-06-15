using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MasterDetails.Models;

namespace MasterDetails.Controllers
{
    public class OrderdetailsController : Controller
    {
        private MasterTiendaContext db = new MasterTiendaContext();

        // GET: Orderdetails
        public ActionResult Index()
        {
            var orderdetails = db.Orderdetails.Include(o => o.Order).Include(o => o.Products);
            return View(orderdetails.ToList());
        }

        // GET: Orderdetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orderdetail orderdetail = db.Orderdetails.Find(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            return View(orderdetail);
        }

        // GET: Orderdetails/Create
        public ActionResult Create()
        {
            ViewBag.Orderid = new SelectList(db.Orders, "Orderid", "Orderid");
            ViewBag.Productid = new SelectList(db.Products, "Productid", "productname");
            return View();
        }

        // POST: Orderdetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "orderdetailid,Orderid,Productid,Quantity,unitprice")] Orderdetail orderdetail)
        {
            if (ModelState.IsValid)
            {
                db.Orderdetails.Add(orderdetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Orderid = new SelectList(db.Orders, "Orderid", "Orderid", orderdetail.Orderid);
            ViewBag.Productid = new SelectList(db.Products, "Productid", "productname", orderdetail.Productid);
            return View(orderdetail);
        }

        // GET: Orderdetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orderdetail orderdetail = db.Orderdetails.Find(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.Orderid = new SelectList(db.Orders, "Orderid", "Orderid", orderdetail.Orderid);
            ViewBag.Productid = new SelectList(db.Products, "Productid", "productname", orderdetail.Productid);
            return View(orderdetail);
        }

        // POST: Orderdetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "orderdetailid,Orderid,Productid,Quantity,unitprice")] Orderdetail orderdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Orderid = new SelectList(db.Orders, "Orderid", "Orderid", orderdetail.Orderid);
            ViewBag.Productid = new SelectList(db.Products, "Productid", "productname", orderdetail.Productid);
            return View(orderdetail);
        }

        // GET: Orderdetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orderdetail orderdetail = db.Orderdetails.Find(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            return View(orderdetail);
        }

        // POST: Orderdetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orderdetail orderdetail = db.Orderdetails.Find(id);
            db.Orderdetails.Remove(orderdetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
