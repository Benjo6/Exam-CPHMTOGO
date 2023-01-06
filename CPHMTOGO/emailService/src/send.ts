import amqp from "amqplib";

async function connectQueue() {
	const connection = await amqp.connect("amqp://localhost");
	const channel = await connection.createChannel();

	await channel.assertQueue("orders");

	const order = {
		orderFrom: {
			restaurantName: "Organic Healthy Products",
			address: {
				street: "8161 Misty Circuit",
				ciry: "Stop, Idaho",
			},
		},
		orderTo: {
			customerName: "Nancy Watkins",
			address: {
				street: "5406 Merry Creek Crossing",
				city: "Mexico, Florida",
			},
		},
		orderItems: [
			{
				amount: 1,
				unit: "kg",
				name: "Raspberries",
				price: 34.23,
				currency: "USD",
			},
			{
				amount: 1,
				unit: "kg",
				name: "Grapes",
				price: 34.23,
				currency: "USD",
			},
			{
				amount: 1,
				unit: "kg",
				name: "Carrot",
				price: 25.23,
				currency: "USD",
			},
		],
	};

	channel.sendToQueue("orders", Buffer.from(JSON.stringify(order)));

	await channel.close();
	await connection.close();
}

connectQueue();
