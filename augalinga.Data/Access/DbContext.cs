﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using augalinga.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace augalinga.Data.Access
{
    public class DataContext : DbContext
    {
        public DbSet<Meeting> Meetings { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=augalingaDB;User Id=sa;Password=augalinga;TrustServerCertificate=True;");
        }
    }
}