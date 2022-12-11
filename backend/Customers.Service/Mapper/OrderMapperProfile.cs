using AutoMapper;
using Customers.Service.Domain;
using Customers.Service.Entity;

namespace Customers.Service.Mapper
{
  public class OrderMapperProfile : Profile
  {
    public OrderMapperProfile()
    {
      CreateMap<OrderEntity, Order>()
        .ReverseMap();
    }
  }
}
