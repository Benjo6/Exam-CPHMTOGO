import { Restaurant } from "@prisma/client";
import prisma from "../prisma/client";

async function getRestaurant(id: string) {
	return prisma.restaurant.findUnique({
		where: {
			id,
		},
		include: {
			menus: {
				include: {
					menuItems: true,
				},
			},
		},
	});
}

async function getAllRestaurants() {
	return prisma.restaurant.findMany({
		include: {
			menus: {
				include: {
					menuItems: true,
				},
			},
		},
	});
}

async function createRestaurant(restaurant: Restaurant) {
	console.log(restaurant);

	return prisma.restaurant.create({
		data: restaurant,
		include: {
			menus: {
				include: {
					menuItems: true,
				},
			},
		},
	});
}
async function updateRestaurant({ cvr, address, id, kontoNr, loginInfoId, name, regNr, role }: Restaurant) {
	return prisma.restaurant.update({
		where: {
			id,
		},
		data: {
			address,
			cvr,
			kontoNr,
			loginInfoId,
			accountId: "acct_1MBM0IEQFUzeCvJi",
			name,
			regNr,
			role,
		},
		include: {
			menus: {
				include: {
					menuItems: true,
				},
			},
		},
	});
}

async function deleteRestaurant(id: string) {
	return prisma.restaurant.delete({
		where: {
			id,
		},
		include: {
			menus: {
				include: {
					menuItems: true,
				},
			},
		},
	});
}

export default {
	createRestaurant,
	deleteRestaurant,
	getAllRestaurants,
	getRestaurant,
	updateRestaurant,
};
