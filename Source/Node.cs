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
            Weight++;
            return Comparer.Compare(value, Value) < 0
                ? AddLeft(value)
                : AddRight(value);
        }

        private Node<T> AddLeft(T value)
        {
            if (Left != null)
            {
                Left = Left.Add(value);

                if (Left.LeftWeight > RightWeight)
                    return BalanceLeft();

                if (Left.RightWeight > RightWeight)
                    return Left.Add(Value);
            }
            else
            {
                Left = new Node<T>(value);
            }

            return this;
        }

        private Node<T> AddRight(T value)
        {
            if (Right != null)
            {
                Right = Right.Add(value);

                if (Right.RightWeight > LeftWeight)
                    return BalanceRight();

                if (Right.LeftWeight > LeftWeight)
                    return Right.Add(Value);
            }
            else
            {
                Right = new Node<T>(value);
            }

            return this;
        }

        public override string ToString() => Value.ToString();


        private Node<T> BalanceRight() =>
            new Node<T>(Right.Value, new Node<T>(Value, Left, Right.Left), Right.Right);

        private Node<T> BalanceLeft() =>
            new Node<T>(Left.Value, Left.Left, new Node<T>(Value, Left.Right, Right));
    }
}
