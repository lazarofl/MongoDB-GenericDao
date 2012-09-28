using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using System.Linq;

namespace MongoDbGenericDao
{
    public class MongoDBGenericDao<T> : Interfaces.IDao<T, string> where T : MongoDBEntity
    {
        private MongoDatabase _repository;
        private readonly string collectioname = typeof(T).Name;

        public MongoDBGenericDao(string pConnectionstring)
        {
            var conn = new MongoConnectionStringBuilder(pConnectionstring);
            _repository = MongoServer.Create(conn).GetDatabase(conn.DatabaseName);
        }

        public T GetByID(string _id)
        {
            return _repository.GetCollection<T>(collectioname).FindOne(Query.EQ("_id", new ObjectId(_id)));
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetCollection<T>(collectioname).FindAll();
        }

        public T GetByCondition(System.Linq.Expressions.Expression<Func<T, bool>> condition)
        {
            return _repository.GetCollection<T>(collectioname).AsQueryable().Where(condition).FirstOrDefault();
        }

        public IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> condition)
        {
            return _repository.GetCollection<T>(collectioname).AsQueryable().Where(condition).ToList();
        }

        public IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> condition, int maxresult, bool orderByDescending)
        {
            var query = _repository.GetCollection<T>(collectioname).AsQueryable().Where(condition);

            if (orderByDescending)
                query.OrderByDescending(x => x.Id);
            else
                query.OrderBy(x => x.Id);

            return query.Take(maxresult);
        }

        public T Save(T pobject)
        {
            _repository.GetCollection<T>(collectioname).Save(pobject);
            return pobject;
        }

        public void Delete(T pobject)
        {
            _repository.GetCollection<T>(collectioname).Remove(Query.EQ("_id", new ObjectId(pobject.Id)));
        }
    }
}
