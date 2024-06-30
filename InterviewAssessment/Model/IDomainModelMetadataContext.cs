using DomainModelEditor.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace DomainModelEditor.Model
{
    public interface IDomainModelMetadataContext
    {
        DbSet<Entity> Entities { get; }

        DbSet<Attribute> Attributes { get; }

        int SaveChanges();
    }
}
