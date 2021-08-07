using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasyNoSQL.MongoDB
{
    public class MongoDBCollection<T> : ICollection<T>
    {
        string _connectionString;
        string _collectionName;
        string _dbName;

        public MongoDBCollection(string connectionString, string dbname, string collectionName)
        {
            this._connectionString = connectionString;
            this._collectionName = collectionName;
            this._dbName = dbname;

            if (typeof(T).BaseType != typeof(ModelBase))
                throw new Exception("model object must Inheritance ModelBase");
        }
        public List<T> Find(Expression<Func<T, bool>> predicate)
        {
            return GetDB().GetCollection<T>(_collectionName).AsQueryable().Where(predicate).ToList();
        }

        public void Insert(T model)
        {
            GetDB().GetCollection<T>(_collectionName).InsertOne(model);
        }

        public bool IsExists(Expression<Func<T, bool>> predicate)
        {
            return Find(predicate).Count() != 0;
        }

        public void Update(T model)
        {
            FilterDefinition<T> filter = GetFilter(model);
            UpdateDefinition<T> update = GetUpdate(model);
            GetDB().GetCollection<T>(_collectionName).UpdateOne(filter, update);
        }

        private UpdateDefinition<T> GetUpdate(T model)
        {
            var tmp = Builders<T>.Update;
            UpdateDefinition<T> update = null;
            Type type = model.GetType();
            foreach (var member in type.GetMembers().Where(x => x.MemberType == MemberTypes.Property))
            {
                if (member.Name == "_id")
                    continue;

                if (update == null)
                    update = tmp.Set(member.Name, type.GetProperty(member.Name).GetValue(model));

                else
                    update = update.Set(member.Name, type.GetProperty(member.Name).GetValue(model));
            }
            return update;
        }

        private FilterDefinition<T> GetFilter(T model)
        {
            string id = GetId(model);
            return Builders<T>.Filter.Eq("_id", id);
        }

        private IMongoDatabase GetDB()
        {
            var cli = new MongoClient(_connectionString);
            return cli.GetDatabase(_dbName);
        }
        private string GetId(T model)
        {
            Type type = model.GetType();

            foreach (var member in type.GetMembers())
            {
                if (member.Name == "_id")
                    return type.GetProperty(member.Name).GetValue(model).ToString();
            }
            return null;
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
