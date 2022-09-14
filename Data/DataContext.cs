﻿using Course2.Models;
using Microsoft.EntityFrameworkCore;

namespace Course2.Data
{
      public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Users { get; set; }
    }
}