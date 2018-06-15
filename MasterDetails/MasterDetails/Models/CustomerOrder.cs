using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterDetails.Models
{
    public class CustomerOrder:Customer
    {
        [Display(Name ="Fecha Orden")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { set; get; }
    }
}