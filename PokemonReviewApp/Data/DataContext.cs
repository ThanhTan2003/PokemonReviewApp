using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories;
        public DbSet<Country> Countries;
        public DbSet<Owner> Owners;
        public DbSet<Pokemon> Pokemons;
        public DbSet<PokemonCategory> PokemonCategories;
        public DbSet<PokemonOwner> PokemonOwners;
        public DbSet<Review> Reviews;
        public DbSet<Reviewer> Reviewers;
        // Cấu hình mô hình dữ liệu của ứng dụng.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PokemonCategory>()
                    .HasKey(pc => new { pc.PokemonId, pc.CategoryId }); // định nghĩa khóa chính

            modelBuilder.Entity<PokemonCategory>()
                    .HasOne(p => p.Pokemon)
                    .WithMany(pc => pc.PokemonCategories) // Định nghĩa mối quan hệ một - nhiều (Một `Pokemon` có thể thuộc nhiều `PokemonCategory`)
                    .HasForeignKey(p => p.PokemonId); // xác định rằng cột `PokemonId` trong bảng `PokemonCategory` là khóa ngoại trỏ đến khóa chính của bảng `Pokemon`

            modelBuilder.Entity<PokemonCategory>()
                    .HasOne(p => p.Category)
                    .WithMany(pc => pc.PokemonCategories)
                    .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<PokemonOwner>()
                    .HasKey(po => new { po.PokemonId, po.OwnerId });

            modelBuilder.Entity<PokemonOwner>()
                    .HasOne(p => p.Pokemon)
                    .WithMany(pc => pc.PokemonOwners)
                    .HasForeignKey(p => p.PokemonId);

            modelBuilder.Entity<PokemonOwner>()
                    .HasOne(p => p.Owner)
                    .WithMany(pc => pc.PokemonOwners)
                    .HasForeignKey(c => c.OwnerId);
        }
    }
}
