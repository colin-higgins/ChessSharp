using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

    public class DataContext : DbContext
    {
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<System.Data.Entity.Infrastructure.IncludeMetadataConvention>();
        //}

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }