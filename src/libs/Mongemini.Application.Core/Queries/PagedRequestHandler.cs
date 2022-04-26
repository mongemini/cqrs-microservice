using MediatR;
using Mongemini.Application.Core.Contracts;
using Mongemini.Application.Core.ViewModels;

namespace Mongemini.Application.Core.Queries
{
    public abstract class PagedRequestHandler<TRequest, TResult> : IRequestHandler<TRequest, PagedList<TResult>>
        where TRequest : IRequest<PagedList<TResult>>, IPagedRequest
    {

        public abstract Task<PagedList<TResult>> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
