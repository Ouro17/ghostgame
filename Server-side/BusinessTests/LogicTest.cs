using Business;
using Game;
using Ghost;
using NUnit.Framework;

namespace BusinessTests
{
    public class LogicTest
    {
        private readonly ILogic<string> logic;

        public LogicTest()
        {
            logic = new GhostLogic(new GhostTree(), new Minimax<string, int>(new GhostHeuristic()), new Resource());
        }

        [Test]
        public void NoMatchTest()
        {
            string word = "&";

            ResultContainer<string> result = logic.Play(word);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Status, Is.EqualTo(StatusEnum.MachineWin));
            Assert.That(result.Element, Is.EqualTo(word));
        }

        [Test]
        public void PlayTest()
        {
            string word = "r";

            ResultContainer<string> result = logic.Play(word);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Status, Is.EqualTo(StatusEnum.Continue));
            Assert.That(result.Element.Length, Is.EqualTo(word.Length + 1));

        }

        [Test]
        public void MachineWinTest()
        {
            string word = "helium";

            ResultContainer<string> result = logic.Play(word);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Status, Is.EqualTo(StatusEnum.MachineWin));
            Assert.That(result.Element, Is.EqualTo(word));
        }

        [Test]
        public void PlayerWinTest()
        {
            string word = "helloin";

            ResultContainer<string> result = logic.Play(word);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Status, Is.EqualTo(StatusEnum.PlayerWin));
            Assert.That(result.Element, Is.EqualTo(word + "g"));
        }
    }
}