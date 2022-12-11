using Customers.Service.Domain;
using Customers.Service.Infrastructure;
using Customers.Service.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
// ReSharper disable TemplateIsNotCompileTimeConstantProblem

namespace Customers.Service.Controllers;

[Route("api/states")]
public class StatesApiController : Controller
{
    readonly IStatesRepository _statesRepository;
    readonly ILogger _logger;

    public StatesApiController(IStatesRepository statesRepo, ILoggerFactory loggerFactory)
    {
        _statesRepository = statesRepo;
        _logger = loggerFactory.CreateLogger(nameof(StatesApiController));
    }

    [HttpGet]
    [NoCache]
    [ProducesResponseType(typeof(List<State>), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> States()
    {
        try
        {
            var states = await _statesRepository.GetStatesAsync();
            return Ok(states);
        }
        catch (Exception exp)
        {
            _logger.LogError(exp.Message);
            return BadRequest();
        }
    }

}

