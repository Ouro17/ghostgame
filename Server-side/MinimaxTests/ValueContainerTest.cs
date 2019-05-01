using Game;
using NUnit.Framework;

namespace MinimaxTests
{
    public class ValueContainerTest
    {
        private readonly IValueContainer<string, int> one;
        private readonly IValueContainer<string, int> two;
        private readonly IValueContainer<string, int> plusInfinity;
        private readonly IValueContainer<string, int> minusInfinity;

        public ValueContainerTest()
        {
            one = new ValueContainer<string, int>
            {
                Points = 1
            };

            two = new ValueContainer<string, int>
            {
                Points = 2
            };

            plusInfinity = new ValueContainer<string, int>
            {
                PlusInfinity = true
            };

            minusInfinity = new ValueContainer<string, int>
            {
                MinusInfinity = true
            };
        }

        [Test]
        public void Assignations()
        {
            ValueContainer<string, int> test = new ValueContainer<string, int>
            {
                MinusInfinity = true
            };

            Assert.That(test.MinusInfinity, Is.True);
            Assert.That(test.PlusInfinity, Is.False);

            test.PlusInfinity = true;
            Assert.That(test.PlusInfinity, Is.True);
            Assert.That(test.MinusInfinity, Is.False);


            test.PlusInfinity = false;
            Assert.That(test.MinusInfinity, Is.False);
            Assert.That(test.PlusInfinity, Is.False);


            test.MinusInfinity = true;
            Assert.That(test.MinusInfinity, Is.True);
            Assert.That(test.PlusInfinity, Is.False);


            test.MinusInfinity = false;
            Assert.That(test.MinusInfinity, Is.False);
            Assert.That(test.PlusInfinity, Is.False);

            test.MinusInfinity = true;
            test.Points = 1;
            Assert.That(test.MinusInfinity, Is.False);
            Assert.That(test.PlusInfinity, Is.False);
        }


        [Test]
        public void LessThan()
        {
            object obj = new object();

            Assert.That(one, Is.LessThan(two));
            Assert.That(one, Is.LessThan(plusInfinity));

            Assert.That(two, Is.Not.LessThan(one));
            Assert.That(one, Is.Not.LessThan(one));
            Assert.That(one, Is.Not.LessThan(minusInfinity));

            Assert.That(minusInfinity, Is.LessThan(one));
        }

        [Test]
        public void GreaterThan()
        {
            object obj = new object();

            Assert.That(two, Is.GreaterThan(one));
            Assert.That(one, Is.GreaterThan(minusInfinity));

            Assert.That(one, Is.Not.GreaterThan(one));
            Assert.That(one, Is.Not.GreaterThan(two));
            Assert.That(one, Is.Not.GreaterThan(plusInfinity));

            Assert.That(plusInfinity, Is.GreaterThan(one));
        }

        [Test]
        public void ContainerEqual()
        {
            object obj = new object();

            Assert.That(one, Is.EqualTo(one));
            Assert.That(one, Is.GreaterThanOrEqualTo(one));
            Assert.That(one, Is.LessThanOrEqualTo(one));

            Assert.That(one, Is.Not.EqualTo(two));
            Assert.That(one, Is.Not.EqualTo(plusInfinity));
            Assert.That(one, Is.Not.EqualTo(minusInfinity));
            Assert.That(one, Is.Not.EqualTo(obj));
            Assert.That(one, Is.Not.EqualTo(null));
        }
    }
}
