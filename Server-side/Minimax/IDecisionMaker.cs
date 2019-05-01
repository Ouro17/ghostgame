using System;

namespace Game
{
    public interface IDecisionMaker<T, V> where T : IComparable<T>
                                          where V : IComparable<V>
    {
        IValueContainer<T, V> Evaluate(Node<T> node, int depth, bool player, bool forcePlay);
    }
}
