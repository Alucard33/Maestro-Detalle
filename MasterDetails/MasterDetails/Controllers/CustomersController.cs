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
    public class CustomersController : Controller
    {
        private MasterTiendaContext db = new MasterTiendaContext();

        // GET: Customers
        // Agregamos un parámetro al método
        /* Agregamos dos parámetros más para implementar la funcionalidad de paginación, uno de
           orden de clasificación actual "pagina" y un filtro actual */
        public ActionResult Index(string sortOrder, string FiltroActual, string CadenaBuscar, int? pagina)
        {
            /* Definimos un parámetro de ordenamiento en el campo "Apellidos" y verificamos si el
            parámetro recibido no es nulo o vacío.
            Lo enviamos a la Vista */
            ViewBag.CONTACTNAMESortParam = String.IsNullOrEmpty(sortOrder) ? "ContacName_desc" : "";
            /* Definimos un parámetro de ordenamiento en el campo "DUI" y le asignamos el parámetro
            recibido y verificamos si ya está ordenado en forma "ascendente"
            Lo enviamos a la Vista */
            ViewBag.COMPANYNAMESortParam = sortOrder == "CompanyName_asc" ? "CompanyName_desc" : "CompanyName_asc";
            // Definimos una variable a la que le asignaremos el listado de todos los doctores

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

            var customers = from s in db.Customers select s;
            switch (sortOrder)
            {
                // Ordenamiento descendente por campo "Apellidos"
                case "ContacName_desc":
                    customers = customers.OrderByDescending(s => s.ContacName);
                    break;
                // Ordenamiento descendente por campo "DUI"
                case "CompanyName_desc":
                    customers = customers.OrderByDescending(s => s.CompanyName);
                    break;
                // Ordenamiento ascendente por campo "DUI"
                case "CompanyName_asc":
                    customers = customers.OrderBy(s => s.CompanyName);
                    break;
                // Ordenamiento ascendente por campo "Apellidos"
                default:
                    customers = customers.OrderBy(s => s.ContacName);
                    break;
            }
            //Definimos la busqueda, si el parametro de la busqueda no es nulo o vacio
            if (!String.IsNullOrEmpty(CadenaBuscar))
            {
                //Le asignamos al objeto "doctores" el resultado de la consulta que selecciona a los doctores
                //cuyo nombre y apellido contiene la cadena de la busqueda. Hemos utilizado una instruccion LINQ
                customers = customers.Where(s => s.CompanyName.Contains(CadenaBuscar) || s.ContacName.Contains(CadenaBuscar));
            }

            //Definimos el tamaño de la página y la cantidad de páginas
            int PageSize = 3;
            int PageNumber = (pagina ?? 1);
            // Enviamos el listado de doctores limitado por las variables PageSize y PageNumber
            return View(customers.ToPagedList(PageNumber, PageSize));
        }
        /*public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }*/

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Customerid,CompanyName,ContacName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Customerid,CompanyName,ContacName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
