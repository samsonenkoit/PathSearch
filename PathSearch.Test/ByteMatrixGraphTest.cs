using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathSearch.Test
{
    [TestFixture]
    public class ByteMatrixGraphTest
    {
        private ByteMatrixGraph _graph;

        [SetUp]
        public void SetUp()
        {
            byte[,] m = new byte[,]
            {
                {1,1,0,1,0,0 },
                {0,0,0,0,1,0 },
                {0,0,1,0,0,0 },
                {0,0,0,1,0,0 },
                {0,1,0,1,1,1 },
                {0,1,0,0,1,0 }
            };

            _graph = new ByteMatrixGraph(m);
        }

        [Test]
        public void GetConnected_GetConnectedForValidNode_GetConnectedNodes()
        {
            var connectedNodes = _graph.GetConnected(new Point(5, 0)).ToList();

            Assert.That(connectedNodes.Count == 1);
            Assert.That(connectedNodes[0].Column == 0 && connectedNodes[0].Row == 4);
        }

        [Test]
        public void GetConnected_GetConnectedForValidNode_GetConnectedNodes2()
        {
            var connectedNodes = _graph.GetConnected(new Point(3, 1)).ToList();

            Assert.That(connectedNodes.Count == 3);
            Assert.That(connectedNodes.Any(t => t.Row == 3 && t.Column == 0));
            Assert.That(connectedNodes.Any(t => t.Row == 2 && t.Column == 1));
            Assert.That(connectedNodes.Any(t => t.Row == 3 && t.Column == 2));
        }

        [Test]
        public void GetConnected_GetConnectedForIzolateNode_ReturnEmptyList()
        {
            var connectedNodes = _graph.GetConnected(new Point(5, 5)).ToList();

            Assert.That(!connectedNodes.Any());
        }

        [Test]
        public void GetConnected_GetConnectedForUnvalidNode_ReturnEmptyList()
        {
            var connectedNodes = _graph.GetConnected(new Point(50, 50)).ToList();

            Assert.That(!connectedNodes.Any());
        }
    }
}
