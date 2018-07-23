using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BalancingBinaryTree
{
    public sealed class Tree<T> : IEnumerable<T>
    {
        public Tree(IEnumerable<T> values)
        {
            foreach (T value in values)
            {
                var node = new Node<T>(value);
                Root = Root?.Add(node) ?? node;
            }
        }

        public Tree(params T[] args) : this((IEnumerable<T>) args)
        {
        }

        public Node<T> Root { get; }

        public int Count => Root?.Count ?? 0;

        public IEnumerator<T> GetEnumerator() =>
            GetNodes(Root).Select(x => x.Value).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static IEnumerable<Node<T>> GetNodes(Node<T> current) =>
            current == null
                ? Enumerable.Empty<Node<T>>()
                : GetNodes(current.Left)
                    .Union(new[] {current})
                    .Union(GetNodes(current.Right));
    }
}
