using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BalancingBinaryTree
{
    public sealed class Tree<T> : IEnumerable<T>
    {
        public static readonly IComparer<T> Comparer = Comparer<T>.Default;

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

        public IEnumerator<T> GetEnumerator() => GetNodes(Root).Select(x => x.Value).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Node<T> Find(T value) => Find(Root, value);

        public Node<T> FindNearest(T value) => FindNearest(Root, value);

        public IEnumerable<Node<T>> GetRange(T min, T max) => GetRange(Root, min, max);

        private static IEnumerable<Node<T>> GetRange(Node<T> current, T min, T max) =>
            current == null
                ? Enumerable.Empty<Node<T>>()
                : Comparer.Compare(current.Value, min) < 0
                    ? GetRange(current.Right, min, max)
                    : Comparer.Compare(current.Value, max) > 0
                        ? GetRange(current.Left, min, max)
                        : GetRange(current.Left, min, max)
                            .Union(new[] {current})
                            .Union(GetRange(current.Right, min, max));

        private static IEnumerable<Node<T>> GetNodes(Node<T> current) =>
            current == null
                ? Enumerable.Empty<Node<T>>()
                : GetNodes(current.Left)
                    .Union(new[] {current})
                    .Union(GetNodes(current.Right));

        private static Node<T> Find(Node<T> current, T value) =>
            current == null
                ? null
                : Comparer.Compare(value, current.Value) == 0
                    ? current
                    : Comparer.Compare(value, current.Value) < 0
                        ? Find(current.Left, value)
                        : Find(current.Right, value);

        private static Node<T> FindNearest(Node<T> current, T value) =>
            current == null
                ? null
                : Comparer.Compare(value, current.Value) == 0
                    ? current
                    : Comparer<T>.Default.Compare(value, current.Value) < 0
                        ? FindNearest(current.Left, value) ?? current
                        : FindNearest(current.Right, value) ?? current;
    }
}
