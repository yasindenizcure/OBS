using Microsoft.EntityFrameworkCore;
using DenemeDers.Entity;

namespace DenemeDers.Context
{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions<ContextDb> options)
            : base(options)
        {
        }

        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Ders> Dersler { get; set; }
        public DbSet<OgretimGorevlisi> OgretimGorevlileri { get; set; }
        public DbSet<Not> Notlar { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Bolum> Bolumler { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.Entity<Ders>()
        .HasOne(d => d.Bolum)
        .WithMany(b => b.Dersler)
        .HasForeignKey(d => d.BolumId)
        .OnDelete(DeleteBehavior.Cascade);
        }

    }
}