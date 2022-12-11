using System.Collections.Generic;

namespace Customers.Service.Entity
{
  public class CustomerEntity
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public StateEntity State { get; set; }
    public int StateId { get; set; }
    public int Zip { get; set; }
    public string Gender { get; set; }
    public int OrderCount { get; set; }
    public ICollection<OrderEntity> Orders { get; set; }
    public decimal? OrderTotal { get; set; }
  }
}
