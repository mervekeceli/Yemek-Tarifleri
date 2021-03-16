using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proje.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje.Data
{
    public class ApplicationDbContext : IdentityDbContext<Musteri>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Yemek> Yemek { get; set; }

        public DbSet<Kategori> Kategori { get; set; }

        public DbSet<Malzeme> Malzeme { get; set; }

        public DbSet<YemekMalzeme> YemekMalzeme { get; set; }

        
    }
}
