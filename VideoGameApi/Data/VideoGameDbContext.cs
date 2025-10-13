using Microsoft.EntityFrameworkCore;

namespace VideoGameApi.Data {
    public class VideoGameDbContext(DbContextOptions<VideoGameDbContext> options):DbContext(options)
        {
        public DbSet<VideoGame> VideoGames => Set<VideoGame>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<VideoGame>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Platform).HasMaxLength(100);
                entity.Property(e => e.Developer).HasMaxLength(100);
                entity.Property(e => e.Publisher).HasMaxLength(100);
            });
        }
    }

}
   
