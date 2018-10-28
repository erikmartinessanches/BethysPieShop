using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethysPieShop.Models
{
    public class AppDbContext : DbContext
    {
        //We have to pass the options to bdContext for this to work.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //Specify which types need to be a table (Pies) in the db.
        public DbSet<Pie> Pies { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
    }
}
