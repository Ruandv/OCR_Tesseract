using Microsoft.EntityFrameworkCore;

namespace OCR.Database.Layer
{
    public class OCRContext : DbContext
    {
        public OCRContext(DbContextOptions<OCRContext> options) : base(options) { }
        public DbSet<OcrTemplate> Templates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }

    public static class DbInitializer
    {
        public static void Initialize(OCRContext context)
        {
            context.Database.EnsureCreated();

        }
    }
}
