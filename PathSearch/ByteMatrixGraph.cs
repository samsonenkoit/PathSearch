using PathSearch.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathSearch
{
    public struct Point
    {
        public int Row { get; }
        public int Column { get;  }

        public Point(int row, int column)
        {
            if (row < 0 || column < 0)
                throw new ArgumentOutOfRangeException($"{nameof(row)} and {nameof(column)} mast be grate than zero");

            Row = row;
            Column = column;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + Row.GetHashCode();
            hash = hash * 23 + Column.GetHashCode();
            return hash;
        }

        public bool Equals(Point p)
        {
            return (Row == p.Row) && (Column == p.Column);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            
            Point p = (Point)obj;
            return Equals(p);

        }
    }

    public class ByteMatrixGraph : Graph<Point>
    {
        private readonly byte[,] _m;

        public readonly int _rowsCount;
        private readonly int _columnsCount;
        
        public ByteMatrixGraph(byte[,] m)
        {
            _m = m ?? throw new ArgumentNullException(nameof(m));
            _rowsCount = _m.GetLength(0);
            _columnsCount = _m.GetLength(1);
        }

        public override IEnumerable<Point> GetConnected(Point node)
        {
            if (node.Row >= _rowsCount || node.Column >= _columnsCount)
                return new Point[0];

            return UnsafeGetConnected(node);
        }

        private IEnumerable<Point> UnsafeGetConnected(Point node)
        {
            if (node.Row - 1 > -1 && _m[node.Row - 1, node.Column] != 1)
                yield return new Point(node.Row - 1, node.Column);

            if (node.Row + 1 < _rowsCount && _m[node.Row + 1, node.Column] != 1)
                yield return new Point(node.Row + 1, node.Column);

            if (node.Column - 1 > -1 && _m[node.Row, node.Column - 1] != 1)
                yield return new Point(node.Row, node.Column - 1);

            if (node.Column + 1 < _columnsCount && _m[node.Row, node.Column + 1] != 1)
                yield return new Point(node.Row, node.Column + 1);
        }

    }
}
