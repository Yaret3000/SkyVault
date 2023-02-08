using Microsoft.EntityFrameworkCore;
using Model;

namespace Persistence
{
    /// <summary>
    /// Main database data context
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<FileMetadata> FilesMetadata { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            //Config FileMetadata
            modelBuilder.Entity<FileMetadata>()
                .ToContainer("FilesMetadata")
                .HasPartitionKey(e => e.Id);
        }
    }
}
