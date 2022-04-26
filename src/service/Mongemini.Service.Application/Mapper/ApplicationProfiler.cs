using AutoMapper;
using Mongemini.Service.Application.Queries.Blanks;
using Mongemini.Service.Application.ViewModels.Blanks;
using Mongemini.Service.Domain.Aggregates;
using Mongemini.Service.Infrastructure.Entities;
using Mongemini.Service.Infrastructure.Filters;

namespace Mongemini.Service.Application.Mapper
{
    public class ApplicationProfiler : Profile
    {
        public ApplicationProfiler()
        {
            CreateMap<BlankEntity, BlankViewModel>().ReverseMap();
            CreateMap<Blank, BlankViewModel>().ReverseMap();

            CreateMap<GetBlanksRequest, BlankMainFilter>();
        }
    }
}
