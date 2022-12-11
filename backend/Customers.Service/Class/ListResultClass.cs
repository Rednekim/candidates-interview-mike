using System.Collections.Generic;

namespace Customers.Service.UtilityClass
{
  public class ListResultClass<T>
  {
    public ICollection<T> Data { get; set; }
    public int Total { get; set; } = 0;
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
  }
}
