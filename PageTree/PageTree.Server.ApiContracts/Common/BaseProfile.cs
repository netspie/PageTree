using AutoMapper;

namespace PageTree.Server.ApiContracts.Common
{
    public abstract class BaseProfile : Profile
    {
        public void CreateMapCirc<TSource, TDestination>()
        {
            CreateMap<TSource, TDestination>();
            CreateMap<TDestination, TSource>();
        }
    }
}
