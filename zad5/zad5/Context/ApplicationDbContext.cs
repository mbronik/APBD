// using Microsoft.EntityFrameworkCore;
// using zad5.Models;
//
// namespace zad5.context;
//
// public partial class ApplicationDbContext : DbContext
//
// {
//     public ApplicationDbContext()
//     {
//     }
//
//     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//         : base(options)
//     {
//     }
//
//     public virtual DbSet<Client> Clients { get; set; }
//     public virtual DbSet<ClientTrip> ClientTrips { get; set; }
//     public virtual DbSet<Country> Countries { get; set; }
//     public virtual DbSet<CountryTrip> CountryTrips { get; set; }
//     public virtual DbSet<Trip> Trips { get; set; }
//
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//         // if (optionsBuilder.IsConfigured)
//         // {
//             IConfigurationRoot configuration = new ConfigurationBuilder()
//                 .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
//                 .AddJsonFile("appsettings.json")
//                 .Build();
//             optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 23)));
//         // }
//     }
//
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.Entity<Client>(entity =>
//         {
//             entity.HasKey(e => e.IdClient);
//             entity.Property(e => e.FirstName).IsRequired().HasMaxLength(120);
//             entity.Property(e => e.LastName).IsRequired().HasMaxLength(120);
//             entity.Property(e => e.Email).IsRequired().HasMaxLength(120);
//             entity.Property(e => e.Telephone).IsRequired().HasMaxLength(120);
//             entity.Property(e => e.Pesel).IsRequired().HasMaxLength(120);
//         });
//
//         modelBuilder.Entity<ClientTrip>(entity =>
//         {
//             entity.HasKey(e => new { e.IdClient, e.IdTrip });
//             entity.Property(e => e.RegisteredAt).IsRequired().HasColumnType("datetime");
//             entity.Property(e => e.PaymentDate).HasColumnType("datetime");
//
//             // entity.HasOne(d => d.Client)
//             //     .WithMany(p => p.ClientTrips)
//             //     .HasForeignKey(d => d.IdClient)
//             //     .OnDelete(DeleteBehavior.ClientSetNull)
//             //     .HasConstraintName("FK_ClientTrip_Client");
//             //
//             // entity.HasOne(d => d.Trip)
//             //     .WithMany(p => p.ClientTrips)
//             //     .HasForeignKey(d => d.IdTrip)
//             //     .OnDelete(DeleteBehavior.ClientSetNull)
//             //     .HasConstraintName("FK_ClientTrip_Trip");
//         });
//
//         modelBuilder.Entity<Country>(entity =>
//         {
//             entity.HasKey(e => e.IdCountry);
//             entity.Property(e => e.Name).IsRequired().HasMaxLength(120);
//         });
//
//         modelBuilder.Entity<CountryTrip>(entity =>
//         {
//             entity.HasKey(e => new { e.IdCountry, e.IdTrip });
//
//             // entity.HasOne(d => d.Country)
//             //     .WithMany(p => p.CountryTrips)
//             //     .HasForeignKey(d => d.IdCountry)
//             //     .OnDelete(DeleteBehavior.ClientSetNull)
//             //     .HasConstraintName("FK_CountryTrip_Country");
//             //
//             // entity.HasOne(d => d.Trip)
//             //     .WithMany(p => p.CountryTrips)
//             //     .HasForeignKey(d => d.IdTrip)
//             //     .OnDelete(DeleteBehavior.ClientSetNull)
//             //     .HasConstraintName("FK_CountryTrip_Trip");
//         });
//
//         modelBuilder.Entity<Trip>(entity =>
//         {
//             entity.HasKey(e => e.IdTrip);
//             entity.Property(e => e.Name).IsRequired().HasMaxLength(120);
//             entity.Property(e => e.Description).IsRequired().HasMaxLength(220);
//             entity.Property(e => e.DateFrom).IsRequired().HasColumnType("datetime");
//             entity.Property(e => e.DateTo).IsRequired().HasColumnType("datetime");
//             entity.Property(e => e.MaxPeople).IsRequired();
//         });
//
//         OnModelCreatingPartial(modelBuilder);
//     }
//
//     partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
// }