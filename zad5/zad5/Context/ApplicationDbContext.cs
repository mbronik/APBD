using Microsoft.EntityFrameworkCore;
using zad5.Models;

namespace zad5.context;

public class ApplicationDbContext : DbContext

{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<Client> Clients { get; set; }
}