using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mongemini.Persistence.Implementations.Data;
using Mongemini.Service.Infrastructure.Entities;

namespace Mongemini.Service.Infrastructure.Configurations
{
    public class BlankEntityConfiguration : EntityTypeConfiguration<BlankEntity>
    {
        public override void Configure(EntityTypeBuilder<BlankEntity> builder)
        {
            builder.Property(p => p.Id).HasColumnName("Id");
        }
    }
}
