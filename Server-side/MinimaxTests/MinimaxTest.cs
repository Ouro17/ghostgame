using Game;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MinimaxTests
{
    public class MinimaxTest
    {
        private readonly IDecisionMaker<string, int> decisionMaker;
        private readonly IHeuristic<string, int> heuristic;
        private readonly Node<string> tree;

        public MinimaxTest()
        {
            heuristic = new AHeuristic();
            decisionMaker = new Minimax<string, int>(heuristic);

            #region Tree
            tree = new Node<string>
            {
                Value = "h",
                Children = new HashSet<Node<string>>
                        {
                            new Node<string>
                            {
                                Value = "he",
                                Children = new HashSet<Node<string>>
                                {
                                    new Node<string>
                                    {
                                        Value = "hel",
                                        Children = new HashSet<Node<string>>
                                        {
                                            new Node<string>
                                            {
                                                Value = "hell",
                                                Children = new HashSet<Node<string>>
                                                {
                                                     new Node<string>
                                                     {
                                                        Value = "hello",
                                                        Terminal = true
                                                     },
                                                }
                                            },
                                            new Node<string>
                                            {
                                                Value = "heli",
                                                Children = new HashSet<Node<string>>
                                                {
                                                    new Node<string>
                                                    {
                                                        Value = "heliu",
                                                        Children = new HashSet<Node<string>>
                                                        {
                                                             new Node<string>
                                                             {
                                                                 Value = "helium",
                                                                 Terminal = true
                                                             }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            new Node<string>
                            {
                                Value = "ha",
                                Children = new HashSet<Node<string>>
                                {
                                     new Node<string>
                                     {
                                         Value = "hai",
                                         Children = new HashSet<Node<string>>
                                         {
                                             new Node<string>
                                             {
                                                 Value = "hair",
                                                 Terminal = true
                                             }
                                         }
                                     },
                                     new Node<string>
                                     {
                                         Value = "hal",
                                         Children = new HashSet<Node<string>>
                                         {
                                             new Node<string>
                                             {
                                                 Value = "halt",
                                                 Terminal = true
                                             }
                                         }
                                     }
                                }
                            }
                        }
                };
            #endregion
        }

        [Test]
        public void TestWin()
        {
            int turn = 2;
            int depth = 5;
            var treeAux = tree;

            IValueContainer<string, int> result = null;

            while (treeAux != null && !treeAux.Terminal)
            {
                result = decisionMaker.Evaluate(treeAux, depth, true, false);

                Assert.That(result.Value, Is.Not.Null);

                string choosen = result.Value.Substring(0, turn);

                treeAux = treeAux.Children.Where(e => e.Value == choosen).FirstOrDefault();

                turn++;
            }

            Assert.That(result.Value, Is.Not.Null);
        }

        [Test]
        public void TestForceWin()
        {
            int turn = 2;
            int depth = 5;
            var treeAux = tree;

            IValueContainer<string, int> result = null;

            while(treeAux != null && !treeAux.Terminal)
            {
                result = decisionMaker.Evaluate(treeAux, depth, true, turn < 4);

                Assert.That(result.Value, Is.Not.Null);

                string choosen = result.Value.Substring(0, turn);

                treeAux = treeAux.Children.Where(e => e.Value == choosen).FirstOrDefault();

                turn++;
            }

            Assert.That(result.Value, Is.Not.Null);
        }
    }
}