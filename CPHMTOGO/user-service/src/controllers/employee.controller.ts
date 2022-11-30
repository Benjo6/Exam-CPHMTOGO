import { v4 as uuid } from "uuid";
import { IEmployee } from "../interfaces/employee.interface";
import mapper from "../models/employee.model";

function createEmployee(employee: IEmployee) {
	return mapper.createEmployee({
		id: uuid(),
		firstname: employee.firstname,
		lastname: employee.lastname,
		active: employee.active,
		loginInfoId: employee.loginInfoId,
		address: employee.address,
		role: employee.role,
		kontoNr: employee.kontoNr,
		regNr: employee.regNr,
	});
}

function updateEmployee(employee: IEmployee, id: string) {
	return mapper.updateEmployee({
		id,
		firstname: employee.firstname,
		lastname: employee.lastname,
		active: employee.active,
		loginInfoId: employee.loginInfoId,
		address: employee.address,
		role: employee.role,
		kontoNr: employee.kontoNr,
		regNr: employee.regNr,
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
