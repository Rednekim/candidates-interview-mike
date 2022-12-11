using Customers.Service.Entity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Customers.Service.Repository
{
  public interface IStatesRepository
  {
    Task<List<StateEntity>> GetStatesAsync(CancellationToken cancellationToken = default);
  }
}
