using AutoMapper;
using Mongemini.Service.Domain.Aggregates;
using Mongemini.Service.Infrastructure.Entities;

namespace Mongemini.Service.Domain.Mapper
{
    public class DomainProfiler : Profile
    {
        public DomainProfiler()
        {
            CreateMap<BlankEntity, Blank>().ReverseMap();
        }
    }
}
