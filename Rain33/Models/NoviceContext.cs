using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rain33.Models
{
    public class NoviceContext : DbContext
    {
        public NoviceContext()
        {
        }

        public NoviceContext(DbContextOptions<NoviceContext> options)
            : base(options)
        { }

        public DbSet<Novice> Novica { get; set; }
        public DbSet<Uporabniki> Uporabnik { get; set; }
    }
}
