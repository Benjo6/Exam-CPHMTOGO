import express from "express";
import controller from "../controllers/customer.controller";
import { ICustomerReq } from "../interfaces/customer.interface";

const router = express.Router();

router.post("/", async (req: ICustomerReq, res) => {
	try {
		res.json(await controller.createCustomer(req.body));
	} catch (e: any) {
		res.json({ msg: e.message });
	}
});
router.put("/:customerId", async (req: ICustomerReq, res) => {
	try {
		res.json(await controller.updateCustomer(req.body, req.params.customerId));
	} catch (e: any) {
		res.json({ msg: e.message });
	}
});
router.get("/:customerId", async (req: ICustomerReq, res) => {
	try {
		res.json(await controller.getCustomerById(req.params.customerId));
	} catch (e: any) {
		res.json({ msg: e.message });
	}
});
router.delete("/:customerId", async (req: ICustomerReq, res) => {
	try {
		res.json(await controller.deleteCustomer(req.params.customerId));
	} catch (e: any) {
		res.json({ msg: e.message });
	}
});

export default router;
