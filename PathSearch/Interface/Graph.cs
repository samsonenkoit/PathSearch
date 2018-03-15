using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathSearch.Interface
{
    public abstract class Graph<T>
    {
        public abstract IEnumerable<T> GetConnected(T node);
    }
}
