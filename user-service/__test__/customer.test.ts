import { prismaMock } from "./../singleton";
import { v4 as uuid } from "uuid";
import { Customer } from "@prisma/client";
import { createCustomer } from "../mappers/customer";

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

	await expect(createCustomer(customer)).resolves.toEqual({
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

	expect(createCustomer(customer)).rejects.toEqual(
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

	expect(createCustomer(customer)).rejects.toEqual(
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

	expect(createCustomer(customer)).rejects.toEqual(
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

	expect(createCustomer(customer)).rejects.toEqual(
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

	expect(createCustomer(customer)).rejects.toEqual(
		new Error("Customer.phone is not 8 digits")
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
// 		birtdate: new Date(),
// 		address: "Fakestreet 23 3000 Helsingør",
// 		loginInfoId: loginInfoId,
// 		role: "Customer",
// 	};

// 	expect(createCustomer(customer)).resolves.toEqual(
// 		new Error("Cusotmer.birtday is empty or invalid")
// 	);
// });

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

	expect(createCustomer(customer)).rejects.toEqual(
		new Error("Customer.address is empty")
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

	expect(createCustomer(customer)).rejects.toEqual(
		new Error("Customer.phone is not 8 digits")
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

	expect(createCustomer(customer)).rejects.toEqual(
		new Error("Customer.role is empty")
	);
});

// test("Should update customer", () => {});
// test("Should fail if id is not uuid on update", () => {});
// test("Throw if customer.name is empty on update", () => {});
// test("Throw if customer.role is empty on update", () => {});
// test("Throw if customer.kontoNr is empty on update", () => {});
// test("Throw if customer.regNr is empty on update", () => {});
// test("Should get customer by id", () => {});
// test("Should fail when getting customer with an invalid id", () => {});
// test("should delete customer by id", () => {});
// test("should fail when deleting a customer with an invalid id", () => {});
