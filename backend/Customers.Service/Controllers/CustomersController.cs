using Customers.Service.Domain;
using Customers.Service.DTO;
using Customers.Service.Entity;
using Customers.Service.Infrastructure;
using Customers.Service.Repository;
using Customers.Service.UtilityClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Customers.Service.Controllers;

[Route("api/customers")]
public class CustomersApiController : Controller
{
  private readonly ILogger _logger;
  private readonly ICustomersRepository _customersRepository;
  public CustomersApiController(ILoggerFactory loggerFactory,
    ICustomersRepository customersRepository)
  {
    _logger = loggerFactory.CreateLogger(nameof(CustomersApiController));
    _customersRepository = customersRepository;
  }

  /// <summary>
  /// Returns valid Customers with relative Orders and Orders Total from the Database.
  /// </summary>
  /// <param name="params"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  [HttpGet]
  [NoCache]
  [ProducesResponseType(typeof(ListResultClass<CustomerEntity>), 200)]
  [ProducesResponseType(400)]

  public async Task<ActionResult<ListResultClass<CustomerEntity>>> List(ListResultParams @params, CancellationToken cancellationToken = default)
    => await _customersRepository.GetListAsync(@params, cancellationToken);

  // GET api/customers/5
  [HttpGet("{id}", Name = "GetCustomerRoute")]
  [NoCache]
  [ProducesResponseType(typeof(Customer), 200)]
  [ProducesResponseType(400)]
  public async Task<ActionResult> Customers(int id)
  {
    throw new NotImplementedException();
  }

  /// <summary>
  /// Adds a new customer into the Database.
  /// </summary>
  /// <param name="customer">Customer to add.</param>
  /// <returns>ID of the newly created customer.</returns>
  // POST api/customers
  [HttpPost]
  [ProducesResponseType(201)]
  [ProducesResponseType(400)]
  public async Task<ActionResult<int>> CreateCustomer([FromBody] CustomerDTO customer, CancellationToken cancellationToken = default)
    => await _customersRepository.CreateAsync(customer, cancellationToken);

  // PUT api/customers/5
  [HttpPut("{id}")]
  [ProducesResponseType(200)]
  [ProducesResponseType(400)]
  public async Task<ActionResult> UpdateCustomer(int id, [FromBody] Customer customer)
  {
    throw new NotImplementedException();
  }

  // DELETE api/customers/5
  [HttpDelete("{id}")]
  [ProducesResponseType(200)]
  [ProducesResponseType(400)]
  public async Task<ActionResult> DeleteCustomer(int id)
  {
    throw new NotImplementedException();
  }
}
