using Customers.Service.DTO;
using Customers.Service.Entity;
using Customers.Service.UtilityClass;
using System.Threading;
using System.Threading.Tasks;

namespace Customers.Service.Repository
{
  public interface ICustomersRepository
  {
    Task<ListResultClass<CustomerEntity>> GetListAsync(ListResultParams @params, CancellationToken cancellationToken = default);
    Task<int> CreateAsync(CustomerDTO customer, CancellationToken cancellationToken = default);
  }
}
