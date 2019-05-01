using Game;

namespace Ghost
{
    public class GhostHeuristic : IHeuristic<string, int>
    {
        /// <summary>
        /// Given a string gave a approximate value of its value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IValueContainer<string, int> Evaluate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new ValueContainer<string, int>
                {
                    Points = 0,
                    Value = string.Empty
                };
            }

            return new ValueContainer<string, int>
            {
                Points = value.Length * (value.Length % 2 != 0 ? 1 : -1),
                Value = value
            };
        }
    }
}
