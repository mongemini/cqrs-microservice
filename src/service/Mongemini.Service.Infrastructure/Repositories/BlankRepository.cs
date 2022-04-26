using Mongemini.Persistence.Contracts.Data;
using Mongemini.Persistence.Implementations.Data;
using Mongemini.Service.Infrastructure.Contracts;
using Mongemini.Service.Infrastructure.Entities;

namespace Mongemini.Service.Infrastructure.Repositories
{
    public class BlankRepository : BaseRepository<BlankEntity, string>, IBlankRepository
    {
        public BlankRepository(IBlankContext context) : base(context)
        {
        }
    }
}
