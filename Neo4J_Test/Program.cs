using Entity;
using Neo4J;
using Neo4jClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo4J_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = ConfigurationManagerNeo4J.GetDefaultClient(); //need replace to DAL
            PersonDAL personDAL = new PersonDAL();

            User user1 = new User();
            user1.Login = "ostap";
            User user2 = new User();
            user2.Login = "vika";
            //personDAL.CreatePerson(user);

            //personDAL.UpdatePerson(user, "ostap");

            //personDAL.DeletePerson(user);

            personDAL.CreateRelationship(user2, user1);
            //personDAL.DeleteRelationship(user2, user1);

            var people = client.Cypher.Match("(p:Person)").Return(p => p.As<User>()).Results;
            
            foreach (var p in people)
            {
                Console.WriteLine(p.Login + "\n");
            }
            Console.ReadKey();
        }
    }
}
