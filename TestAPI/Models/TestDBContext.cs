using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TestAPI.Models
{
    public partial class TestDBContext : DbContext
    {
        public TestDBContext(DbContextOptions<TestDBContext> options)
            : base(options)
        {
        }
        // DbSet for StoryInfo table
        public DbSet<StoryInfo> StoryInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoryInfo>()
                .ToTable("StoryInfo") // Map this class to the StoryInfo table
                .HasKey(s => s.Id);   // Define the primary key

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
