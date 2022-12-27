
namespace APIGateway.Models.OrderService;

public record CreateSpecificOrderItemModel( String Preference, Double Price, int Quantity, Guid OrderId);
public class CreateOrderItemModel
{
    public CreateOrderItemModel(String Preference, Double Price, int Quantity)
    {
        this.Preference = Preference;
        this.Price = Price;
        this.Quantity = Quantity;
    }

    public String Preference { get; set; }
    public Double Price { get; set; }
    public int Quantity { get; set; }

    public void Deconstruct(out String Preference, out Double Price, out int Quantity)
    {
        Preference = this.Preference;
        Price = this.Price;
        Quantity = this.Quantity;
    }
}
