import express from "express";
import { IEmployeeReq } from "../interfaces/employee.interface";
import controller from "../controllers/employee.controller";

const router = express.Router();

router.post("/", async (req: IEmployeeReq, res) => {
	try {
		res.json(await controller.createEmployee(req.body));
	} catch (e: any) {
		res.json({ msg: e.message });
	}
});
router.get("/:employeeId", async (req: IEmployeeReq, res) => {
	try {
		res.json(await controller.getEmployeeById(req.params.employeeId));
	} catch (e: any) {
		res.json({ msg: e.message });
	}
});
router.put("/:employeeId", async (req: IEmployeeReq, res) => {
	try {
		res.json(await controller.updateEmployee(req.body, req.params.employeeId));
	} catch (e: any) {
		res.json({ msg: e.message });
	}
});
router.delete("/:employeeId", async (req: IEmployeeReq, res) => {
	try {
		res.json(await controller.deleteEmployee(req.params.employeeId));
	} catch (e: any) {
		res.json({ msg: e.message });
	}
});

export default router;
