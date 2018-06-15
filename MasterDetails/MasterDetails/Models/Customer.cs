using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterDetails.Models
{
    public class Customer
    {
        [Key]
        [Display(Name = "Codigo de Cliente")]
        public int Customerid { set; get; }

        [Display(Name = "Nombre Compañia")]
        [StringLength(30, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres)", MinimumLength = 3)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string CompanyName { set; get; }

        [Display(Name = "Nombre Contacto")]
        public string ContacName { set; get; }

        public virtual ICollection<Order> Orders { set; get;}

    }
}