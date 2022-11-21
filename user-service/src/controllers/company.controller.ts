import { ICompany } from "../interfaces/company.interface";
import { v4 as uuid } from "uuid";
import mapper from "../models/company.model";

function createCompany(company: ICompany) {
	return mapper.createCompany({
		id: uuid(),
		name: company.name,
		loginInfoId: company.loginInfoId,
		role: company.role,
		kontoNr: company.kontoNr,
		regNr: company.regNr,
	});
}

function updateCompany(company: ICompany, id: string) {
	return mapper.updateCompany({
		id,
		name: company.name,
		loginInfoId: company.loginInfoId,
		role: company.role,
		kontoNr: company.kontoNr,
		regNr: company.regNr,
	});
}

function getCompanyById(id: string) {
	return mapper.getCompanyById(id);
}

function deleteCompany(id: string) {
	return mapper.deleteCompany(id);
}

export default {
	createCompany,
	getCompanyById,
	updateCompany,
	deleteCompany,
};
