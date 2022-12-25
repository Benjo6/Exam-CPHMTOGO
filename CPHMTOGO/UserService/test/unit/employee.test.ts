import { prismaMock } from "../../src/singleton";
import { v4 as uuid } from "uuid";
import model from "../../src/models/employee.model";

test("Should create new employee", async () => {
	const employeeId = uuid();
	const loginInfoId = uuid();
	const employee = {
		id: employeeId,
		firstname: "abed",
		lastname: "Hariri",
		active: true,
		loginInfoId,
		address: "fake Street 23 3000 Helsingør",
		role: "Employee",
		kontoNr: 123123123,
		regNr: 1233,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	};

	prismaMock.employee.create.mockResolvedValue(employee);

	await expect(model.createEmployee(employee)).resolves.toEqual({
		id: employeeId,
		firstname: "abed",
		lastname: "Hariri",
		active: true,
		loginInfoId,
		address: "fake Street 23 3000 Helsingør",
		role: "Employee",
		kontoNr: 123123123,
		regNr: 1233,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	});
});

test("Should fail if id is not uuid on create", async () => {
	const loginInfoId = uuid();
	const employee = {
		id: "employeeId",
		firstname: "abed",
		lastname: "Hariri",
		active: true,
		loginInfoId,
		address: "fake Street 23 3000 Helsingør",
		role: "Employee",
		kontoNr: 123123123,
		regNr: 1233,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	};

	prismaMock.employee.create.mockRejectedValue(employee);

	await expect(model.createEmployee(employee)).rejects.toThrow(new Error("Employee.id or employee.loginInfoId is not a valid uuid"));
});

test("Throw if employee.firstname is empty on create", async () => {
	const employeeId = uuid();
	const loginInfoId = uuid();
	const employee = {
		id: employeeId,
		firstname: "",
		lastname: "Hariri",
		active: true,
		loginInfoId,
		address: "fake Street 23 3000 Helsingør",
		role: "Employee",
		kontoNr: 123123123,
		regNr: 1233,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	};

	prismaMock.employee.create.mockRejectedValue(employee);

	await expect(model.createEmployee(employee)).rejects.toThrow(new Error("Employee.firstname is empty"));
});

test("Throw if employee.lastname is empty on create", async () => {
	const employeeId = uuid();
	const loginInfoId = uuid();
	const employee = {
		id: employeeId,
		firstname: "Abed",
		lastname: "",
		active: true,
		loginInfoId,
		address: "fake Street 23 3000 Helsingør",
		role: "Employee",
		kontoNr: 123123123,
		regNr: 1233,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	};

	prismaMock.employee.create.mockRejectedValue(employee);

	await expect(model.createEmployee(employee)).rejects.toThrow(new Error("Employee.lastname is empty"));
});

test("Throw if employee.role is empty on create", async () => {
	const employeeId = uuid();
	const loginInfoId = uuid();
	const employee = {
		id: employeeId,
		firstname: "Abed",
		lastname: "Hariri",
		active: true,
		loginInfoId,
		address: "fake Street 23 3000 Helsingør",
		role: "",
		kontoNr: 123123123,
		regNr: 1233,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	};

	prismaMock.employee.create.mockRejectedValue(employee);

	await expect(model.createEmployee(employee)).rejects.toThrow(new Error("Employee.role is empty"));
});

test("Throw if employee.loginInfoId is invalid on create", async () => {
	const employeeId = uuid();
	const employee = {
		id: employeeId,
		firstname: "Abed",
		lastname: "Hariri",
		active: true,
		loginInfoId: "asdasd",
		address: "fake Street 23 3000 Helsingør",
		role: "Employee",
		kontoNr: 123123123,
		regNr: 1233,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	};

	prismaMock.employee.create.mockRejectedValue(employee);

	await expect(model.createEmployee(employee)).rejects.toThrow(new Error("Employee.id or employee.loginInfoId is not a valid uuid"));
});

test("Throw if employee.address is empty on create", async () => {
	const employeeId = uuid();
	const loginInfoId = uuid();
	const employee = {
		id: employeeId,
		firstname: "Abed",
		lastname: "Hariri",
		active: true,
		loginInfoId,
		address: "",
		role: "Employee",
		kontoNr: 123123123,
		regNr: 1233,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	};

	prismaMock.employee.create.mockRejectedValue(employee);

	await expect(model.createEmployee(employee)).rejects.toThrow(new Error("Employee.address is empty"));
});

test("Throw if employee.kontoNr is empty on create", async () => {
	const employeeId = uuid();
	const loginInfoId = uuid();
	const employee = {
		id: employeeId,
		firstname: "Abed",
		lastname: "Hariri",
		active: true,
		loginInfoId,
		address: "Fake street 23, 3000 Helsingør",
		role: "Employee",
		kontoNr: 0,
		regNr: 1233,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	};

	prismaMock.employee.create.mockRejectedValue(employee);

	await expect(model.createEmployee(employee)).rejects.toThrow(new Error("Employee.kontoNr is zero or less"));
});

test("Throw if employee.regNr is empty on create", async () => {
	const employeeId = uuid();
	const loginInfoId = uuid();
	const employee = {
		id: employeeId,
		firstname: "Abed",
		lastname: "Hariri",
		active: true,
		loginInfoId,
		address: "Fake street 23, 3000 Helsingør",
		role: "Employee",
		kontoNr: 12312313,
		regNr: 0,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	};

	prismaMock.employee.create.mockRejectedValue(employee);

	await expect(model.createEmployee(employee)).rejects.toThrow(new Error("Employee.regNr is zero or less"));
});

test("Should update new employee", async () => {
	const employeeId = uuid();
	const loginInfoId = uuid();
	const employee = {
		id: employeeId,
		firstname: "abed",
		lastname: "Hariri",
		active: true,
		loginInfoId,
		address: "fake Street 23 3000 Helsingør",
		role: "Employee",
		kontoNr: 123123123,
		regNr: 1233,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	};

	prismaMock.employee.update.mockResolvedValue(employee);

	await expect(model.updateEmployee(employee)).resolves.toEqual({
		id: employeeId,
		firstname: "abed",
		lastname: "Hariri",
		active: true,
		loginInfoId,
		address: "fake Street 23 3000 Helsingør",
		role: "Employee",
		kontoNr: 123123123,
		regNr: 1233,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	});
});

test("Should fail if id is not uuid on update", async () => {
	const loginInfoId = uuid();
	const employee = {
		id: "employeeId",
		firstname: "abed",
		lastname: "Hariri",
		active: true,
		loginInfoId,
		address: "fake Street 23 3000 Helsingør",
		role: "Employee",
		kontoNr: 123123123,
		regNr: 1233,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	};

	prismaMock.employee.update.mockRejectedValue(employee);

	await expect(model.updateEmployee(employee)).rejects.toThrow(new Error("Employee.id or employee.loginInfoId is not a valid uuid"));
});

test("Throw if employee.firstname is empty on update", async () => {
	const employeeId = uuid();
	const loginInfoId = uuid();
	const employee = {
		id: employeeId,
		firstname: "",
		lastname: "Hariri",
		active: true,
		loginInfoId,
		address: "fake Street 23 3000 Helsingør",
		role: "Employee",
		kontoNr: 123123123,
		regNr: 1233,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	};

	prismaMock.employee.update.mockRejectedValue(employee);

	await expect(model.updateEmployee(employee)).rejects.toThrow(new Error("Employee.firstname is empty"));
});

test("Throw if employee.lastname is empty on update", async () => {
	const employeeId = uuid();
	const loginInfoId = uuid();
	const employee = {
		id: employeeId,
		firstname: "Abed",
		lastname: "",
		active: true,
		loginInfoId,
		address: "fake Street 23 3000 Helsingør",
		role: "Employee",
		kontoNr: 123123123,
		regNr: 1233,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	};

	prismaMock.employee.update.mockRejectedValue(employee);

	await expect(model.updateEmployee(employee)).rejects.toThrow(new Error("Employee.lastname is empty"));
});

test("Throw if employee.role is empty on update", async () => {
	const employeeId = uuid();
	const loginInfoId = uuid();
	const employee = {
		id: employeeId,
		firstname: "Abed",
		lastname: "Hariri",
		active: true,
		loginInfoId,
		address: "fake Street 23 3000 Helsingør",
		role: "",
		kontoNr: 123123123,
		regNr: 1233,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	};

	prismaMock.employee.update.mockRejectedValue(employee);

	await expect(model.updateEmployee(employee)).rejects.toThrow(new Error("Employee.role is empty"));
});

test("Throw if employee.loginInfoId is invalid on update", async () => {
	const employeeId = uuid();
	const employee = {
		id: employeeId,
		firstname: "Abed",
		lastname: "Hariri",
		active: true,
		loginInfoId: "asdasd",
		address: "fake Street 23 3000 Helsingør",
		role: "Employee",
		kontoNr: 123123123,
		regNr: 1233,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	};

	prismaMock.employee.update.mockRejectedValue(employee);

	await expect(model.updateEmployee(employee)).rejects.toThrow(new Error("Employee.id or employee.loginInfoId is not a valid uuid"));
});

test("Throw if employee.address is empty on update", async () => {
	const employeeId = uuid();
	const loginInfoId = uuid();
	const employee = {
		id: employeeId,
		firstname: "Abed",
		lastname: "Hariri",
		active: true,
		loginInfoId,
		address: "",
		role: "Employee",
		kontoNr: 123123123,
		regNr: 1233,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	};

	prismaMock.employee.update.mockRejectedValue(employee);

	await expect(model.updateEmployee(employee)).rejects.toThrow(new Error("Employee.address is empty"));
});

test("Throw if employee.active is empty on update", async () => {
	const employeeId = uuid();
	const loginInfoId = uuid();
	const employee = {
		id: employeeId,
		firstname: "Abed",
		lastname: "Hariri",
		loginInfoId,
		address: "",
		role: "Employee",
		kontoNr: 123123123,
		regNr: 1233,
	};

	prismaMock.employee.update.mockRejectedValue(employee);

	await expect(model.updateEmployee(employee as any)).rejects.toThrow(new Error("Employee.active is empty"));
});

test("Throw if employee.kontoNr is empty on update", async () => {
	const employeeId = uuid();
	const loginInfoId = uuid();
	const employee = {
		id: employeeId,
		firstname: "Abed",
		lastname: "Hariri",
		active: true,
		loginInfoId,
		address: "Fake street 23, 3000 Helsingør",
		role: "Employee",
		kontoNr: 0,
		regNr: 1233,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	};

	prismaMock.employee.update.mockRejectedValue(employee);

	await expect(model.updateEmployee(employee)).rejects.toThrow(new Error("Employee.kontoNr is zero or less"));
});

test("Throw if employee.regNr is empty on update", async () => {
	const employeeId = uuid();
	const loginInfoId = uuid();
	const employee = {
		id: employeeId,
		firstname: "Abed",
		lastname: "Hariri",
		active: true,
		loginInfoId,
		address: "Fake street 23, 3000 Helsingør",
		kontoNr: 12312313,
		regNr: 0,
		accountId: "acct_1MBLzdCfd0VXBbOf",
		role: "Employee",
	};

	prismaMock.employee.update.mockRejectedValue(employee);

	await expect(model.updateEmployee(employee)).rejects.toThrow(new Error("Employee.regNr is zero or less"));
});

test("Should get employee by id", async () => {
	const employeeId = uuid();
	const loginInfoId = uuid();
	const employee = {
		id: employeeId,
		firstname: "abed",
		lastname: "Hariri",
		active: true,
		loginInfoId,
		address: "fake Street 23 3000 Helsingør",
		role: "Employee",
		kontoNr: 123123123,
		regNr: 1233,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	};

	prismaMock.employee.findUnique.mockResolvedValue(employee);

	await expect(model.getEmployeeById(employee.id)).resolves.toEqual({
		id: employeeId,
		firstname: "abed",
		lastname: "Hariri",
		active: true,
		loginInfoId,
		address: "fake Street 23 3000 Helsingør",
		role: "Employee",
		kontoNr: 123123123,
		regNr: 1233,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	});
});

test("Should fail when getting employee with an invalid id", async () => {
	const loginInfoId = uuid();
	const employee = {
		id: "asdasd",
		firstname: "abed",
		lastname: "Hariri",
		active: true,
		loginInfoId,
		address: "fake Street 23 3000 Helsingør",
		role: "Employee",
		kontoNr: 123123123,
		regNr: 1233,
	};

	prismaMock.employee.findUnique.mockRejectedValue(employee);

	await expect(model.getEmployeeById(employee.id)).rejects.toThrow(new Error("Id is not a valid uuid"));
});

test("should delete employee by id", async () => {
	const employeeId = uuid();
	const loginInfoId = uuid();
	const employee = {
		id: employeeId,
		firstname: "abed",
		lastname: "Hariri",
		active: true,
		loginInfoId,
		address: "fake Street 23 3000 Helsingør",
		role: "Employee",
		kontoNr: 123123123,
		regNr: 1233,
		accountId: "acct_1MBLzdCfd0VXBbOf",
	};

	prismaMock.employee.delete.mockResolvedValue(employee);

	await expect(model.deleteEmployee(employee.id)).resolves.toEqual(null);
});

test("should fail when deleting a employee with an invalid id", async () => {
	const loginInfoId = uuid();
	const employee = {
		id: "asdasd",
		firstname: "abed",
		lastname: "Hariri",
		active: true,
		loginInfoId,
		address: "fake Street 23 3000 Helsingør",
		role: "Employee",
		kontoNr: 123123123,
		regNr: 1233,
	};

	prismaMock.employee.delete.mockRejectedValue(employee);

	await expect(model.deleteEmployee(employee.id)).rejects.toThrow(new Error("Id is not a valid uuid"));
});
