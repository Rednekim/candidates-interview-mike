using AutoMapper;
using Customers.Service.Domain;
using Customers.Service.DTO;
using Customers.Service.Entity;
using System.Linq;

namespace Customers.Service.Mapper
{
  public class CustomerMapperProfile : Profile
  {
    public CustomerMapperProfile()
    {
      CreateMap<CustomerDTO, Customer>()
        .ReverseMap();
      CreateMap<CustomerEntity, Customer>()
        .ReverseMap();
      CreateMap<Customer, CustomerEntity>()
        .ForMember(dst => dst.OrderTotal, opt => opt.MapFrom(src => src.Orders.Sum(o => o.Price)));
    }
  }
}
