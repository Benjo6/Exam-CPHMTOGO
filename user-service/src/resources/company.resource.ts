import { app } from "../server";
import { createCompany } from "../mappers/company";
import { v4 as uuid } from "uuid";
import { Company } from "@prisma/client";

app.get("/create-company", async (req, res) => {
	try {
		const companyId = uuid();
		const loginInfoId = uuid();
		const company: Company = {
			id: companyId,
			name: "Abed",
			role: "Hariri",
			loginInfoId: loginInfoId,
			kontoNr: 123123123,
			regNr: 12312,
		};

		const newCompany = await createCompany(company);
		console.log(newCompany);
		res.json(newCompany);
	} catch (e: any) {
		res.json({ error: e.message });
	}
});
