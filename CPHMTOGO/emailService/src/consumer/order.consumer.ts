import amqp, { ConsumeMessage } from "amqplib";
import sendMail from "../controller/order.controller";

async function connectQueue() {
	const connection = await amqp.connect("amqp://localhost");
	const channel = await connection.createChannel();

	await channel.assertQueue("orders");

	await channel.consume(
		"orders",
		(msg: ConsumeMessage | null) => {
			if (msg !== null) {
				console.log(JSON.parse(msg.content.toString()));
				sendMail(JSON.parse(msg.content.toString()));
			} else {
				console.log(`msg was null`);
			}
		},
		{
			noAck: true,
		}
	);
}

connectQueue();
