import restaurant from "../controllers/restaurant.controller";
import menu from "../controllers/menu.controller";
import menuItem from "../controllers/menuItem.controller";
import { RestaurantArgs } from "../interfaces/restaurant.interface";
import { v4 as uuid } from "uuid";
import { IMenuArgs } from "../interfaces/menu.interface";
import { IMenuItemArgs } from "../interfaces/menuItem.interface";

const resolvers = {
	Query: {
		getRestaurant: async (parent: any, { id }: any, context: any, info: any) => {
			return await restaurant.getRestaurant(id);
		},

		getAllRestaurants: async (parent: any, args: any, context: any, info: any) => {
			return await restaurant.getAllRestaurants();
		},

		createRestaurant: async (
			parent: any,
			{ restaurant: { cvr, address, kontoNr, loginInfoId, name, regNr } }: RestaurantArgs,
			context: any,
			info: any
		) => {
			return await restaurant.createRestaurant({
				id: uuid(),
				address,
				cvr,
				kontoNr,
				loginInfoId,
				name,
				regNr,
				role: "Restaurant",
				accountId: "acct_1MBM0IEQFUzeCvJi",
			});
		},

		updateRestaurant: async (
			parent: any,
			{ restaurant: { id, cvr, address, kontoNr, loginInfoId, name, regNr } }: RestaurantArgs,
			context: any,
			info: any
		) => {
			return await restaurant.updateRestaurant({
				address,
				cvr,
				id,
				kontoNr,
				loginInfoId,
				name,
				regNr,
				role: "Restaurant",
				accountId: "acct_1MBM0IEQFUzeCvJi",
			});
		},

		deleteRestaurant: async (parent: any, { id }: any, context: any, info: any) => {
			return await restaurant.deleteRestaurant(id);
		},

		getMenu: async (parent: any, { id }: any, context: any, info: any) => {
			return await menu.getMenu(id);
		},

		getAllMenus: async (parent: any, args: any, context: any, info: any) => {
			return await menu.getAllMenus();
		},

		creaetMenu: async (parent: any, { menu: { title, restaurantId } }: IMenuArgs, context: any, info: any) => {
			return await menu.creaetMenu({
				id: uuid(),
				restaurantId,
				title,
			});
		},

		deleteMenu: async (parent: any, { id }: any, context: any, info: any) => {
			return await menu.deleteMenu(id);
		},

		updateMenu: async (parent: any, { menu: { id, title } }: IMenuArgs, context: any, info: any) => {
			return await menu.updateMenu(id, title);
		},

		getMenuItem: async (parent: any, { id }: any, context: any, info: any) => {
			return await menuItem.getMenuItem(id);
		},

		getAllMenuItems: async (parent: any, args: any, context: any, info: any) => {
			return await menuItem.getAllMenuItems();
		},

		createMenuItem: async (parent: any, { menuItem: { description, foodType, menuId, name, price } }: IMenuItemArgs, context: any, info: any) => {
			return await menuItem.createMenuItem({
				id: uuid(),
				description,
				foodType,
				menuId,
				name,
				price,
			});
		},

		deleteMenuItem: async (parent: any, { id }: any, context: any, info: any) => {
			return await menuItem.deleteMenuItem(id);
		},

		updateMenuItem: async (parent: any, { menuItem: { id, description, foodType, name, price, menuId } }: IMenuItemArgs, context: any, info: any) => {
			return await menuItem.updateMenuItem(id, description, name, price, foodType);
		},
	},
};

export default resolvers;
