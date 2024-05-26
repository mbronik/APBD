using Microsoft.EntityFrameworkCore;
using zad6.model;

namespace zad6.context;

public class Z6DbContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     IConfigurationRoot configuration = new ConfigurationBuilder()
    //         .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    //         .AddJsonFile("appsettings.json")
    //         .Build();
    //     optionsBuilder.UseSqlServer(configuration.GetConnectionString("BookDBConnection"));
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(e =>
        {
            e.HasKey(et => et.PatientId);
            e.Property(p => p.FirstName).HasMaxLength(100).IsRequired();
            
            // e.HasOne(p => p.Animals)
        });
    }
}