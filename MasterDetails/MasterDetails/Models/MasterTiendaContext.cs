using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MasterDetails.Models
{
    public class MasterTiendaContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MasterTiendaContext() : base("name=MasterTiendaContext")
        {
        }

        public System.Data.Entity.DbSet<MasterDetails.Models.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<MasterDetails.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<MasterDetails.Models.Orderdetail> Orderdetails { get; set; }

        public System.Data.Entity.DbSet<MasterDetails.Models.Product> Products { get; set; }
    }
}
