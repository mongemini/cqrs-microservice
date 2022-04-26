using AutoMapper;
using Mongemini.Domain.Entities;
using Mongemini.Domain.Exceptions;
using Mongemini.Persistence.Contracts.Exceptions;
using Mongemini.Persistence.Implementations.Extensions;
using Mongemini.Service.Infrastructure.Contracts;
using Mongemini.Service.Infrastructure.Entities;

namespace Mongemini.Service.Domain.Aggregates
{
    public class Blank : AggregateRoot<string>
    {
        private readonly IMapper _mapper;

        private readonly IBlankRepository _blankRepository;

        public Blank(IMapper mapper, IBlankRepository blankRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _blankRepository = blankRepository ?? throw new ArgumentNullException(nameof(blankRepository));
        }

        private Blank() { }

        public string SomeText { get; set; }

        public async Task<Blank> GetBlankAsync(string id, CancellationToken cancellationToken)
        {
            return await _blankRepository.Get(id).SingleOrDefaultAsync<BlankEntity, Blank>(_mapper, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Blank> CreateBlankAsync(Blank blank, CancellationToken cancellationToken)
        {
            var settingDb = await this.GetBlankAsync(blank.Id, cancellationToken).ConfigureAwait(false);
            if (settingDb != null)
            {
                throw new DomainException($"Blank for this id ({blank.Id}) already exists.");
            }

            var dbEntity = _mapper.Map<BlankEntity>(blank);
            await _blankRepository.AddAsync(dbEntity, cancellationToken).ConfigureAwait(false);
            await _blankRepository.SaveAsync(cancellationToken).ConfigureAwait(false);

            return blank;
        }

        public async Task<Blank> UpdateBlankAsync(Blank blank, CancellationToken cancellationToken)
        {
            var dbEntity = await _blankRepository.FindAsync(blank.Id, cancellationToken).ConfigureAwait(false);

            if (dbEntity == null)
            {
                throw new NotFoundException($"Blank not found, is {blank.Id}");
            }

            _mapper.Map(blank, dbEntity);
            _blankRepository.Update(dbEntity);

            await _blankRepository.SaveAsync(cancellationToken).ConfigureAwait(false);

            return blank;
        }

        public async Task<bool> DeleteBlankAsync(string id, CancellationToken cancellationToken)
        {
            _blankRepository.Delete(id);
            var affectedRows = await _blankRepository.SaveAsync(cancellationToken).ConfigureAwait(false);
            return affectedRows != 0;
        }
    }
}
