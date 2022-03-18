using Microsoft.EntityFrameworkCore;

namespace Models 
{
    public class HuntContext : DbContext
    {
        public DbSet<Guns> Guns { get; set; }

        public DbSet<Variant> Variant { get; set; } 

        public DbSet<Loadout> Loadout { get; set; }

        public DbSet<GunsVariant> GunsVariant { get; set; }

        public DbSet<Tools> Tools { get; set; }
        
        public DbSet<SecondGun> SecondGun { get; set; }

        public HuntContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GunsVariant>()
                        .HasOne(g => g.Gun)
                        .WithMany(gu => gu.GunsVariant)
                        .HasForeignKey(gi => gi.GunID);

            modelBuilder.Entity<GunsVariant>()
                        .HasOne(g => g.Variant)
                        .WithMany(gu => gu.VariantGuns)
                        .HasForeignKey(gi => gi.VariantID);
        }


    }
}