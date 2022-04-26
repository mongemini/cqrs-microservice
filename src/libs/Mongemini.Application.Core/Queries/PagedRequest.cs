using MediatR;
using Mongemini.Application.Core.Contracts;
using Mongemini.Application.Core.ViewModels;

namespace Mongemini.Application.Core.Queries
{
    public abstract class PagedRequest<TResult> : IRequest<PagedList<TResult>>, IPagedRequest
    {
        public string Sort { get; set; }
        public int Direction { get; set; }

        public int? Page { get; set; }
        public int? Size { get; set; }
    }
}
