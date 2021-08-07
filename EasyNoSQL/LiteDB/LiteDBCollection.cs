using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyNoSQL.LiteDB
{
    public class LiteDBCollection<T> : ICollection<T>
    {
        string _collectionName;
        string _dbName;
        public LiteDBCollection(string dbname, string collectionName)
        {
            this._collectionName = collectionName;
            this._dbName = dbname;

            if (typeof(T).BaseType != typeof(ModelBase))
                throw new Exception("model object must Inheritance ModelBase");
        }

        public List<T> Find(Expression<Func<T, bool>> predicate)
        {
            using (var db = new LiteDatabase(_dbName))
            {
                var col = db.GetCollection<T>(_collectionName);
                return col.Find(predicate).ToList();
            }
        }

        public bool IsExists(Expression<Func<T, bool>> predicate)
        {
            using (var db = new LiteDatabase(_dbName))
            {
                var col = db.GetCollection<T>(_collectionName);
                return col.Exists(predicate);
            }
        }

        public void Insert(T model)
        {
            using (var db = new LiteDatabase(_dbName))
            {
                db.GetCollection<T>(_collectionName).Insert(model);
            }
        }

        public void Update(T model)
        {
            using (var db = new LiteDatabase(_dbName))
            {
                db.GetCollection<T>(_collectionName).Update(model);
            }
        }

        public void Delete(string id)
        {

        }
    }
}
