import { prismaMock } from "../../src/singleton";
import { v4 as uuid } from "uuid";
import { Customer, prisma } from "@prisma/client";
import model from "../../src/models/customer.model";

test("Should create new customer", async () => {
	const customerId = uuid();
	const loginInfoId = uuid();
	const customer: Customer = {
		id: customerId,
		firstname: "Abed",
		lastname: "Hariri",
		phone: 13215487,
		birtdate: new Date(2003, 12, 11),
		address: "Fakestreet 23 3000 Helsingør",
		loginInfoId: loginInfoId,
		role: "Customer",
	};

	prismaMock.customer.create.mockResolvedValue(customer);

	await expect(model.createCustomer(customer)).resolves.toEqual({
		id: customerId,
		firstname: "Abed",
		lastname: "Hariri",
		phone: 13215487,
		birtdate: new Date(2003, 12, 11),
		address: "Fakestreet 23 3000 Helsingør",
		loginInfoId: loginInfoId,
		role: "Customer",
	});
});

test("Should fail if id is not a valid uuid on create", () => {
	const loginInfoId = uuid();
	const customer: Customer = {
		id: "pizza",
		firstname: "Abed",
		lastname: "Hariri",
		phone: 13215487,
		birtdate: new Date(2003, 12, 11),
		address: "Fakestreet 23 3000 Helsingør",
		loginInfoId: loginInfoId,
		role: "Customer",
	};

	prismaMock.customer.create.mockRejectedValue(customer);

	expect(model.createCustomer(customer)).rejects.toEqual(
		new Error("Customer.id or customer.loginInfoId is not a valid uuid")
	);
});

test("Throw if customer.fistname is empty on create", () => {
	const customerId = uuid();
	const loginInfoId = uuid();
	const customer: Customer = {
		id: customerId,
		firstname: "",
		lastname: "Hariri",
		phone: 13215487,
		birtdate: new Date(2003, 12, 11),
		address: "Fakestreet 23 3000 Helsingør",
		loginInfoId: loginInfoId,
		role: "Customer",
	};

	prismaMock.customer.create.mockRejectedValue(customer);

	expect(model.createCustomer(customer)).rejects.toEqual(
		new Error("Customer.firstname is empty")
	);
});

test("Throw if customer.lastname is empty on create", () => {
	const customerId = uuid();
	const loginInfoId = uuid();
	const customer: Customer = {
		id: customerId,
		firstname: "Abed",
		lastname: "",
		phone: 13215487,
		birtdate: new Date(2003, 12, 11),
		address: "Fakestreet 23 3000 Helsingør",
		loginInfoId: loginInfoId,
		role: "Customer",
	};

	prismaMock.customer.create.mockRejectedValue(customer);

	expect(model.createCustomer(customer)).rejects.toEqual(
		new Error("Customer.lastname is empty")
	);
});

test("Throw if customer.phone is less than 8 digit on create", () => {
	const customerId = uuid();
	const loginInfoId = uuid();
	const customer: Customer = {
		id: customerId,
		firstname: "Abed",
		lastname: "Hariri",
		phone: 123457,
		birtdate: new Date(2003, 12, 11),
		address: "Fakestreet 23 3000 Helsingør",
		loginInfoId: loginInfoId,
		role: "Customer",
	};

	prismaMock.customer.create.mockRejectedValue(customer);

	expect(model.createCustomer(customer)).rejects.toEqual(
		new Error("Customer.phone is not 8 digits")
	);
});

test("Throw if customer.phone is more than 8 digit on create", () => {
	const customerId = uuid();
	const loginInfoId = uuid();
	const customer: Customer = {
		id: customerId,
		firstname: "Abed",
		lastname: "Hariri",
		phone: 123456789,
		birtdate: new Date(2003, 12, 11),
		address: "Fakestreet 23 3000 Helsingør",
		loginInfoId: loginInfoId,
		role: "Customer",
	};

	prismaMock.customer.create.mockRejectedValue(customer);

	expect(model.createCustomer(customer)).rejects.toEqual(
		new Error("Customer.phone is not 8 digits")
	);
});

test("Throw if customer.address is empty on create", () => {
	const customerId = uuid();
	const loginInfoId = uuid();
	const customer: Customer = {
		id: customerId,
		firstname: "Abed",
		lastname: "Hariri",
		phone: 12345678,
		birtdate: new Date(2013, 12, 11),
		address: "",
		loginInfoId: loginInfoId,
		role: "Customer",
	};

	prismaMock.customer.create.mockRejectedValue(customer);

	expect(model.createCustomer(customer)).rejects.toEqual(
		new Error("Customer.address is empty")
	);
});

test("Throw if customer.role is empty on create", () => {
	const customerId = uuid();
	const loginInfoId = uuid();
	const customer: Customer = {
		id: customerId,
		firstname: "Abed",
		lastname: "Hariri",
		phone: 13215487,
		birtdate: new Date(2003, 12, 11),
		address: "Fakestreet 23 3000 Helsingør",
		loginInfoId: loginInfoId,
		role: "",
	};

	prismaMock.customer.create.mockRejectedValue(customer);

	expect(model.createCustomer(customer)).rejects.toEqual(
		new Error("Customer.role is empty")
	);
});

// test("Throw if customer.birthdate is empty on create", () => {
// 	const customerId = uuid();
// 	const loginInfoId = uuid();
// 	const customer: Customer = {
// 		id: customerId,
// 		firstname: "Abed",
// 		lastname: "Hariri",
// 		phone: 12345678,
// 		birtdate: new Date(""),
// 		address: "Fakestreet 23 3000 Helsingør",
// 		loginInfoId: loginInfoId,
// 		role: "Customer",
// 	};

// 	expect(model.createCustomer(customer)).resolves.toEqual(
// 		new Error("Cusotmer.birtday is empty or invalid")
// 	);
// });

test("Should update customer", async () => {
	const customerId = uuid();
	const loginInfoId = uuid();
	const customer: Customer = {
		id: customerId,
		firstname: "Abed",
		lastname: "Hariri",
		phone: 12345678,
		birtdate: new Date(2003, 12, 11),
		address: "Fakestreet 23 3000 Helsingør",
		loginInfoId: loginInfoId,
		role: "Customer",
	};

	prismaMock.customer.update.mockResolvedValue(customer);

	await expect(model.updateCustomer(customer)).resolves.toEqual({
		id: customerId,
		firstname: "Abed",
		lastname: "Hariri",
		phone: 12345678,
		birtdate: new Date(2003, 12, 11),
		address: "Fakestreet 23 3000 Helsingør",
		loginInfoId: loginInfoId,
		role: "Customer",
	});
});

test("Should fail if id is not uuid on update", async () => {
	const loginInfoId = uuid();
	const customer: Customer = {
		id: "asdasdad",
		firstname: "Abed",
		lastname: "Hariri",
		phone: 12345678,
		birtdate: new Date(2003, 12, 11),
		address: "Fakestreet 23 3000 Helsingør",
		loginInfoId: loginInfoId,
		role: "Customer",
	};

	prismaMock.customer.update.mockRejectedValue(customer);

	await expect(model.updateCustomer(customer)).rejects.toEqual(
		new Error("Customer.id or customer.loginInfoId is not a valid uuid")
	);
});

test("Throw if customer.fistname is empty on update", () => {
	const customerId = uuid();
	const loginInfoId = uuid();
	const customer: Customer = {
		id: customerId,
		firstname: "",
		lastname: "Hariri",
		phone: 13215487,
		birtdate: new Date(2003, 12, 11),
		address: "Fakestreet 23 3000 Helsingør",
		loginInfoId: loginInfoId,
		role: "Customer",
	};

	prismaMock.customer.update.mockRejectedValue(customer);

	expect(model.updateCustomer(customer)).rejects.toEqual(
		new Error("Customer.firstname is empty")
	);
});

test("Throw if customer.lastname is empty on update", () => {
	const customerId = uuid();
	const loginInfoId = uuid();
	const customer: Customer = {
		id: customerId,
		firstname: "Abed",
		lastname: "",
		phone: 13215487,
		birtdate: new Date(2003, 12, 11),
		address: "Fakestreet 23 3000 Helsingør",
		loginInfoId: loginInfoId,
		role: "Customer",
	};

	prismaMock.customer.update.mockRejectedValue(customer);

	expect(model.updateCustomer(customer)).rejects.toEqual(
		new Error("Customer.lastname is empty")
	);
});

test("Throw if customer.phone is less than 8 digit on update", () => {
	const customerId = uuid();
	const loginInfoId = uuid();
	const customer: Customer = {
		id: customerId,
		firstname: "Abed",
		lastname: "Hariri",
		phone: 123457,
		birtdate: new Date(2003, 12, 11),
		address: "Fakestreet 23 3000 Helsingør",
		loginInfoId: loginInfoId,
		role: "Customer",
	};

	prismaMock.customer.update.mockRejectedValue(customer);

	expect(model.updateCustomer(customer)).rejects.toEqual(
		new Error("Customer.phone is not 8 digits")
	);
});

test("Throw if customer.phone is more than 8 digit on update", () => {
	const customerId = uuid();
	const loginInfoId = uuid();
	const customer: Customer = {
		id: customerId,
		firstname: "Abed",
		lastname: "Hariri",
		phone: 123456789,
		birtdate: new Date(2003, 12, 11),
		address: "Fakestreet 23 3000 Helsingør",
		loginInfoId: loginInfoId,
		role: "Customer",
	};

	prismaMock.customer.update.mockRejectedValue(customer);

	expect(model.updateCustomer(customer)).rejects.toEqual(
		new Error("Customer.phone is not 8 digits")
	);
});

test("Throw if customer.address is empty on update", () => {
	const customerId = uuid();
	const loginInfoId = uuid();
	const customer: Customer = {
		id: customerId,
		firstname: "Abed",
		lastname: "Hariri",
		phone: 12345678,
		birtdate: new Date(2013, 12, 11),
		address: "",
		loginInfoId: loginInfoId,
		role: "Customer",
	};

	prismaMock.customer.update.mockRejectedValue(customer);

	expect(model.updateCustomer(customer)).rejects.toEqual(
		new Error("Customer.address is empty")
	);
});

test("Throw if customer.role is empty on update", () => {
	const customerId = uuid();
	const loginInfoId = uuid();
	const customer: Customer = {
		id: customerId,
		firstname: "Abed",
		lastname: "Hariri",
		phone: 13215487,
		birtdate: new Date(2003, 12, 11),
		address: "Fakestreet 23 3000 Helsingør",
		loginInfoId: loginInfoId,
		role: "",
	};

	prismaMock.customer.update.mockRejectedValue(customer);

	expect(model.updateCustomer(customer)).rejects.toEqual(
		new Error("Customer.role is empty")
	);
});

// test("Should get customer by id", () => {});
// test("Should fail when getting customer with an invalid id", () => {});
// test("should delete customer by id", () => {});
// test("should fail when deleting a customer with an invalid id", () => {});

test("Should get customer by id", async () => {
	const customerId = uuid();
	const loginInfoId = uuid();
	const customer: Customer = {
		id: customerId,
		firstname: "Abed",
		lastname: "Hariri",
		phone: 13215487,
		birtdate: new Date(2003, 12, 11),
		address: "Fakestreet 23 3000 Helsingør",
		loginInfoId: loginInfoId,
		role: "",
	};

	prismaMock.customer.findUnique.mockResolvedValue(customer);

	await expect(model.getCustomerById(customer.id)).resolves.toEqual({
		id: customerId,
		firstname: "Abed",
		lastname: "Hariri",
		phone: 13215487,
		birtdate: new Date(2003, 12, 11),
		address: "Fakestreet 23 3000 Helsingør",
		loginInfoId: loginInfoId,
		role: "",
	});
});

test("Should fail when getting customer with an invalid id", async () => {
	const loginInfoId = uuid();
	const customer: Customer = {
		id: "asdasd",
		firstname: "Abed",
		lastname: "Hariri",
		phone: 13215487,
		birtdate: new Date(2003, 12, 11),
		address: "Fakestreet 23 3000 Helsingør",
		loginInfoId: loginInfoId,
		role: "",
	};

	prismaMock.customer.findUnique.mockRejectedValue(customer);

	await expect(model.getCustomerById(customer.id)).rejects.toEqual(
		new Error("Id is not a valid uuid")
	);
});

test("should delete customer by id", async () => {
	const customerId = uuid();
	const loginInfoId = uuid();
	const customer: Customer = {
		id: customerId,
		firstname: "Abed",
		lastname: "Hariri",
		phone: 13215487,
		birtdate: new Date(2003, 12, 11),
		address: "Fakestreet 23 3000 Helsingør",
		loginInfoId: loginInfoId,
		role: "",
	};

	prismaMock.customer.findUnique.mockResolvedValue(customer);

	await expect(model.deletCustomer(customer.id)).resolves.toEqual(null);
});

test("should fail when deleting a customer with an invalid id", async () => {
	const loginInfoId = uuid();
	const customer: Customer = {
		id: "asdsad",
		firstname: "Abed",
		lastname: "Hariri",
		phone: 13215487,
		birtdate: new Date(2003, 12, 11),
		address: "Fakestreet 23 3000 Helsingør",
		loginInfoId: loginInfoId,
		role: "",
	};

	prismaMock.customer.findUnique.mockResolvedValue(customer);

	await expect(model.deletCustomer(customer.id)).rejects.toEqual(
		new Error("Id is not a valid uuid")
	);
});
