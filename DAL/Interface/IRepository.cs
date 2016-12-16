using System;

namespace DAL.Interface
{
    /// <summary>
    ///     Interface for entities
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : IEntity
    {
        /// <summary>
        ///     Adds to repository
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Add(T entity);

        /// <summary>
        ///     Removes from repository
        /// </summary>
        /// <param name="user"></param>
        void Delete(T user);

        /// <summary>
        ///     Search by predicates
        /// </summary>
        /// <param name="predicates"></param>
        /// <returns></returns>
        int[] GetByPredicate(Func<T, bool>[] predicates);

        /// <summary>
        ///     Search by criteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        int[] SearchForUsers(IDalCriterion<T>[] criteria);

        /// <summary>
        ///     Save
        /// </summary>
        void Save();

        /// <summary>
        ///     Load
        /// </summary>
        void Load();
    }
}
