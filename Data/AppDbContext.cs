using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using week6.Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<week6.Models.Product> Product { get; set; } = default!;

        public DbSet<week6.Models.Genre> Genre { get; set; } = default!;

        public DbSet<week6.Models.Movie> Movie { get; set; } = default!;
    }
