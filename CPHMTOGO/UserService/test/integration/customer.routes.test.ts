import { app } from "../../src/server";
import request from "supertest";
import { v4 as uuid } from "uuid";

describe("Server should be", () => {
	test("should say server is running", async () => {
		const res = await request(app).get("/");
		expect(res.text).toEqual("server is running");
	});
});

describe("Customer Post Endpoint", () => {
	test("should create a Customer", async () => {
		const res = await request(app).post("/customer").send({
			firstname: "Abed",
			lastname: "Hariri",
			phone: 12345678,
			birtdate: "20-08-2021",
			address: "fake-address",
			loginInfoId: "01098a2b-e001-48be-8224-2b7938b943a2",
			role: "Customer",
		});

		expect(res.statusCode).toEqual(200);
		expect(res.body).toEqual({
			firstname: "Abed",
			lastname: "Hariri",
			phone: 12345678,
			birtdate: "20-08-2021",
			address: "fake-address",
			loginInfoId: "01098a2b-e001-48be-8224-2b7938b943a2",
			role: "Customer",
		});
	});
});

test("should return customer", async () => {
	const res = await request(app).get("/customerUser");
	expect(res.body).toEqual({
		id: "39bfb82a-b55f-457c-9eaa-ac77e98367f3",
		firstname: "Abed",
		lastname: "Hariri",
		phone: 12345678,
		birtdate: "2012-10-03T22:00:00.000Z",
		address: "Fakestreet 23 3000 Helsingør",
		loginInfoId: "01098a2b-e001-48be-8224-2b7938b943a2",
		role: "Customer",
	});
});
