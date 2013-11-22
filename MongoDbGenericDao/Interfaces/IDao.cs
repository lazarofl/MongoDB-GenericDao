using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace MongoDbGenericDao.Interfaces
{
    public interface IDao<T, ID> where T : MongoDBEntity
    {
        /// <summary>
        /// Saves the Entity.
        /// </summary>
        /// <param name="pobject">The pobject.</param>
        /// <returns></returns>
        T Save(T pobject);
        /// <summary>
        /// Gets the Entity by ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        T GetByID(ID id);
        /// <summary>
        /// Gets the first Entity by condition.
        /// </summary>
        /// <param name="func">The condition.</param>
        /// <returns></returns>
        T GetByCondition(System.Linq.Expressions.Expression<Func<T, bool>> condition);
        /// <summary>
        /// Gets all Entities.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();
        /// <summary>
        /// Gets all Entities by condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
        IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> condition);
        /// <summary>
        /// GetAll by query
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="top">TOP Clause, if is [0] will be ignored</param>
        /// <param name="orderByDescending">If is [true] order by desc - If is [false] order by asc</param>
        /// <returns></returns>
        IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> condition, int maxresult, bool orderByDescending);
        /// <summary>
        /// Paginates the specified func.
        /// </summary>
        /// <param name="func">The func.</param>
        /// <param name="pagesize">The pagesize.</param>
        /// <param name="page">The page.</param>
        /// <param name="pOrderByDescending">if set to <c>true</c> [p order by descending].</param>
        /// <returns></returns>
        IEnumerable<T> Paginate<TKey>(System.Linq.Expressions.Expression<Func<T, bool>> condition, int pagesize, int page, Func<T, TKey> pOrderByClause = null, bool pOrderByDescending = false);
        /// <summary>
        /// Paginates the specified func.
        /// </summary>
        /// <param name="pagesize">The pagesize.</param>
        /// <param name="page">The page.</param>
        /// <param name="pOrderByDescending">if set to <c>true</c> [p order by descending].</param>
        /// <returns></returns>
        IEnumerable<T> Paginate<TKey>(int pagesize, int page, Func<T, TKey> pOrderByClause = null, bool pOrderByDescending = false);
        /// <summary>
        /// Seach using 'text' command
        /// </summary>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="foundedRecords"></param>
        /// <returns></returns>
        IEnumerable<T> Search(string field, string search, int page, int pagesize, out long foundedRecords);
        /// <summary>
        /// Seach using 'text' command based in key value pairs And Clause
        /// </summary>
        /// <param name="keys_and_values"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="foundedRecords"></param>
        /// <returns></returns>
        IEnumerable<T> Search_And(IDictionary<string, string> keys_and_values, int page, int pagesize, out long foundedRecords);
        IEnumerable<T> Search_And(IList<IMongoQuery> search, int page, int pagesize, out long foundedRecords);
        /// <summary>
        /// Seach using 'text' command based in key value pairs Or Clause
        /// </summary>
        /// <param name="keys_and_values"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="foundedRecords"></param>
        /// <returns></returns>
        IEnumerable<T> Search_Or(IDictionary<string, string> keys_and_values, int page, int pagesize, out long foundedRecords);
        IEnumerable<T> Search_Or(IList<IMongoQuery> search, int page, int pagesize, out long foundedRecords);
        /// <summary>
        /// Deletes the Entity.
        /// </summary>
        /// <param name="pobject">The pobject.</param>
        void Delete(T pobject);
        /// <summary>
        /// Deletes the Entities.
        /// </summary>
        /// <param name="pobject">The query condition.</param>
        void Delete(System.Linq.Expressions.Expression<Func<T, bool>> condition);
        /// <summary>
        /// Counts the specified condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
        long Count(System.Linq.Expressions.Expression<Func<T, bool>> condition);
        /// <summary>
        /// Count all elements
        /// </summary>
        /// <returns></returns>
        long Count();
    }
}
