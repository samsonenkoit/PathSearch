using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathSearch.Interface
{
    public abstract class Graph<T>
    {
        /// <summary>
        /// Возвращает список графов, соединенных с данным
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public abstract IEnumerable<T> GetConnected(T node);
    }
}
