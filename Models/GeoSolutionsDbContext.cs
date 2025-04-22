using Microsoft.EntityFrameworkCore;
using ServiceManagement.Models;

namespace GeoSolucoesAPI.Models
{
    public class GeoSolutionsDbContext : DbContext
    {
        public GeoSolutionsDbContext()
        {
        }

        public GeoSolutionsDbContext(DbContextOptions<GeoSolutionsDbContext> options)
            : base(options)
        {
        }

        public DbSet<ServiceTypeDbo> ServiceTypes { get; set; }
        public DbSet<IntentionServiceDbo> IntentionServices { get; set; }
        public DbSet<BudgetDbo> Budgets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AddressDbo> Addresses { get; set; }
        public DbSet<HostingDbo> Hostings { get; set; }
        public DbSet<DistanceDbo> Distances { get; set; }
        public DbSet<CityDbo> Cities { get; set; }
        public DbSet<StartPointDbo> StartPoints { get; set; }
        public DbSet<ConfrontationDbo> Confrontations { get; set; }

        public IQueryable<BudgetDbo> GetBudgetFull()
            => Budgets
            .Include(x => x.Address)
            .Include(x => x.User)
            .Include(x => x.ServiceType)
            .Include(x => x.IntentionService);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BudgetDbo>()
                .HasOne(b => b.Address)
                .WithOne()
                .HasForeignKey<AddressDbo>(a => a.BudgetId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IntentionServiceDbo>()
             .HasOne<ServiceTypeDbo>()
             .WithMany(s => s.IntentionServices)
             .HasForeignKey(i => i.ServiceTypeId)
             .OnDelete(DeleteBehavior.Cascade);


        }
    }



}
