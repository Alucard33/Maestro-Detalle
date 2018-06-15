using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterDetails.Models
{
    public class Order
    {
        [Key]
        [Display(Name = "Numero Orden")]
        public int Orderid { set; get; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Orden")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { set; get; }

        public int Customerid { set; get; }

        public virtual Customer Customer { set; get;}

        public virtual ICollection<Orderdetail> Orderdetails { set; get; }
    }
}