using Microsoft.EntityFrameworkCore;

namespace Customers.Service.Test.TestHelper;

public sealed class SqliteInMemoryDbContextFactory<TDbContext> : IDbContextFactory<TDbContext>, IDisposable where TDbContext : DbContext
{
  private readonly SharedInMemorySqliteDatabaseProvider _sharedInMemorySqliteDatabaseProvider;
  private readonly Func<DbContextOptions, TDbContext> _createContext;
  private readonly DbContextOptions _dbContextOptions;
  private bool _isDisposed;

  public SqliteInMemoryDbContextFactory(
    Action<DbContextOptionsBuilder<TDbContext>>? optionsAction = null,
    Func<DbContextOptions, TDbContext>? createContext = null)
  {
    _sharedInMemorySqliteDatabaseProvider = new SharedInMemorySqliteDatabaseProvider();
    DbContextOptionsBuilder<TDbContext> contextOptionsBuilder = new DbContextOptionsBuilder<TDbContext>().UseSqlite<TDbContext>(_sharedInMemorySqliteDatabaseProvider.ConnectionString).EnableSensitiveDataLogging(true);
    if (optionsAction != null)
      optionsAction(contextOptionsBuilder);
    _dbContextOptions = contextOptionsBuilder.Options;
    _createContext = createContext ?? (Func<DbContextOptions, TDbContext>) (options => (TDbContext) Activator.CreateInstance(typeof (TDbContext), (object) options));
  }

  public TDbContext CreateDbContext() => _createContext(_dbContextOptions);

  public void Dispose() => Dispose(true);

  private void Dispose(bool disposing)
  {
    if (_isDisposed)
      return;
    if(disposing)
      _sharedInMemorySqliteDatabaseProvider.Dispose();
    _isDisposed = true;
  }
}
