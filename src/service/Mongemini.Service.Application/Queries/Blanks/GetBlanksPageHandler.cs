using AutoMapper;
using Mongemini.Application.Core.Extensions;
using Mongemini.Application.Core.Queries;
using Mongemini.Application.Core.ViewModels;
using Mongemini.Service.Application.ViewModels.Blanks;
using Mongemini.Service.Infrastructure.Contracts;
using Mongemini.Service.Infrastructure.Entities;
using Mongemini.Service.Infrastructure.Filters;


namespace Mongemini.Service.Application.Queries.Blanks
{
    public class GetBlanksPageHandler : PagedRequestHandler<GetBlanksRequest, BlankViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IBlankRepository _blankRepository;

        public GetBlanksPageHandler(IMapper mapper, IBlankRepository blankRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _blankRepository = blankRepository ?? throw new ArgumentNullException(nameof(blankRepository));
        }

        public override Task<PagedList<BlankViewModel>> Handle(GetBlanksRequest request, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<BlankMainFilter>(request);

            return _blankRepository.Where(filter.Build())
                                   .ToLookupAsync<BlankEntity, BlankViewModel>(filter, _mapper, cancellationToken);
        }
    }
}
