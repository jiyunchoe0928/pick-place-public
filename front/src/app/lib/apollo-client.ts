// lib/apollo-client.ts  
import { ApolloClient, InMemoryCache } from '@apollo/client';  

const SERVER_URL = "http://localhost:3333"

const client = new ApolloClient({  
  uri: `${SERVER_URL}/graphql/`,
  cache: new InMemoryCache(),  
});  
  
export default client;  