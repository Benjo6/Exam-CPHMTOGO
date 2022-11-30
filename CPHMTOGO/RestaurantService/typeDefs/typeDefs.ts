const typeDefs = `#graphql

  type Query {
    getRestaurant(id: String): Restaurant
    getAllRestaurants: [Restaurant]
    createRestaurant(restaurant: RestaurantInput): Restaurant
    updateRestaurant(restaurant: RestaurantInput): Restaurant
    deleteRestaurant(id: String): Restaurant
    
    getMenu(id: String): Menu
    getAllMenus: [Menu]
    creaetMenu(menu: MenuInput): Menu
    deleteMenu(id: String): Menu
    updateMenu(id: String, menu: MenuInput): Menu

    getMenuItem(id: String): MenuItem
    getAllMenuItems: [MenuItem]
    createMenuItem(menuItem: MenuItemInput): MenuItem
    deleteMenuItem(id: String): MenuItem
    updateMenuItem(menuItem: MenuItemInput): MenuItem
  }

  type Restaurant {
    id:          String
    name:        String
    address:      String
    loginInfoId: String
    cityId:      String
    kontoNr:     Int
    regNr:       Int
    CVR:         Int
    role:        String
    menus:       [Menu]
  }

  input RestaurantInput {
    id:          String
    name:        String
    address:      String
    loginInfoId: String
    cityId:      String
    kontoNr:     Int
    regNr:       Int
    CVR:         Int
    role:        String
  }

  type Menu {
    id:           String      
    title:        String
    restaurantId: String 
    menuItems:   [MenuItem]     
  }

  input MenuInput {
    id:           String
    title:        String
    restaurantId: String 
  }

  type MenuItem {
    id:          String
    name:        String
    description: String
    price:       Float
    menuId:      String
    foodType:    String
  }

  input MenuItemInput {
    id:          String
    name:        String
    description: String
    price:       Float
    menuId:      String
    foodType:    String
  }
`;

export default typeDefs;
