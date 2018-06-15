using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterDetails.Models
{
    public class Orderdetail
    {
        [Key]
        public int orderdetailid { set; get; }

        public int Orderid { set; get; }

        public int Productid { set; get; }
		[Display(Name="Cantidad")]
        public int Quantity { set; get; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Display(Name="Precio Unitario")]
        public decimal unitprice { set; get; }

        public virtual Order Order { set; get; }

        public virtual Product Products { set; get; }
    }
}