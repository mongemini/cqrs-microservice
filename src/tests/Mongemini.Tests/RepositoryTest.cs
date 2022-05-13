using Mongemini.Service.Infrastructure.Entities;
using System.Linq;
using System.Threading;
using Xunit;

namespace Mongemini.Tests
{
    public class RepositoryTest
    {
        [Fact]
        public async void SaveAsync_Blank_Test()
        {

            var repository = TestHelper.GetRepository();

            await repository.AddAsync(new BlankEntity() { Id = "id1", SomeText = "some test" }, CancellationToken.None);

            await repository.SaveAsync(CancellationToken.None);

            var results = repository.GetAll();
            var blank = results.FirstOrDefault();

            Assert.NotNull(results);
            Assert.NotNull(blank);
            Assert.Equal("some test", blank.SomeText);
        }
    }
}
