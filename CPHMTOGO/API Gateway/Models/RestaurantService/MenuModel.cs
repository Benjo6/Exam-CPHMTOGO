namespace APIGateway.Models.RestaurantService;

public record MenuModel(string id, string title, string restaurantId){
    public List<MenuItemModel> MenuItems { get; set; }

};

public record UpdateMenuModel(string id, string title, string restaurantId);
public record CreateMenuModel( string title, string restaurantId);