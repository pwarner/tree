using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BinaryTree
{
    public class TreeTests
    {
        [Fact]
        public void QuickLeftBalanceTest()
        {
            var tree = new Tree<int> {7, 6, 5, 4, 3, 2, 1};

            AssertTreeIsBalanced(tree);
        }

        [Fact]
        public void QuickRightBalanceTest()
        {
            var tree = new Tree<int> {1, 2, 3, 4, 5, 6, 7};

            AssertTreeIsBalanced(tree);
        }

        [Fact]
        public void RandomBalanceTest()
        {
            var rand = new Random();
            var tree = new Tree<int>();
            IEnumerable<int> sequence = Enumerable.Range(1, 7)
                .Select(v => new {Value = v, Rank = rand.Next()})
                .OrderBy(x => x.Rank)
                .Select(x => x.Value);
            tree.Add(sequence);

            AssertTreeIsBalanced(tree);
        }

        private static void AssertTreeIsBalanced(Tree<int> tree)
        {
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
