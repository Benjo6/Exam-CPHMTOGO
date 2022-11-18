import { Company } from "@prisma/client";
import prisma from "../prisma/client";

export async function createCompany(company: Company) {
  try {
    if (isValidCompany(company))
      return await prisma.company.create({
        data: company,
      });
  } catch (e: any) {
    return e;
  }
}

function isValidCompany(company: Company) {
  if (!isUuid(company.id) || !isUuid(company.loginInfoId))
    throw new Error("Company.id or Company.loginInfoId is not a valid uuid");
  if (company.name.length === 0) throw new Error("Company.name is empty");
  if (company.role.length === 0) throw new Error("Company.role is empty");
  if (company.kontoNr <= 0) throw new Error("Company.kontoNr is zero or less");
  if (company.regNr <= 0) throw new Error("Company.regNr is zero or less");
  return true;
}

function isUuid(uuid: string) {
  const regexExp =
    /^[0-9a-fA-F]{8}\b-[0-9a-fA-F]{4}\b-[0-9a-fA-F]{4}\b-[0-9a-fA-F]{4}\b-[0-9a-fA-F]{12}$/gi;
  return regexExp.test(uuid);
}
