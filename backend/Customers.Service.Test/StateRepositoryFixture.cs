using AutoMapper;
using Customers.Service.Domain;
using Customers.Service.Infrastructure;
using Customers.Service.Mapper;
using Customers.Service.Repository;
using Customers.Service.Test.TestHelper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Customers.Service.Test;

public class StateRepositoryFixture
{
  private readonly StatesRepository _sut;
  private readonly SqliteInMemoryDbContextFactory<CustomersDbContext> _testDatabase;
  private readonly CustomersDbContext _dbContext;
  private readonly IMapper _mapper;

  public StateRepositoryFixture()
  {
    var loggerFactory = Substitute.For<ILoggerFactory>();

    _testDatabase = new SqliteInMemoryDbContextFactory<CustomersDbContext>();
    _testDatabase.SetupDatabase();
    _dbContext = _testDatabase.CreateDbContext();

    MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile<StateMapperProfile>());
    _mapper = config.CreateMapper();
    _sut = new StatesRepository(_dbContext, loggerFactory, _mapper);
  }
  [Fact]
  public async Task MockedDbContextExample()
  {
    var statesRange = new List<State>
    {
      new() {Name = "Test-Name-1", Abbreviation = "TN1"},
      new() {Name = "Test-Name-2", Abbreviation = "TN2"},
      new() {Name = "Test-Name-3", Abbreviation = "TN3"},
    };

    await _dbContext.States.AddRangeAsync(statesRange);
    await _dbContext.SaveChangesAsync();

    var result = await _sut.GetStatesAsync();

    result.Should().HaveCount(3);
    result.Should().ContainSingle(x => x.Abbreviation == "TN1")
      .And.ContainSingle(x => x.Abbreviation == "TN2")
      .And.ContainSingle(x => x.Abbreviation == "TN3");
  }
}
