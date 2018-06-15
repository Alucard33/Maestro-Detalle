using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterDetails.Models
{
    public class Product
    {
        [Key]
        [Display(Name ="Codigo Producto")]
        public int Productid { set; get; }

        [Display(Name ="Nombre Producto")]
        public string productname { set; get; }

        [DisplayFormat(DataFormatString ="{0:C2}", ApplyFormatInEditMode = false)]
        [Display(Name ="Precio Unidad")]
        public decimal unitprice { set; get; }

        public virtual ICollection<Orderdetail> Orderdetails { set; get; }
    }
}