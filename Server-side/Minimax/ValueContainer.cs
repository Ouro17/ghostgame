using System;
using System.Collections.Generic;

namespace Game
{
    public class ValueContainer<T, V> : IComparable<IValueContainer<T, V>>, IValueContainer<T, V> where V : IComparable<V>
    {
        private bool plusInfinity;
        public bool PlusInfinity {
            get
            {
                return plusInfinity;
            }
            set
            {
                plusInfinity = value;

                // If we are plus infinity, cant be minus infinity
                if (minusInfinity && value)
                {
                    minusInfinity = false;
                }
            }
        }

        private bool minusInfinity;
        public bool MinusInfinity {
            get
            {
                return minusInfinity;
            }
            set
            {
                minusInfinity = value;

                // If we are minus infinity, cant be plus infinity
                if (plusInfinity && value)
                {
                    plusInfinity = false;
                }
            }
        }

        private V points;
        public V Points {
            get
            {
                return points;
            }
            set
            {
                this.points = value;

                // Have a value, cant be neither minus or plus infinity
                MinusInfinity = false;
                PlusInfinity = false;
            }
        }

        public T Value { get; set; }

        public int CompareTo(IValueContainer<T, V> other)
        {
            int result = 0;

            if (!other.Equals(this))
            {
                if (other.PlusInfinity)
                {
                    result = -1; // this is smaller
                }
                else if (other.MinusInfinity)
                {
                    result = 1; // this is bigger
                }
                else if (this.PlusInfinity)
                {
                    result = 1; // this is bigger
                }
                else if (this.MinusInfinity)
                {
                    result = -1; // this is smaller
                }
                else
                {
                    result = this.Points.CompareTo(other.Points);
                }
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            return obj is ValueContainer<T, V> container &&
                   PlusInfinity == container.PlusInfinity &&
                   MinusInfinity == container.MinusInfinity &&
                   EqualityComparer<V>.Default.Equals(Points, container.Points);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PlusInfinity, MinusInfinity, Points);
        }
    }
}
