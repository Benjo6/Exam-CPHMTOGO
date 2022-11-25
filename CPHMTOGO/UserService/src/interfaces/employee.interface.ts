export interface IEmployeeReq {
	body: IEmployee;
	params: {
		employeeId: string;
	};
}

export interface IEmployee {
	firstname: string;
	lastname: string;
	active: boolean;
	loginInfoId: string;
	address: string;
	role: string;
	kontoNr: number;
	regNr: number;
}
