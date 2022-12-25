import { Employee } from "@prisma/client";

export interface IEmployeeReq {
	body: Employee;
	params: {
		employeeId: string;
	};
}
