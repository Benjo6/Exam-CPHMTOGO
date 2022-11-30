import { ApolloServer } from "@apollo/server";
import { startStandaloneServer } from "@apollo/server/standalone";
import resolvers from "./resolvers/resolver";
import typeDefs from "./typeDefs/typeDefs";

async function startServer() {
	const server = new ApolloServer({
		typeDefs,
		resolvers,
	});
	const { url } = await startStandaloneServer(server, {
		listen: { port: 4000 },
	});

	console.log(`🚀  Server ready at: ${url}`);
}
startServer();
