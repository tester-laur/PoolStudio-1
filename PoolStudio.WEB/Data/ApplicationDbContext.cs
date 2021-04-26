using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using PoolStudio.WEB.Models;

namespace PoolStudio.WEB.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PoolStudio.WEB.Models.Item> Item { get; set; }

        public DbSet<PoolStudio.WEB.Models.Clasification> Clasification { get; set; }

        public DbSet<PoolStudio.WEB.Models.ItemTest> ItemTests { get; set; }
    }
}
