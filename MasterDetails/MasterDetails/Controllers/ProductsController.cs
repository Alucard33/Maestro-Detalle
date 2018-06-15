using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MasterDetails.Models;
// Agregamos para la funcionalidad de paginación
using PagedList;

namespace MasterDetails.Controllers
{
    public class ProductsController : Controller
    {
        private MasterTiendaContext db = new MasterTiendaContext();

        // GET: Products
        // Agregamos un parámetro al método
        /* Agregamos dos parámetros más para implementar la funcionalidad de paginación, uno de
           orden de clasificación actual "pagina" y un filtro actual */
        public ActionResult Index(string sortOrder, string FiltroActual, string CadenaBuscar, int? pagina)
        {
            /* Definimos un parámetro de ordenamiento en el campo "productname" y verificamos si el
            parámetro recibido no es nulo o vacío.
            Lo enviamos a la Vista */
            ViewBag.PRODUCNAMESortParam = String.IsNullOrEmpty(sortOrder) ? "productname_desc" : "";
            /* Definimos un parámetro de ordenamiento en el campo "productname" y le asignamos el parámetro
            recibido y verificamos si ya está ordenado en forma "ascendente"
            Lo enviamos a la Vista */
            ViewBag.PRODUCNAMEortParam = sortOrder == "productname_asc" ? "productname_desc" : "productname_asc";
            // Definimos una variable a la que le asignaremos el listado de todos los Products

            // Definimos una Vista enviándole el parámetro "sortOrder"
            ViewBag.OrdenamientoActual = sortOrder;

            // Definimos la paginación
            if (CadenaBuscar != null) // Si hay cadena a buscar
            {
                pagina = 1;
            }
            else
            {
                CadenaBuscar = FiltroActual;
            }
            // Definimos un parámetro de búsqueda. Lo enviamos a la Vista
            ViewBag.FiltroActual = CadenaBuscar;

            var products = from s in db.Products select s;
            switch (sortOrder)
            {
                // Ordenamiento descendente por campo "productname"
                case "productname_desc":
                    products = products.OrderByDescending(s => s.productname);
                    break;
                // Ordenamiento ascendente por campo "productname"
                default:
                    products = products.OrderBy(s => s.productname);
                    break;
            }
            //Definimos la busqueda, si el parametro de la busqueda no es nulo o vacio
            if (!String.IsNullOrEmpty(CadenaBuscar))
            {
                //Le asignamos al objeto "doctores" el resultado de la consulta que selecciona a los Products
                //cuyo productname contiene la cadena de la busqueda. Hemos utilizado una instruccion sql
                products = products.Where(s => s.productname.Contains(CadenaBuscar));
            }

            //Definimos el tamaño de la página y la cantidad de páginas
            int PageSize = 3;
            int PageNumber = (pagina ?? 1);
            // Enviamos el listado de doctores limitado por las variables PageSize y PageNumber
            return View(products.ToPagedList(PageNumber, PageSize));
        }
        /*public ActionResult Index()
        {
            return View(db.Products.ToList());
        }*/

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Productid,productname,unitprice")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Productid,productname,unitprice")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
