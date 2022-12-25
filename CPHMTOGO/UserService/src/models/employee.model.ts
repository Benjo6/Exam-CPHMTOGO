import { Employee } from "@prisma/client";
import prisma from "../../prisma/client";
import isValidUuid from "../utils/checkUuid";

async function createEmployee(employee: Employee) {
	try {
		if (isValidCustomer(employee))
			return prisma.employee.create({
				data: employee,
			});
	} catch (e: any) {
		throw e;
	}
}

async function updateEmployee(employee: Employee) {
	try {
		if (isValidCustomer(employee))
			return prisma.employee.update({
				where: {
					id: employee.id,
				},
				data: employee,
			});
	} catch (e: any) {
		throw e;
	}
}

async function getEmployeeById(id: string) {
	try {
		if (!isValidUuid(id)) throw new Error("Id is not a valid uuid");
		return prisma.employee.findUnique({
			where: {
				id,
			},
		});
	} catch (e: any) {
		throw e;
	}
}

async function deleteEmployee(id: string) {
	try {
		if (!isValidUuid(id)) throw new Error("Id is not a valid uuid");
		prisma.employee.delete({
			where: {
				id,
			},
		});
		return null;
	} catch (e: any) {
		throw e;
	}
}

function isValidCustomer(employee: Employee) {
	if (!isValidUuid(employee.id) || !isValidUuid(employee.loginInfoId)) throw new Error("Employee.id or employee.loginInfoId is not a valid uuid");
	if (employee.firstname.length === 0) throw new Error("Employee.firstname is empty");
	if (employee.lastname.length === 0) throw new Error("Employee.lastname is empty");
	if (employee.active == undefined) throw new Error("Employee.active is empty");
	if (employee.role.length === 0) throw new Error("Employee.role is empty");
	if (employee.address.length === 0) throw new Error("Employee.address is empty");
	if (employee.kontoNr <= 0) throw new Error("Employee.kontoNr is zero or less");
	if (employee.regNr <= 0) throw new Error("Employee.regNr is zero or less");
	return true;
}

export default {
	createEmployee,
	updateEmployee,
	getEmployeeById,
	deleteEmployee,
};
