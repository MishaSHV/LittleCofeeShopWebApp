using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Configuration;
using LittleCofeeShopWebApp.Domain.Entities;

namespace LittleCofeeShopWebApp.Domain.Concrete
{
    public class LittleCofeeShopDbContext : DbContext
    {
        public LittleCofeeShopDbContext() : base("LittleCofeeShop")
        {
            //Database.SetInitializer(new CofeeShopDbInitializer());
        }
        public DbSet<Cofee> CofeeRecords { get; set; }
        public DbSet<VolumeOption> VolumeOptionRecords { get; set; }
        public DbSet<MilkOption> MilkOptionRecords { get; set; }
        public DbSet<SugarOption> SugarOptionRecords { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Unit> Units { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DefaultConnection, Configuration>());

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Cofee>()
                .HasMany<VolumeOption>(s => s.VolumeOptions)
                .WithMany(c => c.CofeeTypes)
                .Map(cs =>
                {
                    cs.MapLeftKey("CofeeRefId");
                    cs.MapRightKey("VolumeRefId");
                    cs.ToTable("CofeeTypesVolumeOpt");
                });

            modelBuilder.Entity<Cofee>()
                .HasMany<MilkOption>(s => s.MilkOptions)
                .WithMany(c => c.CofeeTypes)
                .Map(cs =>
                {
                    cs.MapLeftKey("CofeeRefId");
                    cs.MapRightKey("MilkRefId");
                    cs.ToTable("CofeeTypesMilkOpt");
                });

            modelBuilder.Entity<Cofee>()
                .HasMany<SugarOption>(s => s.SugarOptions)
                .WithMany(c => c.CofeeTypes)
                .Map(cs =>
                {
                    cs.MapLeftKey("CofeeRefId");
                    cs.MapRightKey("SugarRefId");
                    cs.ToTable("CofeeTypesSugarOpt");
                });

            modelBuilder.Entity<VolumeOption>().Property(x => x.Size).HasPrecision(16, 3);

            modelBuilder.Entity<Order>().Property(o => o.UserId).IsOptional();
            modelBuilder.Entity<User>().HasMany(u => u.UserOrders).WithOptional(u => u.User).WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}