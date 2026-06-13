using Microsoft.EntityFrameworkCore;

namespace loan_management_system.Model;

public class LoanContext : DbContext
{
    public DbSet<Customers> Customers { get; set; }
    public DbSet<Loans> Loans { get; set; }
    public DbSet<Payments> Payments { get; set; }
    public DbSet<LoanSchedule> LoanSchedule { get; set; }
    
    public LoanContext(DbContextOptions options) : base(options) { }
}