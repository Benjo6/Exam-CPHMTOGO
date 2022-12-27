import express from "express";
import companyRoutes from "./routes/company.routes";
import customerRoutes from "./routes/customer.routes";
import employeeRoutes from "./routes/employee.routes";
import bodyParser from "body-parser";
import prisma from "../prisma/client";

export const app = express();

app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());

app.use("/company", companyRoutes);
app.use("/customer", customerRoutes);
app.use("/employee", employeeRoutes);

app.get("/", (req, res) => {
	res.send("server is running");
});

app.get("/customerUser", async (req, res) => {
	res.json(
		await prisma.customer.findUnique({
			where: {
				id: "39bfb82a-b55f-457c-9eaa-ac77e98367f3",
			},
		})
	);
});

app.listen(process.env.PORT, () => {
	console.log(`user-service listening on port ${process.env.PORT}`);
});
