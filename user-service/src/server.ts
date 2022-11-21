import express from "express";

export const app = express();

app.listen(3000, () => {
	console.log(`user-service listening on port ${3000}`);
});
