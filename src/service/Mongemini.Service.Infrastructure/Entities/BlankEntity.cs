using Mongemini.Persistence.Contracts.Data;

namespace Mongemini.Service.Infrastructure.Entities
{
    public class BlankEntity : IEntity<string>
    {
        public string Id { get; set; }

        public string SomeText { get; set; }
    }
}
