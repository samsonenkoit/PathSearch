using PathSearch.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathSearch.Test.Fake
{
    public struct FakePoint
    {
        public int Row { get; }
        public int Column { get; }

        public FakePoint(int row, int column)
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
        

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            FakePoint p = (FakePoint)obj;
            return (Row == p.Row) && (Column == p.Column);

        }
    }

    public class FakeGraph : Graph<FakePoint>
    {
        private readonly byte[,] _m;

        public readonly int _rowsCount;
        private readonly int _columnsCount;

        public FakeGraph(byte[,] m)
        {
            _m = m ?? throw new ArgumentNullException(nameof(m));
            _rowsCount = _m.GetLength(0);
            _columnsCount = _m.GetLength(1);
        }

        public override IEnumerable<FakePoint> GetConnected(FakePoint node)
        {
            if (node.Row >= _rowsCount || node.Column >= _columnsCount)
                return new FakePoint[0];

            return UnsafeGetConnected(node);
        }

        private IEnumerable<FakePoint> UnsafeGetConnected(FakePoint node)
        {
            if (node.Row - 1 > -1 && _m[node.Row - 1, node.Column] != 1)
                yield return new FakePoint(node.Row - 1, node.Column);

            if (node.Row + 1 < _rowsCount && _m[node.Row + 1, node.Column] != 1)
                yield return new FakePoint(node.Row + 1, node.Column);

            if (node.Column - 1 > -1 && _m[node.Row, node.Column - 1] != 1)
                yield return new FakePoint(node.Row, node.Column - 1);

            if (node.Column + 1 < _columnsCount && _m[node.Row, node.Column + 1] != 1)
                yield return new FakePoint(node.Row, node.Column + 1);
        }

    }
}
