using Microsoft.EntityFrameworkCore;
using QB.Domain.Models;
using System;
using System.Collections.Generic;

namespace QB.Persistence.Sqlite
{
    public class SqliteDbContext : DbContext
    {
        public SqliteDbContext(DbContextOptions<SqliteDbContext> options)
            : base(options)
        {
        }

        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<City> City { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqliteDbContext).Assembly);
        }

        internal IEnumerable<object> AsAsyncEnumerable()
        {
            throw new NotImplementedException();
        }
    }
}
