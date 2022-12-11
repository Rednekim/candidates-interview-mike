using Microsoft.Data.Sqlite;

namespace Customers.Service.Test.TestHelper;

public sealed class SharedInMemorySqliteDatabaseProvider : IDisposable
{
  private readonly SqliteConnection _keepAliveConnection;

  /// <summary>
  ///     Initializes a new instance of the <see cref="T:Post.Common.Testing.EntityFrameworkCore.Persistence.SharedInmemorySqliteDatabaseProvider" /> class.
  /// </summary>
  public SharedInMemorySqliteDatabaseProvider()
  {
    ConnectionString = "DataSource=" + Guid.NewGuid().ToString("N") + ";mode=memory;cache=shared";
    _keepAliveConnection = new SqliteConnection(ConnectionString);
    _keepAliveConnection.Open();
  }

  /// <summary>
  ///     The ConnectionString to be used to connect to the in memory database.
  /// </summary>
  public string ConnectionString { get; }

  /// <inheritdoc />
  public void Dispose()
  {
    _keepAliveConnection.Close();
    _keepAliveConnection.Dispose();
  }
}
