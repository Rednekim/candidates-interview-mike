using AutoMapper;
using Customers.Service.Domain;
using Customers.Service.Entity;
using Customers.Service.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Customers.Service.Repository
{
  public class StatesRepository : IStatesRepository
  {
    private readonly CustomersDbContext _Context;
    private readonly ILogger _Logger;
    private readonly IMapper _Mapper;

    public StatesRepository(CustomersDbContext context, ILoggerFactory loggerFactory, IMapper mapper)
    {
      _Context = context;
      _Logger = loggerFactory.CreateLogger("StatesRepository");
      _Mapper = mapper;
    }

    public async Task<List<StateEntity>> GetStatesAsync(CancellationToken cancellationToken = default)
    {
      return await _Context.States
        .OrderBy(s => s.Abbreviation)
        .Select(s => _Mapper.Map<State, StateEntity>(s))
        .ToListAsync(cancellationToken);
    }
  }
}
