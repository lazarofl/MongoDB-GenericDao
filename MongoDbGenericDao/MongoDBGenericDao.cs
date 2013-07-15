using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using System.Linq;

namespace MongoDbGenericDao
{
    public abstract class MongoDBGenericDao<T> : Interfaces.IDao<T, string> where T : MongoDBEntity
    {
        private MongoDatabase _repository;

        private readonly string collectioname = typeof(T).Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDBGenericDao{T}" /> class.
        /// </summary>
        /// <param name="pConnectionstring">The connectionstring.</param>
        public MongoDBGenericDao(string pConnectionstring)
        {
            var conn = new MongoConnectionStringBuilder(pConnectionstring);
            _repository = MongoServer.Create(conn).GetDatabase(conn.DatabaseName);
        }

        /// <summary>
        /// Gets the by ID.
        /// </summary>
        /// <param name="_id">The _id.</param>
        /// <returns></returns>
        public virtual T GetByID(string _id)
        {
            return _repository.GetCollection<T>(collectioname).FindOne(Query.EQ("_id", new ObjectId(_id)));
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetCollection<T>(collectioname).FindAll();
        }

        /// <summary>
        /// Gets the by condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
        public virtual T GetByCondition(System.Linq.Expressions.Expression<Func<T, bool>> condition)
        {
            return _repository.GetCollection<T>(collectioname).AsQueryable().Where(condition).FirstOrDefault();
        }

        /// <summary>
        /// Gets all using a condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> condition)
        {
            return _repository.GetCollection<T>(collectioname).AsQueryable().Where(condition).ToList();
        }

        /// <summary>
        /// Gets all using a condition, a TOP clause and an optional orderBy clause.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="maxresult">The maxresult.</param>
        /// <param name="orderByDescending">if set to <c>true</c> [order by descending].</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> condition, int maxresult, bool orderByDescending = false)
        {
            var query = _repository.GetCollection<T>(collectioname).AsQueryable().Where(condition);

            if (orderByDescending)
                query.OrderByDescending(x => x.Id);
            else
                query.OrderBy(x => x.Id);

            return query.Take(maxresult);
        }

        /// <summary>
        /// Saves the specified pobject.
        /// </summary>
        /// <param name="pobject">The pobject.</param>
        /// <returns></returns>
        public virtual T Save(T pobject)
        {
            _repository.GetCollection<T>(collectioname).Save(pobject);
            return pobject;
        }

        /// <summary>
        /// Deletes the specified pobject.
        /// </summary>
        /// <param name="pobject">The pobject.</param>
        public virtual void Delete(T pobject)
        {
            _repository.GetCollection<T>(collectioname).Remove(Query.EQ("_id", new ObjectId(pobject.Id)));
        }

        /// <summary>
        /// Deletes the specified pobject.
        /// </summary>
        /// <param name="pobject">The pobject.</param>
        public virtual void Delete(System.Linq.Expressions.Expression<Func<T, bool>> condition)
        {
            var todelete = this.GetAll(condition) as List<T>;
            if (todelete != null && todelete.Count > 0)
            {
                for (int i = 0; i < todelete.Count; i++)
                    this.Delete(todelete[i]);
            }
        }

        /// <summary>
        /// Counts the specified condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
        public virtual long Count(System.Linq.Expressions.Expression<Func<T, bool>> condition)
        {
            return _repository.GetCollection<T>(collectioname).AsQueryable().Where(condition).LongCount();
        }

        /// <summary>
        /// Count all elements
        /// </summary>
        /// <returns></returns>
        public virtual long Count()
        {
            return _repository.GetCollection<T>(collectioname).Count();
        }

        /// <summary>
        /// Paginates by an specified condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="pagesize">The pagesize.</param>
        /// <param name="page">The page.</param>
        /// <param name="pOrderByClause">The OrderBy Clause.</param>
        /// <param name="pOrderByDescending">if set to <c>true</c> [order by descending].</param>
        /// <returns></returns>
        public virtual IEnumerable<T> Paginate<Tkey>(System.Linq.Expressions.Expression<Func<T, bool>> condition, int pagesize, int page, Func<T, Tkey> pOrderByClause = null, bool pOrderByDescending = false)
        {
            var query = _repository.GetCollection<T>(collectioname).AsQueryable().Where(condition);

            if (pOrderByDescending)
                query.OrderByDescending(x => x.Id);
            else
                query.OrderBy(x => x.Id);

            if (pOrderByDescending)
                return query.OrderByDescending(pOrderByClause).Skip(pagesize * (page - 1)).Take(pagesize);
            else
                return query.OrderBy(pOrderByClause).Skip(pagesize * (page - 1)).Take(pagesize);

        }

        /// <summary>
        /// Paginates by an specified condition.
        /// </summary>
        /// <param name="pagesize">The pagesize.</param>
        /// <param name="page">The page.</param>
        /// <param name="pOrderByClause">The OrderBy Clause.</param>
        /// <param name="pOrderByDescending">if set to <c>true</c> [order by descending].</param>
        /// <returns></returns>
        public virtual IEnumerable<T> Paginate<Tkey>(int pagesize, int page, Func<T, Tkey> pOrderByClause = null, bool pOrderByDescending = false)
        {
            var query = _repository.GetCollection<T>(collectioname).AsQueryable();

            if (pOrderByDescending)
                query.OrderByDescending(x => x.Id);
            else
                query.OrderBy(x => x.Id);

            if (pOrderByDescending)
                return query.OrderByDescending(pOrderByClause).Skip(pagesize * (page - 1)).Take(pagesize);
            else
                return query.OrderBy(pOrderByClause).Skip(pagesize * (page - 1)).Take(pagesize);
        }
    }
}
