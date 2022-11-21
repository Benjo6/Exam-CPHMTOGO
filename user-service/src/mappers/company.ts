import { Company } from "@prisma/client";
import prisma from "../../prisma/client";
import isValidUuid from "../utils/checkUuid";

export async function createCompany(company: Company) {
	try {
		if (isValidCompany(company))
			return await prisma.company.create({
				data: company,
			});
	} catch (e: any) {
		throw e;
	}
}

export async function updateCompany(company: Company) {
	try {
		if (isValidCompany(company))
			return await prisma.company.update({
				where: {
					id: company.id,
				},
				data: company,
			});
	} catch (e: unknown) {
		throw e;
	}
}

export async function getCompanyById(id: string) {
	try {
		if (!isValidUuid(id)) throw new Error("Id is not a valid uuid");
		return await prisma.company.findUnique({
			where: {
				id,
			},
		});
	} catch (e: unknown) {
		throw e;
	}
}

export async function deleteCompany(id: string) {
	try {
		if (!isValidUuid(id)) throw new Error("Id is not a valid uuid");
		await prisma.company.delete({
			where: {
				id,
			},
		});
		return null;
	} catch (e: unknown) {
		throw e;
	}
}

function isValidCompany(company: Company) {
	if (!isValidUuid(company.id) || !isValidUuid(company.loginInfoId))
		throw new Error("Company.id or Company.loginInfoId is not a valid uuid");
	if (company.name.length === 0) throw new Error("Company.name is empty");
	if (company.role.length === 0) throw new Error("Company.role is empty");
	if (company.kontoNr <= 0) throw new Error("Company.kontoNr is zero or less");
	if (company.regNr <= 0) throw new Error("Company.regNr is zero or less");
	return true;
}
