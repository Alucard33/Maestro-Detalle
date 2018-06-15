using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterDetails.Models
{
    public class ProductOrder:Product
    {
        [Display(Name="Cantidad")]
        public int quantity { set; get; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Display(Name ="Total")]
        public decimal parcial { get { return unitprice * quantity; } }
    }
}