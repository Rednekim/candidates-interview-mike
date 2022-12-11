using Microsoft.EntityFrameworkCore;

namespace Customers.Service.Test.TestHelper;

public static class DbContextFactoryExtensions
{
    
  public static void SetupDatabase<TDbContext>(
    this IDbContextFactory<TDbContext> dbContextFactory,
    Action<TDbContext>? setupAction = null)
    where TDbContext : DbContext
  {
    TDbContext dbContext = dbContextFactory.CreateDbContext();
    try
    {
      dbContext.Database.EnsureCreated();
      if (setupAction == null)
        return;
      setupAction(dbContext);
      dbContext.SaveChanges();
    }
    finally
    {
      if ((object) dbContext != null)
        ((IDisposable) dbContext).Dispose();
    }
  }

  public static async Task SetupDatabaseAsync<TDbContext>(
    this IDbContextFactory<TDbContext> dbContextFactory,
    Func<TDbContext, CancellationToken, Task>? setupFuncAsync = null,
    CancellationToken cancellationToken = default (CancellationToken))
    where TDbContext : DbContext
  {
    TDbContext dbContext = dbContextFactory.CreateDbContext();
    try
    {
      int num1 = await dbContext.Database.EnsureCreatedAsync(cancellationToken) ? 1 : 0;
      if (setupFuncAsync == null)
      {
        dbContext = default (TDbContext);
      }
      else
      {
        await setupFuncAsync(dbContext, cancellationToken);
        int num2 = await dbContext.SaveChangesAsync(cancellationToken);
        dbContext = default (TDbContext);
      }
    }
    finally
    {
      if ((object) dbContext != null)
        ((IDisposable) dbContext).Dispose();
    }
  }

  public static void AssertDatabaseContent<TDbContext>(
    this IDbContextFactory<TDbContext> dbContextFactory,
    Action<TDbContext> assertAction)
    where TDbContext : DbContext
  {
    TDbContext dbContext = dbContextFactory.CreateDbContext();
    try
    {
      assertAction(dbContext);
    }
    finally
    {
      if ((object) dbContext != null)
        ((IDisposable) dbContext).Dispose();
    }
  }

  public static async Task AssertDatabaseContentAsync<TDbContext>(
    this IDbContextFactory<TDbContext> dbContextFactory,
    Func<TDbContext, CancellationToken, Task> assertFuncAsync,
    CancellationToken cancellationToken = default (CancellationToken))
    where TDbContext : DbContext
  {
    TDbContext dbContext = dbContextFactory.CreateDbContext();
    try
    {
      await assertFuncAsync(dbContext, cancellationToken);
    }
    finally
    {
      if ((object) dbContext != null)
        ((IDisposable) dbContext).Dispose();
    }
    dbContext = default (TDbContext);
  }
}
