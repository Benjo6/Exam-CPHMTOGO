import express from "express";
import companyRoutes from "./routes/company.routes";
import customerRoutes from "./routes/customer.routes";
import bodyParser from "body-parser";

export const app = express();

app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());

app.use("/company", companyRoutes);
app.use("/customer", customerRoutes);

app.listen(3000, () => {
	console.log(`user-service listening on port ${3000}`);
});
