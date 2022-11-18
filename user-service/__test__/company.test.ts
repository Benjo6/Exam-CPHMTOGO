import { Company } from "@prisma/client";
import { createCompany } from "../mappers/company";
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

test("Should fail if id is not uuid", async () => {
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

test("Name should not be empty", async () => {
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

test("Role should not be empty", async () => {
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
test("KontoNr should not be empty", async () => {
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
test("RegNr should not be empty", async () => {
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
