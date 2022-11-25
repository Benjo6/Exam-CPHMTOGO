import express from "express";
import { ICompanyReq } from "../interfaces/company.interface";
import controller from "../controllers/company.controller";

const router = express.Router();

router.get("/:companyId", async (req: ICompanyReq, res) => {
	try {
		res.json(await controller.getCompanyById(req.params.companyId));
	} catch (e: any) {
		res.json({ error: e.message });
	}
});

router.post("/", async (req: ICompanyReq, res) => {
	try {
		res.json(await controller.createCompany(req.body));
	} catch (e: any) {
		res.json({ error: e.message });
	}
});

router.put("/:companyId", async (req: ICompanyReq, res) => {
	try {
		res.json(await controller.updateCompany(req.body, req.params.companyId));
	} catch (e: any) {
		res.json({ error: e.message });
	}
});

router.delete("/:companyId", async (req: ICompanyReq, res) => {
	try {
		res.json(await controller.deleteCompany(req.params.companyId));
	} catch (e: any) {
		res.json({ error: e.message });
	}
});

export default router;
