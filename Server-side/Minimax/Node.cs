using System;
using System.Collections.Generic;

namespace Game
{
    public class Node<T> where T : IComparable<T>
    {
        public bool Terminal { get; set; } = false;
        public T Value { get; set; } = default(T);
        public HashSet<Node<T>> Children { get; set; } = new HashSet<Node<T>>();

        public override bool Equals(object obj)
        {
            return obj is Node<T> node &&
                   EqualityComparer<T>.Default.Equals(Value, node.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }
}
