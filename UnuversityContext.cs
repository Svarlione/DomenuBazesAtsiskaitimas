using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomenuBazesAtsiskaitimas;

namespace DomenuBazesAtsiskaitimas
{
    public class UnuversityContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Lecture> Lectures { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=Fakultietas;Trusted_Connection=True;");
        }
    }
}
