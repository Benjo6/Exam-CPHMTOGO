namespace APIGateway.Models.RestaurantService;

public record MenuItemModel(string id, string name, string description, double price, string menuId,string foodType);
public record CreateMenuItemModel( string name, string description, double price, string menuId,string foodType);