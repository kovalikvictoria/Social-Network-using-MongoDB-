using Neo4jClient;
using System;

namespace Neo4J_DAL
{
    public class ConfigurationManagerNeo4J
    {
        private ConfigurationManagerNeo4J()
        {
        }

        public static void GetDefaultDatabase()
        {
            var connectionString = GetDefaultConnectionString();
            var database = GetDefaultDatabaseName();
            var client = new GraphClient(new Uri(connectionString), "neo4j", database);
            client.Connect();
        }

        private static string GetDefaultConnectionString()
        {
            return "http://localhost:7474/db/data/";
        }

        private static string GetDefaultDatabaseName()
        {
            return "social_network_users";
        }
    }
}
