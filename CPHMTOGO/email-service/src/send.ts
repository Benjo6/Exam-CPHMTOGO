import amqp from "amqplib";

async function connectQueue() {
	const connection = await amqp.connect("amqp://localhost");
	const channel = await connection.createChannel();

	await channel.assertQueue("orders");

	const order = {
		id: "123",
		customer: {
			name: "John",
		},
		address: {
			street: "123 Fake St.",
			city: "San Francisco",
		},
		restaurant: {
			name: "Roxanne",
		},
		orderItems: [
			{
				name: "apple",
			},
			{
				name: "apple",
			},
		],
		total: 1000,
		date: new Date().toISOString(),
	};

	await channel.sendToQueue("orders", Buffer.from(JSON.stringify(order)));

	await channel.close();
	await connection.close();
}

connectQueue();
