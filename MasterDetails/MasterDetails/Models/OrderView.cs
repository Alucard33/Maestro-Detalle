using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterDetails.Models
{
    public class OrderView
    {
        public CustomerOrder Customer { set; get; }

        public ProductOrder Titles { set; get; }

        public List<ProductOrder> Products { set; get; }

    }
}