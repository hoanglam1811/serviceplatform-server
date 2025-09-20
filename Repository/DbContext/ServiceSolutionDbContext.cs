
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Entities;

namespace Repository.Data;
public class ServiceSolutionDbContext : DbContext
{
    public ServiceSolutionDbContext()
    {
    }

    public ServiceSolutionDbContext(DbContextOptions<ServiceSolutionDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BankAccount> BankAccounts { get; set; }
	public virtual DbSet<Booking> Bookings { get; set; }
	public virtual DbSet<Chat> Chats { get; set; }
	public virtual DbSet<LoyaltyPoint> LoyaltyPoints { get; set; }
	public virtual DbSet<Notification> Notifications { get; set; }
	public virtual DbSet<Payment> Payments { get; set; }
	public virtual DbSet<ProviderProfile> ProviderProfiles { get; set; }
	public virtual DbSet<Review> Reviews { get; set; }
	public virtual DbSet<Services> Services { get; set; }
	public virtual DbSet<ServiceCategory> ServiceCategories { get; set; }
	public virtual DbSet<Transaction> Transactions { get; set; }
	public virtual DbSet<User> Users { get; set; }
	public virtual DbSet<Wallet> Wallets { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "";
 
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
            .Build();

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySQL(configuration.GetConnectionString("Db") ?? string.Empty);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Setting>()
        //    .HasOne(u => u.Coach)
        //    .WithOne(p => p.Setting)
        //    .HasForeignKey<Setting>(p => p.CoachId);

        //modelBuilder.Entity<PageContent>()
        //    .HasOne(u => u.Page)
        //    .WithOne(p => p.PageContent)
        //    .HasForeignKey<PageContent>(p => p.PageId);

    }
}
