using System;
using System.Collections.Generic;

namespace Windsor.SLExample.Services
{
    public interface IRepository<T> : IRepository where T : class
    {
        /// <summary>
        /// Finds one instance
        /// </summary>
        /// <returns></returns>
        T Find(Func<T, bool> predicate);

        /// <summary>
        /// Gets all instances
        /// </summary>
        /// <returns></returns>
        IList<T> GetAll();

        /// <summary>
        /// Saves the instance
        /// </summary>
        /// <param name="instance"></param>
        void Save(T instance);
    }

    public interface IRepository
    {
    }
}