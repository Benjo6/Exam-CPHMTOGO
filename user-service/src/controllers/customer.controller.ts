import { ICompany } from "../interfaces/company.interface";
import { v4 as uuid } from "uuid";
import { ICustomer } from "../interfaces/customer.interface";
import mapper from "../models/customer.model";

function createCustomer(customer: ICustomer) {
	return mapper.createCustomer({
		id: uuid(),
		firstname: customer.firstname,
		lastname: customer.lastname,
		phone: customer.phone,
		birtdate: new Date(customer.birtdate),
		address: customer.address,
		loginInfoId: customer.loginInfoId,
		role: customer.role,
	});
}

function updateCustomer(customer: ICustomer, id: string) {
	return mapper.updateCustomer({
		id,
		firstname: customer.firstname,
		lastname: customer.lastname,
		phone: customer.phone,
		birtdate: new Date(customer.birtdate),
		address: customer.address,
		loginInfoId: customer.loginInfoId,
		role: customer.role,
	});
}

function getCustomerById(id: string) {
	return mapper.getCustomerById(id);
}

function deleteCustomer(id: string) {
	return mapper.deletCustomer(id);
}

export default {
	createCustomer,
	updateCustomer,
	deleteCustomer,
	getCustomerById,
};
