using System.Collections.Generic;
using Entity;
using Neo4jClient;

namespace Neo4J
{
    public class PersonDAL
    {
        IGraphClient client;

        public PersonDAL()
        {
            client = ConfigurationManagerNeo4J.GetDefaultClient();
        }

        public void CreatePerson(User p)
        {
            client.Cypher
                .Create("(p:Person {name: {pname}})")
                .WithParam("pname", p.Login)
                .ExecuteWithoutResults();
        }

        public void CreateRelations(string p1, string p2)
        {
            client.Cypher
                .Match("(p1:Person {name: {pn1}})", "(p2:Person {name: {pn2}})")
                .WithParam("pn1", p1)
                .WithParam("pn2", p2)
                .Create("(p1)-[:FOLLOWING]->(p2)")
                .Create("(p1)<-[:FOLLOWER]-(p2)")
                .ExecuteWithoutResults();
        }

        public IEnumerable<User> GetFollowingRelations(string user)
        {
            return client.Cypher
                .Match("(p1:Person {name: {pn}})-[:FOLLOWING]->(p2:Person)")
                .WithParam("pn", user)
                .Return<User>("p2").Results;
        }

        public IEnumerable<User> GetFollowerRelations(string user)
        {
            return client.Cypher
                .Match("(p1:Person {name: {pn}})-[:FOLLOWER]->(p2:Person)")
                .WithParam("pn", user)
                .Return<User>("p2").Results;
        }

        public void UpdatePerson(string name, string new_name)
        {
            client.Cypher
                .Match("(p:Person {name: {pname}})")
                .WithParam("pname", name)
                .Set("p.name = {new_name}")
                .WithParam("new_name", new_name)
                .ExecuteWithoutResults();
        }

        public void DeleteRelations(string p1, string p2)
        {
            client.Cypher
                .Match("(p1:Person {name: {pn1}})-[f1:FOLLOWING]->(p2:Person {name: {pn2}})",
                       "(p1:Person {name: {pn1}})<-[f2:FOLLOWER]-(p2:Person {name: {pn2}})")
                .WithParam("pn1", p1)
                .WithParam("pn2", p2)
                .Delete("f1")
                .Delete("f2")
                .ExecuteWithoutResults();
        }

        public void DeletePerson(User p)
        {
            client.Cypher
                .Match("(p:Person {name: {pname}})")
                .WithParam("pname", p.Login)
                .Delete("p")
                .ExecuteWithoutResults();
        }
    }
}
