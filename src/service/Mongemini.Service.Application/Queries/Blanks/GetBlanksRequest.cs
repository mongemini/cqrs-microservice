using Mongemini.Service.Application.ViewModels.Blanks;
using Mongemini.Application.Core.Queries;


namespace Mongemini.Service.Application.Queries.Blanks
{
    public class GetBlanksRequest : PagedRequest<BlankViewModel>
    {
        public string? BlankFilter { get; set; }
    }
}
