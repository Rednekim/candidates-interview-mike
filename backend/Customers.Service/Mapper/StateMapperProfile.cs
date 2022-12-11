using AutoMapper;
using Customers.Service.Domain;
using Customers.Service.Entity;

namespace Customers.Service.Mapper
{
  public class StateMapperProfile : Profile
  {
    public StateMapperProfile()
    {
      CreateMap<StateEntity, State>()
        .ReverseMap();
    }
  }
}
