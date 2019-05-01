using System;

namespace Game
{
    public interface IValueContainer<T, V> where V : IComparable<V>
    {
        bool MinusInfinity { get; set; }
        bool PlusInfinity { get; set; }
        V Points { get; set; }
        T Value { get; set; }

        int CompareTo(IValueContainer<T, V> other);
        bool Equals(object obj);
        int GetHashCode();
    }
}