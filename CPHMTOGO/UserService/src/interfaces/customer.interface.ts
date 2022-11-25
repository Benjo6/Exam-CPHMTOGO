export interface ICustomer {
	firstname: string;
	lastname: string;
	phone: number;
	birtdate: string;
	address: string;
	loginInfoId: string;
	role: string;
}

export interface ICustomerReq {
	body: ICustomer;
	params: {
		customerId: string;
	};
}
