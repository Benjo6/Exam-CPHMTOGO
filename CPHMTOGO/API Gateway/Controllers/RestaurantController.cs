using APIGateway.Models.RestaurantService;
using GraphQL;
using GraphQL.Client.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace APIGateway.Controllers;

[Route("[Controller]")]
public class RestaurantController : ControllerBase
{
    private readonly IGraphQLClient _client;

    public RestaurantController(IGraphQLClient client)
    {
        // Initialize the GraphQL client with the endpoint of the GraphQL server
        _client = client;
    }
    [HttpGet("restaurant/{id}")]
    public async Task<RestaurantModel> GetRestaurant(string id)
    {
        // Construct the GraphQL query
        var query = new GraphQLRequest
        {
            Query = @"query($getRestaurantId: String)  {
  getRestaurant(id: $getRestaurantId) {
    id
    name
    loginInfoId
    address
    kontoNr
    regNr
    accountId
    cvr
    role
    menus {
      id
      title
      restaurantId
      menuItems {
        id
        name
        description
        price
        menuId
        foodType
      }
    }
  }
}",
            Variables = new
            {
                getRestaurantId = id
            }
        };

        // Execute the query and retrieve the result
        var response = await _client.SendQueryAsync<ResponseRestaurant>(query);
        return response.Data.getRestaurant;
    }
    
    [HttpGet("restaurant")]
    public async Task<List<RestaurantModel>> GetAllRestaurants()
    {
        // Construct the GraphQL query
        var query = new GraphQLRequest
        {
            Query = @"query  {
  getAllRestaurants {
    id
    name
    address
    loginInfoId
    kontoNr
    accountId
    regNr
    cvr
    role
    menus {
      id
      title
      restaurantId
      menuItems {
        id
        name
        description
        price
        menuId
        foodType
      }
    }
  }
}"

        };

        // Execute the query and retrieve the result
        var response = await _client.SendQueryAsync<ResponseRestaurant>(query);

        return response.Data.getAllRestaurants;
    }

    [HttpPost("restaurant")]
    public async Task<RestaurantModel> CreateRestaurant(CreateRestaurantModel model)
    {
        // Construct the GraphQL mutation
        var query = new GraphQLRequest
        {
            Query = @"query($restaurant: RestaurantInput)  {
  createRestaurant(restaurant: $restaurant) {
    id
    name
    address
    loginInfoId
    kontoNr
    regNr
    accountId
    cvr
    role
  }
}",
            Variables = new
            {
                restaurant=model
            }
        };

        // Execute the mutation and retrieve the result
        var response = await _client.SendMutationAsync<ResponseRestaurant>(query);
        return response.Data.createRestaurant;
    }

    [HttpPut("restaurant")]
    public async Task<RestaurantModel> UpdateRestaurant(UpdateRestaurantModel model)
    {
        // Construct the GraphQL mutation
        var query = new GraphQLRequest()
        {
            Query = @"query($restaurant: RestaurantInput)  {
  updateRestaurant(restaurant: $restaurant) {
    id
    name
    address
    loginInfoId
    kontoNr
    regNr
    accountId
    cvr
    role
  }
}",
            Variables = new
            {
                restaurant = model
            }
        };
        var response = await _client.SendMutationAsync<ResponseRestaurant>(query);
        return response.Data.updateRestaurant;

    }
    
    [HttpDelete("restaurant/{id}")]
    public async Task<RestaurantModel> DeleteRestaurant(string id)
    {
        var query = new GraphQLRequest()
        {
            Query = @"query($deleteRestaurantId: String)  {
  deleteRestaurant(id: $deleteRestaurantId) {
    id
    name
    address
    loginInfoId
    kontoNr
    regNr
    accountId
    cvr
    role
    menus {
      id
      title
      restaurantId
      menuItems {
        id
        name
        description
        price
        menuId
        foodType
      }
    }
  }
}",
            Variables = new
            {
                deleteRestaurantId=id
            }
        };
        var response = await _client.SendMutationAsync<ResponseRestaurant>(query);
        return response.Data.deleteRestaurant;
    }

    [HttpGet("menu")]
    public async Task<List<MenuModel>> GetAllMenu()
    {
        var query = new GraphQLRequest
        {
            Query = @"query  {
  getAllMenus {
    id
    title
    restaurantId
    menuItems {
      id
      name
      description
      price
      menuId
      foodType
    }
  }
}"
        };

        // Execute the query and retrieve the result
        var response = await _client.SendQueryAsync<ResponseMenu>(query);

        return response.Data.getAllMenus;
    }
    
    [HttpGet("menu/{id}")]
    public async Task<MenuModel> GetMenu(string id)
    {
        // Construct the GraphQL query
        var query = new GraphQLRequest
        {
            Query = @"query($getMenuId: String)  {
  getMenu(id: $getMenuId) {
    title
    id
    restaurantId
    menuItems {
      id
      name
      description
      price
      menuId
      foodType
    }
  }
}",
            Variables = new
            {
                getMenuId = id
            }
        };

        // Execute the query and retrieve the result
        var response = await _client.SendQueryAsync<ResponseMenu>(query);
        return response.Data.getMenu;
    }
    [HttpPost("menu")]
    public async Task<MenuModel> CreateMenu(CreateMenuModel model)
    {
        // Construct the GraphQL mutation
        var query = new GraphQLRequest
        {
            Query = @"query($menu: MenuInput)  {
  creaetMenu(menu: $menu) {
    id
    title
    restaurantId
  }
}",
            Variables = new
            {
                menu=model
            }
        };

        // Execute the mutation and retrieve the result
        var response = await _client.SendMutationAsync<ResponseRestaurant>(query);
        return response.Data.creaetMenu;
    }
    [HttpPut("menu")]
    public async Task<UpdateMenuModel> UpdateMenu(UpdateMenuModel model)
    {
        // Construct the GraphQL mutation
        var query = new GraphQLRequest()
        {
            Query = @"query($updateMenuId: String, $menu: MenuInput)  {
  updateMenu(id: $updateMenuId, menu: $menu) {
    id
    title
    restaurantId
  }
}",
            Variables = new
            {
                menu = model,
                updateMenuId = model.id
            }
        };
        var response = await _client.SendMutationAsync<ResponseMenu>(query);
        return response.Data.updateMenu;

    }
    [HttpDelete("menu/{id}")]
    public async Task<MenuModel> DeleteMenu(string id)
    {
        var query = new GraphQLRequest()
        {
            Query = @"query($deleteMenuId: String)  {
  deleteMenu(id: $deleteMenuId) {
    id
    title
    restaurantId
    menuItems {
      id
      name
      description
      price
      foodType
      menuId
    }
  }
}",
            Variables = new
            {
                deleteMenuId=id
            }
        };
        var response = await _client.SendMutationAsync<ResponseMenu>(query);
        return response.Data.deleteMenu;
    }
    [HttpGet("menuitem")]
    public async Task<List<MenuItemModel>> GetAllMenuItem()
    {
        var query = new GraphQLRequest
        {
            Query = @"query  {
  getAllMenuItems {
    id
    name
    description
    menuId
    price
    foodType
  }
}"
        };

        // Execute the query and retrieve the result
        var response = await _client.SendQueryAsync<ResponseMenuItem>(query);

        return response.Data.getAllMenuItems;
    }
    
    [HttpGet("menuitem/{id}")]
    public async Task<MenuItemModel> GetMenuItem(string id)
    {
        // Construct the GraphQL query
        var query = new GraphQLRequest
        {
            Query = @"query($getMenuItemId: String)  {
  getMenuItem(id: $getMenuItemId) {
    id
    name
    description
    price
    menuId
    foodType
  }
}",
            Variables = new
            {
                getMenuItemId = id
            }
        };

        // Execute the query and retrieve the result
        var response = await _client.SendQueryAsync<ResponseMenuItem>(query);
        return response.Data.getMenuItem;
    }
    [HttpPost("menuitem")]
    public async Task<MenuItemModel> CreateMenu(CreateMenuItemModel model)
    {
        // Construct the GraphQL mutation
        var query = new GraphQLRequest
        {
            Query = @"query($menuItem: MenuItemInput)  {
  createMenuItem(menuItem: $menuItem) {
    id
    price
    name
    description
    menuId
    foodType
  }
}",
            Variables = new
            {
                menuItem=model
            }
        };

        // Execute the mutation and retrieve the result
        var response = await _client.SendMutationAsync<ResponseRestaurant>(query);
        return response.Data.createMenuItem;
    }
    [HttpPut("menuitem")]
    public async Task<MenuItemModel> UpdateMenuItem(MenuItemModel model)
    {
        // Construct the GraphQL mutation
        var query = new GraphQLRequest()
        {
            Query = @"query($menuItem: MenuItemInput)  {
  updateMenuItem(menuItem: $menuItem) {
    id
    name
    description
    price
    menuId
    foodType
  }
}",
            Variables = new
            {
                menuItem = model,
            }
        };
        var response = await _client.SendMutationAsync<ResponseMenu>(query);
        return response.Data.updateMenuItem;

    }
    [HttpDelete("menuitem/{id}")]
    public async Task<MenuItemModel> DeleteMenuItem(string id)
    {
        var query = new GraphQLRequest()
        {
            Query = @"query($deleteMenuItemId: String)  {
  deleteMenuItem(id: $deleteMenuItemId) {
    name
    id
    description
    price
    menuId
    foodType
  }
}",
            Variables = new
            {
                deleteMenuItemId=id
            }
        };
        var response = await _client.SendMutationAsync<ResponseMenuItem>(query);
        return response.Data.deleteMenuItem;
    }

    
}

public class ResponseMenuItem
{
    public MenuItemModel getMenuItem { get; set; }
    public List<MenuItemModel> getAllMenuItems { get; set; }
    public MenuItemModel deleteMenuItem { get; set; }
}

public class ResponseMenu
{
    public List<MenuModel> getAllMenus;
    public UpdateMenuModel updateMenu;
    public MenuItemModel updateMenuItem;
    public MenuModel getMenu { get; set; }
    public MenuModel deleteMenu { get; set; }
}

public class ResponseRestaurant
{
    public RestaurantModel updateRestaurant;
    public MenuModel creaetMenu;
    public MenuItemModel createMenuItem;
    public string getAccountId;
    public List<RestaurantModel> getAllRestaurants { get; set; }
    public RestaurantModel getRestaurant { get; set; }
    public RestaurantModel createRestaurant { get; set; }
    public RestaurantModel deleteRestaurant { get; set; }
}


