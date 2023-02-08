using Microsoft.EntityFrameworkCore;
using StateManagements.Models.Models;
using StateManagments.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateManagments.Models.Data
{
    public class StateManagmentContext: DbContext
    {
        public StateManagmentContext(DbContextOptions<StateManagmentContext> options) : base(options)
        {
        }
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Food> Foods { get; set; } = null!;

    }
}
