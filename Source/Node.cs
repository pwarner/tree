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
                return BalanceLeft(node);
            }

            Left = node;

            return this;
        }

        private Node<T> AddRight(Node<T> node)
        {
            if (Right != null)
            {
                return BalanceRight(node);
            }

            Right = node;

            return this;
        }

        private Node<T> BalanceLeft(Node<T> node)
        {
            Left = Left.Add(node);
            Node<T> displaced = Left.Right;

            if (Left.LeftCount > RightCount)
            {
                return Left.ChangeRight(ChangeLeft(displaced));
            }

            if (Left.RightCount > RightCount)
            {
                return new Node<T>(
                    displaced.Value,
                    Left.ChangeRight(displaced.Left),
                    ChangeLeft(displaced.Right)
                );
            }

            return this;
        }

        private Node<T> BalanceRight(Node<T> node)
        {
            Right = Right.Add(node);
            Node<T> displaced = Right.Left;

            if (Right.RightCount > LeftCount)
            {
                return Right.ChangeLeft(ChangeRight(displaced));
            }

            if (Right.LeftCount > LeftCount)
            {
                return new Node<T>(displaced.Value,
                    ChangeRight(displaced.Left),
                    Right.ChangeLeft(displaced.Right));
            }

            return this;
        }

        private Node<T> ChangeLeft(Node<T> newLeft) => new Node<T>(Value, newLeft, Right);
        private Node<T> ChangeRight(Node<T> newRight) => new Node<T>(Value, Left, newRight);
    }
}
