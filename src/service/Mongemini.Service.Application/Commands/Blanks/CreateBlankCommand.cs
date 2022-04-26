using MediatR;
using Mongemini.Service.Application.ViewModels.Blanks;

namespace Mongemini.Service.Application.Commands.Blanks
{
    public class CreateBlankCommand : IRequest<BlankViewModel>
    {
        public CreateBlankCommand(BlankViewModel blank)
        {
            Blank = blank;
        }

        public BlankViewModel Blank { get; set; }
    }
}
