using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
    public class Minimax<T, V> : IDecisionMaker<T, V> where T : IComparable<T>
                                                      where V : IComparable<V>
    {
        private IHeuristic<T, V> heuristic;

        private Random random;

        private readonly IValueContainer<T, V> minusInfinity = new ValueContainer<T, V>
        {
            MinusInfinity = true
        };

        private readonly IValueContainer<T, V> plusInfinity = new ValueContainer<T, V>
        {
            PlusInfinity = true
        };

        public Minimax(IHeuristic<T, V> heuristic)
        {
            this.heuristic = heuristic;

            random = new Random();
        }

        /// <summary>
        /// Evaluate the current node, looking for the best match that keep the game playing.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="depth"></param>
        /// <param name="maximizingPlayer"></param>
        /// <param name="forceSearch"></param>
        /// <returns>The value that thinks it's better to continue the game.</returns>
        public IValueContainer<T, V> Evaluate(Node<T> node, int depth, bool maximizingPlayer, bool forceSearch)
        {
            IValueContainer<T, V> result = new ValueContainer<T, V>();
            ICollection<IValueContainer<T, V>> results = new List<IValueContainer<T, V>>();

            // Sometimes it's needed to force the search. For example if the value is less than some condition.
            if (!forceSearch && (depth == 0 || node.Terminal))
            {
                return heuristic.Evaluate(node.Value);
            }

            // Maximize the gain between own best play and opponents best play.
            if (maximizingPlayer)
            {
                results.Add(minusInfinity);

                foreach (Node<T> child in node.Children)
                {
                    // Get the best child for this player
                    IValueContainer<T, V> maxOfChild = Max(minusInfinity, Evaluate(child, depth - 1, false, false));

                    // If the result of this node is better that what we have before, this is our new record.
                    if (maxOfChild.CompareTo(results.First()) > 0)
                    {
                        results.Clear();
                        results.Add(maxOfChild);
                    }
                    // If the result if the same, add it to the list
                    else if (maxOfChild.CompareTo(results.First()) == 0)
                    {
                        results.Add(maxOfChild);
                    }
                    // Ignore if not better or equal
                }
            }
            else
            {
                results.Add(plusInfinity);

                foreach (Node<T> child in node.Children)
                {
                    // Get the best child for this player
                    IValueContainer<T, V> minOfChild = Min(plusInfinity, Evaluate(child, depth - 1, true, false));

                    // If the result of this node is better that what we have before, this is our new record.
                    if (minOfChild.CompareTo(results.First()) < 0)
                    {
                        results.Clear();
                        results.Add(minOfChild);
                    }
                    // If the result if the same, add it to the list
                    else if (minOfChild.CompareTo(results.First()) == 0)
                    {
                        results.Add(minOfChild);
                    }
                    // Ignore if not better or equal
                }
            }

            return results.ElementAt(random.Next(0, results.Count()));
        }

        private IValueContainer<T, V> Max(IValueContainer<T, V> first, IValueContainer<T, V> second)
        {
            return first.CompareTo(second) > 0 ? first : second;
        }

        private IValueContainer<T, V> Min(IValueContainer<T, V> first, IValueContainer<T, V> second)
        {
            return first.CompareTo(second) < 0 ? first : second;
        }
    }
}
