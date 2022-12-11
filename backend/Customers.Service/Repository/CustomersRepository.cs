using AutoMapper;
using Customers.Service.Domain;
using Customers.Service.DTO;
using Customers.Service.Entity;
using Customers.Service.Infrastructure;
using Customers.Service.UtilityClass;
using Customers.Service.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Customers.Service.Repository
{
  public class CustomersRepository : ICustomersRepository
  {
    private readonly CustomersDbContext _Context;
    private readonly ILogger _Logger;
    private readonly IMapper _Mapper;

    public CustomersRepository(CustomersDbContext context, ILoggerFactory loggerFactory, IMapper mapper)
    {
      _Context = context;
      _Logger = loggerFactory.CreateLogger(nameof(CustomersRepository));
      _Mapper = mapper;
    }

    public async Task<int> CreateAsync(CustomerDTO customer, CancellationToken cancellationToken = default)
    {
      _Logger.LogInformation("{MethodName} - Adding new customer to the database:", nameof(CreateAsync));
      Customer customerToAdd = _Mapper.Map<Customer>(customer);
      await _Context.AddAsync(customerToAdd, cancellationToken);
      try
      {
        await _Context.SaveChangesAsync(cancellationToken);

        _Logger.LogInformation("{MethodName} - Successfully added new customer with ID: {CustomerId}."
          , nameof(CreateAsync), customerToAdd.Id);
        return customer.Id;
      }
      catch (TaskCanceledException ex)
      {
        _Logger.LogException(nameof(CreateAsync), ex.Message, ex.StackTrace);
        return -1;
      }
      catch (Exception ex)
      {
        _Logger.LogException(nameof(CreateAsync), ex.Message, ex.StackTrace);
        throw;
      }
    }
    public async Task<ListResultClass<CustomerEntity>> GetListAsync(ListResultParams @params, CancellationToken cancellationToken = default)
    {
      _Logger.LogInformation("{MethodName} - Retrieving customers from the database...", nameof(GetListAsync));
      ListResultClass<CustomerEntity> resultClass = new()
      {
        PageSize = @params.PageSize,
        PageNumber = @params.PageNumber
      };

      IQueryable<Customer> dataQuery =
              _Context.Customers
              .Include(c => c.State)
              .Include(c => c.Orders)
              .AsQueryable();

      if (@params is not null)
      {
        if (@params.PageNumber is not null && @params.PageSize is not null)
        {
          dataQuery = dataQuery
            .Skip(@params.PageSize.Value * (@params.PageNumber.Value - 1))
          .Take(@params.PageSize.Value);
        }
      }
      List<Task> tasks = new()
      {
        //COUNT QUERY
        _Context.Customers.CountAsync(cancellationToken)
        .ContinueWith(task =>
        {
          if (task.IsCompletedSuccessfully)
          {
            resultClass.Total = task.Result;
          }
        }, cancellationToken),
        //DATA QUERY
        dataQuery.Select(c => _Mapper.Map<CustomerEntity>(c)).ToListAsync(cancellationToken)
        .ContinueWith(task =>
        {
          if (task.IsCompletedSuccessfully)
          {
            resultClass.Data = task.Result;
          }
        }, cancellationToken)
      };

      try
      {
        await Task.WhenAll(tasks);
      }
      catch (TaskCanceledException ex)
      {
        _Logger.LogException(nameof(GetListAsync), ex.Message, ex.StackTrace);
      }
      catch (Exception ex)
      {
        _Logger.LogException(nameof(GetListAsync), ex.Message, ex.StackTrace);
        throw;
      }

      _Logger.LogInformation("{MethodName} - Found {CustomersCount} customers.", nameof(GetListAsync), resultClass.Total);
      return resultClass;
    }
  }
}
