﻿using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Entities;
using System.Data.Common;

namespace PortfolioAPI.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Experience> Experiences { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }
}
