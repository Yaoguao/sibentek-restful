﻿using Microsoft.EntityFrameworkCore;
using Sibentek.Core.Model;

namespace Sibentek.DataAccess;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=../sibentek-restful/database.db");
    }

}