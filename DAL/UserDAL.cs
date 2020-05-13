using Entity;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class UserDAL
    {
        IMongoDatabase db;
        IMongoCollection<User> collection;

        public UserDAL()
        {
            db = ConfigurationManager.GetDefaultDatabase();
            collection = db.GetCollection<User>(GetTableName());
        }

        private string GetTableName()
        {
            return "user";
        }

        // Insert
        public void InsertUser(User user) =>
           collection.InsertOne(user);

        // Update
        public void UpdateUser(ObjectId id, User user) =>
            collection.ReplaceOne(u => u.Id == id, user);

        public void UpdateByField(string login, string fieldName, string value)
        {
            var filter = Builders<User>.Filter.Eq("Login", login);
            var update = Builders<User>.Update.Set(fieldName, value);
            collection.UpdateOne(filter, update);
        }

        public void UpdateByDateTime(string login)
        {
            var filter = Builders<User>.Filter.Eq("Login", login);
            var update = Builders<User>.Update.Set("LastLogin", DateTime.Now.ToString());
            collection.UpdateOne(filter, update);
        }

        // Select
        public List<User> SelectAllUsers() =>
            collection.Find(u => true).ToList();

        public User SelectById(ObjectId id) =>
         collection.Find(u => u.Id == id).FirstOrDefault();

        public User SelectByLogin(string login) =>
            collection.Find(u => u.Login == login).FirstOrDefault();

        // Delete
        public void DeleteUser(User user) =>
             collection.DeleteOne(u => u.Id == user.Id);

        public void DeleteById(ObjectId userId) =>
            collection.DeleteOne(u => u.Id == userId);

        public void DeleteByNickname(string login) =>
            collection.DeleteOne(u => u.Login == login);


        public ObjectId GetUserId(string login)
        {
            var user = collection.Find(u => u.Login == login).FirstOrDefault();
            return user.Id;
        }
    }
}
