import { Employee } from "@prisma/client";
import { v4 as uuid } from "uuid";
import mapper from "../models/employee.model";

function createEmployee(employee: Employee) {
	return mapper.createEmployee({
		id: uuid(),
		firstname: employee.firstname,
		lastname: employee.lastname,
		active: employee.active,
		loginInfoId: employee.loginInfoId,
		address: employee.address,
		role: "Employee",
		kontoNr: employee.kontoNr,
		regNr: employee.regNr,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	});
}

function updateEmployee(employee: Employee, id: string) {
	return mapper.updateEmployee({
		id,
		firstname: employee.firstname,
		lastname: employee.lastname,
		active: employee.active,
		loginInfoId: employee.loginInfoId,
		address: employee.address,
		role: "Employee",
		kontoNr: employee.kontoNr,
		regNr: employee.regNr,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	});
}

function getEmployeeById(id: string) {
	return mapper.getEmployeeById(id);
}

function deleteEmployee(id: string) {
	return mapper.deleteEmployee(id);
}

export default {
	createEmployee,
	updateEmployee,
	getEmployeeById,
	deleteEmployee,
};
