using NUnit.Framework;
using PathSearch.Test.Fake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathSearch.Test
{
    [TestFixture]
    public class BfsSearcherTest
    {
        private BfsSearcher _searcher;

        [SetUp]
        public void SetUp()
        {
            _searcher = new BfsSearcher();
        }

        [Test]
        public void Search_SearchForEmptyGraph_ReturnEmptyPath()
        {
            var emptyGraph = new FakeGraph(new byte[0, 0]);

            var path = _searcher.Search(emptyGraph, new FakePoint(0, 0), new FakePoint(0,0));

            Assert.That(!path.Any());
        }

        [Test]
        public void Search_SearchForStartNodeThatNotInGraph_ReturnEmptyPath()
        {
            var emptyGraph = new FakeGraph(new byte[,] { {0,0 }, {0,0 } });

            var path = _searcher.Search(emptyGraph, new FakePoint(5, 5), new FakePoint(0, 0));

            Assert.That(!path.Any());
        }

        [Test]
        public void Search_SearchForEndNodeThatNotInGraph_ReturnEmptyPath()
        {
            var emptyGraph = new FakeGraph(new byte[,] { { 0, 0 }, { 0, 0 } });

            var path = _searcher.Search(emptyGraph, new FakePoint(0, 0), new FakePoint(5, 5));

            Assert.That(!path.Any());
        }

        [Test]
        public void Search_SearchForValidData_ReturnShortestPath()
        {
            var emptyGraph = new FakeGraph(new byte[,] { { 1, 1 , 0 }, { 0, 0, 0 }, { 0, 1, 1} });

            var path = _searcher.Search(emptyGraph, new FakePoint(2, 0), new FakePoint(0, 2)).ToList();

            Assert.That(path[0].Row == 2 && path[0].Column == 0);
            Assert.That(path[1].Row == 1 && path[1].Column == 0);
            Assert.That(path[2].Row == 1 && path[2].Column == 1);
            Assert.That(path[3].Row == 1 && path[3].Column == 2);
            Assert.That(path[4].Row == 0 && path[4].Column == 2);
        }

        [Test]
        public void Search_SearchForSameStartEndNode_ReturnEmptyPath()
        {
            var emptyGraph = new FakeGraph(new byte[,] { { 0 }});

            var path = _searcher.Search(emptyGraph, new FakePoint(0, 0), new FakePoint(0, 0)).ToList();

            Assert.That(!path.Any());
        }
    }
}
