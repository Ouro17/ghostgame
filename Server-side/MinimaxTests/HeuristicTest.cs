using Game;
using NUnit.Framework;

namespace MinimaxTests
{
    public class HeuristicTest
    {
        private readonly IHeuristic<string, int> heuristic;

        public HeuristicTest()
        {
            heuristic = new AHeuristic();
        }

        [Test]
        public void Test()
        {
            string word = "hello";
            IValueContainer<string, int> value = heuristic.Evaluate(word);
            Assert.That(value.Points, Is.EqualTo(word.Length));
            Assert.That(value.Value, Is.EqualTo(word));

            value = heuristic.Evaluate(string.Empty);
            Assert.That(value.Points, Is.EqualTo(0));
            Assert.That(value.Value, Is.EqualTo(string.Empty));

            value = heuristic.Evaluate("");
            Assert.That(value.Points, Is.EqualTo(0));
            Assert.That(value.Value, Is.EqualTo(string.Empty));

            value = heuristic.Evaluate(null);
            Assert.That(value.Points, Is.EqualTo(0));
            Assert.That(value.Value, Is.EqualTo(string.Empty));
        }
    }

    class AHeuristic : IHeuristic<string, int>
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
                Points = value.Length,
                Value = value
            };
        }
    }
}
