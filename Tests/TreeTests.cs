using System;
using System.Linq;
using Xunit;

namespace BalancingBinaryTree
{
    public static class TreeTests
    {
        [Fact]
        public static void PerfectBalanceLeftTest()
        {
            var tree = new Tree<int>(7, 6, 5, 4, 3, 2, 1);

            AssertTreeIsBalanced(tree);
        }

        [Fact]
        public static void PerfectBalanceRightTest()
        {
            var tree = new Tree<int>(1, 2, 3, 4, 5, 6, 7);

            AssertTreeIsBalanced(tree);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(2, 3, 1)]
        [InlineData(3, 1, 2)]
        [InlineData(1, 3, 2)]
        [InlineData(2, 1, 3)]
        [InlineData(3, 2, 1)]
        public static void ThreeNodePermutationTest(int a, int b, int c)
        {
            var tree = new Tree<int>(a, b, c);

            Assert.Equal(3, tree.Count);

            Assert.Equal(2, tree.Root.Value);
            Assert.Equal(1, tree.Root.Left.Value);
            Assert.Equal(3, tree.Root.Right.Value);
        }

        [Fact]
        public static void EnumeratesInAscendingOrder()
        {
            var rand = new Random();

            var expected = Enumerable.Range(1, 20).ToArray();

            var randomSequence = expected
                .Select(v => new {Value = v, Rank = rand.Next()})
                .OrderBy(x => x.Rank)
                .Select(x => x.Value);

            var actual = new Tree<int>(randomSequence).ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public static void BalancesRandomSequence()
        {
            var tree = new Tree<int>(2, 6, 7, 3, 5, 1, 4);

            Assert.Equal(7, tree.Count);

            Assert.Equal(3, tree.Root.Value);
            Assert.Equal(2, tree.Root.Left.Value);
            Assert.Equal(1, tree.Root.Left.Left.Value);
            Assert.Equal(6, tree.Root.Right.Value);
            Assert.Equal(5, tree.Root.Right.Left.Value);
            Assert.Equal(7, tree.Root.Right.Right.Value);
            Assert.Equal(4, tree.Root.Right.Left.Left.Value);
        }

        [Fact]
        public static void CanFindNode()
        {
            var tree = new Tree<int>(2, 6, 7, 3, 5, 1, 4);

            var node1 = tree.Find(1);
            Assert.NotNull(node1);
            Assert.Same(tree.Root.Left.Left, node1);

            var node7 = tree.Find(7);
            Assert.NotNull(node7);
            Assert.Same(tree.Root.Right.Right, node7);

            var node4 = tree.Find(4);
            Assert.NotNull(node4);
            Assert.Same(tree.Root.Right.Left.Left, node4);
        }

        [Fact]
        public static void FindMissingValueReturnsNull()
        {
            var tree = new Tree<int>(2, 6, 7, 3, 5, 1, 4);
            var node11 = tree.Find(11);
            Assert.Null(node11);
        }

        [Fact]
        public static void FindsNearestNodeIfValueNotPresent()
        {
            var tree = new Tree<int>(4, 9, 1, 8, 5, 2, 3, 6);
            var node7 = tree.FindNearest(7);

            Assert.NotNull(node7);
            Assert.Equal(6, node7.Value);
        }

        [Fact]
        public static void CanGetRangeOfNodes()
        {
            var tree = new Tree<int>(4, 9, 1, 8, 5, 2, 3, 6);
            var nodes = tree.GetRange(3, 7).ToArray();

            Assert.Equal(4, nodes.Length);
            Assert.Equal(3, nodes[0].Value);
            Assert.Equal(4, nodes[1].Value);
            Assert.Equal(5, nodes[2].Value);
            Assert.Equal(6, nodes[3].Value);
        }

        private static void AssertTreeIsBalanced(Tree<int> tree)
        {
            Assert.Equal(7, tree.Count);

            Assert.Equal(4, tree.Root.Value);
            Assert.Equal(2, tree.Root.Left.Value);
            Assert.Equal(1, tree.Root.Left.Left.Value);
            Assert.Equal(3, tree.Root.Left.Right.Value);
            Assert.Equal(6, tree.Root.Right.Value);
            Assert.Equal(5, tree.Root.Right.Left.Value);
            Assert.Equal(7, tree.Root.Right.Right.Value);
        }
    }
}
