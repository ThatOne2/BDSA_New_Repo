using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrialProject.Shared;


namespace Server
{
    public class DataContext : DbContext
    {
         public DbSet<Project> Projects { get; set; }
         public DbSet<Student> Students { get; set; }
         public DbSet<Supervisor> Supervisors { get; set; }
         

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //Ikke nødvendigvis de bedste regler men så kan det ses hvordan man gør
        {
            modelBuilder
                .Entity<Project>()
                .Property(e => e.ProjectStatus)
                .HasConversion(new EnumToStringConverter<Status>());
            
            modelBuilder.Entity<Project>() 
                .HasIndex(u => u.shortDescription)
                .IsUnique();
        }
  
    }
}