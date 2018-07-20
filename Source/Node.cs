using System.Collections.Generic;

namespace BinaryTree
{
    public sealed class Node<T>
    {
        private static readonly IComparer<T> Comparer = Comparer<T>.Default;

        internal Node(T value, Node<T> left = null, Node<T> right = null)
        {
            Value = value;
            Left = left;
            Right = right;
            Weight = 1 + LeftWeight + RightWeight;
        }

        public int Weight { get; private set; }
        public Node<T> Left { get; private set; }
        public Node<T> Right { get; private set; }
        public T Value { get; }

        private int LeftWeight => Left?.Weight ?? 0;
        private int RightWeight => Right?.Weight ?? 0;

        public Node<T> Add(T value)
        {
            if (Comparer.Compare(value, Value) < 0)
            {
                Left = Left?.Add(value) ?? new Node<T>(value);
            }
            else
            {
                Right = Right?.Add(value) ?? new Node<T>(value);
            }

            Weight++;

            return BalanceIfRequired();
        }

        public override string ToString() => Value.ToString();

        private Node<T> BalanceIfRequired()
        {
            int leftGrandChildWeight = Left?.LeftWeight ?? 0;
            int rightGrandChildWeight = Right?.RightWeight ?? 0;

            return leftGrandChildWeight > RightWeight
                ? BalanceLeft()
                : rightGrandChildWeight > LeftWeight
                    ? BalanceRight()
                    : this;
        }

        private Node<T> BalanceRight() =>
            new Node<T>(Right.Value, new Node<T>(Value, Left, Right.Left), Right.Right);

        private Node<T> BalanceLeft() =>
            new Node<T>(Left.Value, Left.Left, new Node<T>(Value, Left.Right, Right));
    }
}
