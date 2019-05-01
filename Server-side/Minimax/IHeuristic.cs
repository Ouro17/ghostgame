using System;

namespace Game
{
    public interface IHeuristic<K, V> where K : IComparable<K> 
                                      where V : IComparable<V>
    {
        IValueContainer<K, V> Evaluate(K value);
    }
}
