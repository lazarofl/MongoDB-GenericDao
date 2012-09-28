using System;
using System.Collections.Generic;

namespace MongoDbGenericDao.Interfaces
{
    public interface IDao<T, ID> where T : MongoDBEntity
    {
        /// <summary>
        /// Gets the Entity by ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        T GetByID(ID id);
        /// <summary>
        /// Gets all Entities.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();
        /// <summary>
        /// Gets the first Entity by condition.
        /// </summary>
        /// <param name="func">The condition.</param>
        /// <returns></returns>
        T GetByCondition(System.Linq.Expressions.Expression<Func<T, bool>> condition);
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
        /// Saves the Entity.
        /// </summary>
        /// <param name="pobject">The pobject.</param>
        /// <returns></returns>
        T Save(T pobject);
        /// <summary>
        /// Deletes the Entity.
        /// </summary>
        /// <param name="pobject">The pobject.</param>
        void Delete(T pobject);
    }
}
