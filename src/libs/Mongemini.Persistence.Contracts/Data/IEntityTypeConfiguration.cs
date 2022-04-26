using Microsoft.EntityFrameworkCore;

namespace Mongemini.Persistence.Contracts.Data
{
    public interface IEntityTypeConfiguration
    {
        void Configure(ModelBuilder modelBuilder);
    }
}
