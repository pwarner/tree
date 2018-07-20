using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTree
{
    public sealed class Tree<T> : IEnumerable<T>
    {
        public Node<T> Root { get; private set; }

        public int Count => Root?.Weight ?? 0;

        public IEnumerator<T> GetEnumerator() =>
            GetNodes(Root).Select(x => x.Value).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T value)
        {
            Root = Root?.Add(value) ?? new Node<T>(value);
        }

        public void Add(IEnumerable<T> values)
        {
            foreach (T value in values)
            {
                Add(value);
            }
        }

        private static IEnumerable<Node<T>> GetNodes(Node<T> current) =>
            current == null
                ? Enumerable.Empty<Node<T>>()
                : GetNodes(current.Left)
                    .Union(new[] {current})
                    .Union(GetNodes(current.Right));
    }
}
