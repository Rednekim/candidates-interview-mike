namespace Customers.Service.Entity
{
  public class OrderEntity
  {
    public int Id { get; set; }
    public string Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
  }
}
