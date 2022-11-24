export interface ICompanyReq {
	body: ICompany;
	params: {
		companyId: string;
	};
}

export interface ICompany {
	name: string;
	role: string;
	loginInfoId: string;
	kontoNr: number;
	regNr: number;
}
