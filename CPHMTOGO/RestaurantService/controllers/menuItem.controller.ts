import { MenuItems } from "@prisma/client";
import prisma from "../prisma/client";

async function getMenuItem(id: string) {
	return prisma.menuItems.findUnique({
		where: {
			id,
		},
	});
}

async function getAllMenuItems() {
	return prisma.menuItems.findMany({});
}

async function createMenuItem(menuItem: MenuItems) {
	return prisma.menuItems.create({
		data: menuItem,
	});
}

async function deleteMenuItem(id: string) {
	return prisma.menuItems.delete({
		where: {
			id,
		},
	});
}

async function updateMenuItem(id: string, description: string, name: string, price: number, foodType: string) {
	return prisma.menuItems.update({
		where: {
			id,
		},
		data: {
			description,
			name,
			price,
			foodType,
		},
	});
}

export default {
	createMenuItem,
	deleteMenuItem,
	updateMenuItem,
	getAllMenuItems,
	getMenuItem,
};
