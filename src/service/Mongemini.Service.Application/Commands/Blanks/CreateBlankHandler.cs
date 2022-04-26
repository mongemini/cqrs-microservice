using AutoMapper;
using MediatR;
using Mongemini.Service.Application.ViewModels.Blanks;
using Mongemini.Service.Domain.Aggregates;

namespace Mongemini.Service.Application.Commands.Blanks
{
    public class CreateBlankHandler : IRequestHandler<CreateBlankCommand, BlankViewModel>
    {
        private readonly IMapper _mapper;

        private readonly Blank _blank;

        public CreateBlankHandler(IMapper mapper, Blank blank)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _blank = blank ?? throw new ArgumentNullException(nameof(blank));
        }

        public async Task<BlankViewModel> Handle(CreateBlankCommand request, CancellationToken cancellationToken)
        {
            var newSetting = await _blank.CreateBlankAsync(_mapper.Map<Blank>(request.Blank), cancellationToken)
                .ConfigureAwait(false);
            return _mapper.Map<BlankViewModel>(newSetting);
        }
    }
}
