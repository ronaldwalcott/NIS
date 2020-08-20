using Microsoft.EntityFrameworkCore;
using NISApi.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NISApi.Data
{
    public class NisDbContext : DbContext
    {
        public NisDbContext(DbContextOptions<NisDbContext> options)
                : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
    }
}
