using System.Collections.Generic;

namespace BalancingBinaryTree
{
    public sealed class Node<T>
    {
        private static readonly IComparer<T> Comparer = Comparer<T>.Default;

        internal Node(T value, Node<T> left = null, Node<T> right = null)
        {
            Value = value;
            Left = left;
            Right = right;
            Count = 1 + LeftCount + RightCount;
        }

        public int Count { get; private set; }

        public Node<T> Left { get; private set; }

        public Node<T> Right { get; private set; }

        public T Value { get; }

        private int LeftCount => Left?.Count ?? 0;

        private int RightCount => Right?.Count ?? 0;

        public override string ToString() => Value.ToString();

        internal Node<T> Add(Node<T> child)
        {
            Count++;
            return Comparer.Compare(child.Value, Value) < 0
                ? AddLeft(child)
                : AddRight(child);
        }

        private Node<T> AddLeft(Node<T> node)
        {
            if (Left != null)
            {
                Left = Left.Add(node);

                if (Left.LeftCount > RightCount)
                {
                    return new Node<T>(Left.Value, Left.Left, new Node<T>(Value, Left.Right, Right));
                }

                if (Left.RightCount > RightCount)
                {
                    return Left.Add(new Node<T>(Value, null, Right));
                }
            }
            else
            {
                Left = node;
            }

            return this;
        }

        private Node<T> AddRight(Node<T> node)
        {
            if (Right != null)
            {
                Right = Right.Add(node);

                if (Right.RightCount > LeftCount)
                {
                    return new Node<T>(Right.Value, new Node<T>(Value, Left, Right.Left), Right.Right);
                }

                if (Right.LeftCount > LeftCount)
                {
                    return Right.Add(new Node<T>(Value, Left, null));
                }
            }
            else
            {
                Right = node;
            }

            return this;
        }
    }
}
