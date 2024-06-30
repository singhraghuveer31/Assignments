using DomainModelEditor.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace DomainModelEditor.Model
{
    public static class DomainModelMetadataConfiguration
    {
        public static void Configure(ModelBuilder dbModelBuilder)
        {
            ConfigureEntityEntity(dbModelBuilder);
        }

        private static void ConfigureEntityEntity(ModelBuilder dbModelBuilder)
        {
            dbModelBuilder.Entity<Entity>(e =>
            {
                e.ToTable("entities").HasKey(k => k.Id).HasName("id");
                e.Property(p => p.Id).HasColumnName("id").IsRequired();
                e.Property(p => p.Name).HasColumnName("name").IsRequired();
                e.HasOne(c => c.Coords).WithOne(p => p.Entity)
                    .HasForeignKey<Coords>(k => k.Id).IsRequired();
                e.HasMany(p => p.Attributes).WithOne(a => a.Entity)
                    .HasForeignKey(f=>f.EntityId);
            });
            
            dbModelBuilder.Entity<Coords>(d =>
            {
                d.ToTable("coords").HasKey(c => c.Id).HasName("id");
                d.Property(c => c.Id).HasColumnName("id").IsRequired();
                d.Property(c => c.X).HasColumnName("x").IsRequired();
                d.Property(c => c.Y).HasColumnName("y").IsRequired();
            });

            dbModelBuilder.Entity<Attribute>(e =>
            {
                e.ToTable("attributes").HasKey(a => a.Id).HasName("id");
                e.Property(a => a.Id).HasColumnName("id").IsRequired();
                e.Property(a => a.DataType).HasColumnName("data_type").IsRequired();
                e.Property(a => a.EntityId).HasColumnName("entity_id").IsRequired();
            });
        }
    }
}