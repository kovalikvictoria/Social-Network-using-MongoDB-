using Entity;
using Neo4J;
using System;

namespace Neo4J_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = ConfigurationManagerNeo4J.GetDefaultClient();
            PersonDAL personDAL = new PersonDAL();

            User user1 = new User();
            user1.Login = "ivasyk";
            User user2 = new User();
            user2.Login = "vika";
            //personDAL.CreatePerson(user);

            //personDAL.UpdatePerson(user, "ostap");

            //personDAL.DeletePerson(user);

            //personDAL.CreateRelationships(user2, user1);
            //personDAL.DeleteRelationships(user2, user1);
            //var people = personDAL.GetFollowingRelations(user2);

            var people = client.Cypher.Match("(p:Person)").Return(p => p.As<User>()).Results;
            
            foreach (var p in people)
            {
                Console.WriteLine(p.Login + "\n");
            }
            Console.ReadKey();
        }
    }
}