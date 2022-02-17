using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Database
{
    public class AFIContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public string DbPath { get; }

        public AFIContext() : base()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "aficustomerportal.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }
    }
}
