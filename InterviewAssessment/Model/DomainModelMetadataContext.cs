using System.Configuration;
using DomainModelEditor.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DomainModelEditor.Model
{
    public class DomainModelMetadataContext : DbContext, IDomainModelMetadataContext
    {
        private const string ConnectionStringIdentifier = "DomainModelDatabase";
        private ILogger Logger { get; }

        public DomainModelMetadataContext() {
            Logger = Log.Logger.ForContext<DomainModelMetadataContext>();
            Logger.Debug("Creating DomainModelMetadataContext.");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[ConnectionStringIdentifier];
            optionsBuilder.UseSqlite(connectionStringSettings.ConnectionString);
        }
        public DbSet<Entity> Entities { get; set; }

        public DbSet<Attribute> Attributes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Logger.Verbose("OnModelCreating");
            DomainModelMetadataConfiguration.Configure(modelBuilder);
        }
    }
}
