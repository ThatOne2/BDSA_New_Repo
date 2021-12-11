using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrialProject.Shared;
using TrialProject.Shared.DTO;

namespace TrialProject.Server.Controllers
{
    public class DataContext : DbContext
    {
         public DbSet<Project>? Projects { get; set; }
         public DbSet<Student>? Students { get; set; }
         public DbSet<Supervisor>? Supervisors { get; set; }
         public DbSet<Tag>? Tags { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //Ikke nødvendigvis de bedste regler men så kan det ses hvordan man gør
        {
            modelBuilder
                .Entity<Project>()
                .Property(e => e.ProjectStatus)
                .HasConversion( v => v.ToString(),
                                v => (Status)Enum.Parse(typeof(Status), v));


            
            modelBuilder.Entity<Project>() 
                .HasIndex(u => u.shortDescription)
                .IsUnique();

            modelBuilder.Entity<Supervisor>()
            .HasMany(c => c.Projects);

             modelBuilder.Entity<Project>()
            .HasMany(c => c.Tags);
        }
  
        /* public async Task<IReadOnlyCollection<ProjectPreviewDTO>> ReadAsync() =>
        (await _context.Characters
                        .Select(c => new CharacterDto(c.Id, c.AlterEgo, c.GivenName, c.Surname))
                        .ToListAsync())
                        .AsReadOnly(); */
    }
}