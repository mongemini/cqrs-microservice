using Mongemini.Persistence.Contracts.Data;
using Mongemini.Service.Infrastructure.Entities;

namespace Mongemini.Service.Infrastructure.Contracts
{
    public interface IBlankRepository : IRepository<BlankEntity, string>
    {
    }
}
