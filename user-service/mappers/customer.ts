import { Customer } from "@prisma/client";
import prisma from "../prisma/client";
import isValidUuid from "../utils/checkUuid";

export async function createCustomer(customer: Customer) {
	try {
		if (isValidCustomer(customer))
			return await prisma.customer.create({
				data: customer,
			});
	} catch (e: unknown) {
		return e;
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
