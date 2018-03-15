using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathSearch.Interface
{
    public interface IPathSearcher
    {
        IEnumerable<T> Search<T>(Graph<T> graph, T startNode, T endNode);
    }
}
