using Game;
using Ghost;
using NUnit.Framework;

namespace MinimaxTests
{
    public class GhostHeuristicTest
    {
        private readonly IHeuristic<string, int> heuristic;

        public GhostHeuristicTest()
        {
            heuristic = new GhostHeuristic();
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
}
