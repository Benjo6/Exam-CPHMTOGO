import express from "express";
import router from "./routes/company.routes";
import bodyParser from "body-parser";

export const app = express();

app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());

app.use("/company", router);

app.listen(3000, () => {
	console.log(`user-service listening on port ${3000}`);
});
