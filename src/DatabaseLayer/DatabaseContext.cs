using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DatabaseLayer
{
    public class OcrDatabaseContext : DbContext
    {
        public DbSet<OcrTemplate> OcrTemplates { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<OcrDocumentPage> Page { get; set; }
        public OcrDatabaseContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            InitializeDatabase();
        }


        public OcrDatabaseContext() : base("OCRSystem")
        {
            InitializeDatabase();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        private void InitializeDatabase()
        {
            Database.SetInitializer(new DBInitializer());
            if (!Database.Exists())
            {
                Database.Initialize(true);
            }
        }

    }
}
