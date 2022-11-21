import { Company } from "@prisma/client";
import {
	createCompany,
	deleteCompany,
	getCompanyById,
	updateCompany,
} from "../mappers/company";
import { prismaMock } from "./../singleton";
import { v4 as uuid } from "uuid";

test("Should create new company", async () => {
	const companyId = uuid();
	const loginInfoId = uuid();
	const company: Company = {
		id: companyId,
		name: "just-eat",
		role: "Admin",
		loginInfoId: loginInfoId,
		kontoNr: 123123123,
		regNr: 12312,
	};

	prismaMock.company.create.mockResolvedValue(company);

	await expect(createCompany(company)).resolves.toEqual({
		id: companyId,
		name: "just-eat",
		role: "Admin",
		loginInfoId: loginInfoId,
		kontoNr: 123123123,
		regNr: 12312,
	});
});

test("Should fail if id is not uuid on create", async () => {
	const company: Company = {
		id: "sushi",
		name: "just-eat",
		role: "",
		loginInfoId: "indian food",
		kontoNr: 123123123,
		regNr: 12312,
	};
	await expect(createCompany(company)).resolves.toEqual(
		new Error("Company.id or Company.loginInfoId is not a valid uuid")
	);
});

test("Throw if company.name is empty on create", async () => {
	const companyId = uuid();
	const loginInfoId = uuid();
	const company: Company = {
		id: companyId,
		name: "",
		role: "asdasd",
		loginInfoId: loginInfoId,
		kontoNr: 123123123,
		regNr: 12312,
	};

	expect(createCompany(company)).resolves.toEqual(
		new Error("Company.name is empty")
	);
});

test("Throw if company.role is empty on create", async () => {
	const companyId = uuid();
	const loginInfoId = uuid();
	const company: Company = {
		id: companyId,
		name: "asdasd",
		role: "",
		loginInfoId: loginInfoId,
		kontoNr: 123123123,
		regNr: 12312,
	};

	expect(createCompany(company)).resolves.toEqual(
		new Error("Company.role is empty")
	);
});

test("Throw if company.kontoNr is empty on create", async () => {
	const companyId = uuid();
	const loginInfoId = uuid();
	const company: Company = {
		id: companyId,
		name: "asdasd",
		role: "Role",
		loginInfoId: loginInfoId,
		kontoNr: 0,
		regNr: 12312,
	};

	expect(createCompany(company)).resolves.toEqual(
		new Error("Company.kontoNr is zero or less")
	);
});

test("Throw if company.regNr is empty on create", async () => {
	const companyId = uuid();
	const loginInfoId = uuid();
	const company: Company = {
		id: companyId,
		name: "asdasd",
		role: "asdasd",
		loginInfoId: loginInfoId,
		kontoNr: 123123123,
		regNr: 0,
	};

	expect(createCompany(company)).resolves.toEqual(
		new Error("Company.regNr is zero or less")
	);
});

test("Should update company", async () => {
	const companyId = uuid();
	const loginInfoId = uuid();
	const company: Company = {
		id: companyId,
		name: "just-eat",
		role: "Admin",
		loginInfoId: loginInfoId,
		kontoNr: 123123123,
		regNr: 12312,
	};

	prismaMock.company.create.mockResolvedValue(company);

	await expect(createCompany(company)).resolves.toEqual({
		id: companyId,
		name: "just-eat",
		role: "Admin",
		loginInfoId: loginInfoId,
		kontoNr: 123123123,
		regNr: 12312,
	});
});

test("Should fail if id is not uuid on update", async () => {
	const company: Company = {
		id: "sushi",
		name: "just-eat",
		role: "",
		loginInfoId: "indian food",
		kontoNr: 123123123,
		regNr: 12312,
	};
	await expect(updateCompany(company)).resolves.toEqual(
		new Error("Company.id or Company.loginInfoId is not a valid uuid")
	);
});

test("Throw if company.name is empty on update", async () => {
	const companyId = uuid();
	const loginInfoId = uuid();
	const company: Company = {
		id: companyId,
		name: "",
		role: "asdasd",
		loginInfoId: loginInfoId,
		kontoNr: 123123123,
		regNr: 12312,
	};

	expect(updateCompany(company)).resolves.toEqual(
		new Error("Company.name is empty")
	);
});

test("Throw if company.role is empty on update", async () => {
	const companyId = uuid();
	const loginInfoId = uuid();
	const company: Company = {
		id: companyId,
		name: "asdasd",
		role: "",
		loginInfoId: loginInfoId,
		kontoNr: 123123123,
		regNr: 12312,
	};

	expect(updateCompany(company)).resolves.toEqual(
		new Error("Company.role is empty")
	);
});

test("Throw if company.kontoNr is empty on update", async () => {
	const companyId = uuid();
	const loginInfoId = uuid();
	const company: Company = {
		id: companyId,
		name: "asdasd",
		role: "Role",
		loginInfoId: loginInfoId,
		kontoNr: 0,
		regNr: 12312,
	};

	expect(updateCompany(company)).resolves.toEqual(
		new Error("Company.kontoNr is zero or less")
	);
});

test("Throw if company.regNr is empty on update", async () => {
	const companyId = uuid();
	const loginInfoId = uuid();
	const company: Company = {
		id: companyId,
		name: "asdasd",
		role: "asdasd",
		loginInfoId: loginInfoId,
		kontoNr: 123123123,
		regNr: 0,
	};

	expect(updateCompany(company)).resolves.toEqual(
		new Error("Company.regNr is zero or less")
	);
});

test("Should get company by id", async () => {
	const companyId = uuid();
	const loginInfoId = uuid();
	const company: Company = {
		id: companyId,
		name: "just-eat",
		role: "Admin",
		loginInfoId: loginInfoId,
		kontoNr: 123123123,
		regNr: 12312,
	};

	prismaMock.company.findUnique.mockResolvedValue(company);

	await expect(getCompanyById(company.id)).resolves.toEqual({
		id: companyId,
		name: "just-eat",
		role: "Admin",
		loginInfoId: loginInfoId,
		kontoNr: 123123123,
		regNr: 12312,
	});
});

test("Should fail when getting company with an invalid id", async () => {
	const loginInfoId = uuid();
	const company: Company = {
		id: "asdasd",
		name: "just-eat",
		role: "Admin",
		loginInfoId: loginInfoId,
		kontoNr: 123123123,
		regNr: 12312,
	};

	await expect(getCompanyById(company.id)).resolves.toEqual(
		new Error("Id is not a valid uuid")
	);
});

test("should delete company by id", async () => {
	const companyId = uuid();
	const loginInfoId = uuid();
	const company: Company = {
		id: companyId,
		name: "just-eat",
		role: "Admin",
		loginInfoId: loginInfoId,
		kontoNr: 123123123,
		regNr: 12312,
	};

	prismaMock.company.findUnique.mockResolvedValue(company);

	await expect(deleteCompany(company.id)).resolves.toEqual(null);
});

test("should fail when deleting a company with an invalid id", async () => {
	const loginInfoId = uuid();
	const company: Company = {
		id: "adsdasd",
		name: "just-eat",
		role: "Admin",
		loginInfoId: loginInfoId,
		kontoNr: 123123123,
		regNr: 12312,
	};

	await expect(deleteCompany(company.id)).resolves.toEqual(
		new Error("Id is not a valid uuid")
	);
});
