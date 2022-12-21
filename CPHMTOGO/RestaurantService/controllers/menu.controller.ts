import { Menu } from "@prisma/client";
import prisma from "../prisma/client";

async function getMenu(id: string) {
	return prisma.menu.findUnique({
		where: {
			id,
		},
	});
}

async function getAllMenus() {
	return prisma.menu.findMany({
		include: {
			menuItems: true,
		},
	});
}

async function creaetMenu(menu: Menu) {
	return prisma.menu.create({
		data: menu,
		include: {
			menuItems: true,
		},
	});
}

async function deleteMenu(id: string) {
	return prisma.menu.delete({
		where: {
			id,
		},
		include: {
			menuItems: true,
		},
	});
}

async function updateMenu(id: string, title: string) {
	return prisma.menu.update({
		where: {
			id,
		},
		data: {
			title,
		},
		include: {
			menuItems: true,
		},
	});
}

export default {
	creaetMenu,
	deleteMenu,
	getAllMenus,
	getMenu,
	updateMenu,
};
