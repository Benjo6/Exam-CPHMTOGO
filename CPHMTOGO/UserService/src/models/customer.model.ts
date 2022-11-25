import { Customer } from "@prisma/client";
import prisma from "../../prisma/client";
import isValidUuid from "../utils/checkUuid";

async function createCustomer(customer: Customer) {
	try {
		if (isValidCustomer(customer))
			return prisma.customer.create({
				data: customer,
			});
	} catch (e: any) {
		throw e;
	}
}

async function updateCustomer(customer: Customer) {
	try {
		if (isValidCustomer(customer))
			return prisma.customer.update({
				where: {
					id: customer.id,
				},
				data: customer,
			});
	} catch (e: any) {
		throw e;
	}
}

async function getCustomerById(id: string) {
	try {
		if (!isValidUuid(id)) throw new Error("Id is not a valid uuid");
		return prisma.customer.findUnique({
			where: {
				id,
			},
		});
	} catch (e: any) {
		throw e;
	}
}

async function deletCustomer(id: string) {
	try {
		if (!isValidUuid(id)) throw new Error("Id is not a valid uuid");
		prisma.customer.delete({
			where: {
				id,
			},
		});
		return null;
	} catch (e: any) {
		throw e;
	}
}

function isValidCustomer(customer: Customer) {
	if (!isValidUuid(customer.id) || !isValidUuid(customer.loginInfoId))
		throw new Error("Customer.id or customer.loginInfoId is not a valid uuid");
	if (customer.firstname.length === 0)
		throw new Error("Customer.firstname is empty");
	if (customer.lastname.length === 0)
		throw new Error("Customer.lastname is empty");
	if (customer.phone.toString().length !== 8)
		throw new Error("Customer.phone is not 8 digits");
	if (customer.role.length === 0) throw new Error("Customer.role is empty");
	if (customer.address.length === 0)
		throw new Error("Customer.address is empty");
	// if (customer.birtdate.toLocaleString() === "")
	// 	throw new Error("Cusotmer.birtday is empty or invalid");
	return true;
}

export default {
	createCustomer,
	updateCustomer,
	getCustomerById,
	deletCustomer,
};
