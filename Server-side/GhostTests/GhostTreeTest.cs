using Game;
using Ghost;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GhostTests
{
    public class GhostTreeTest
    {
        private readonly ITreeBuilder<string> gameTree;
        private readonly GhostTree ghostTree;
        private readonly Node<string> tree;

        public GhostTreeTest()
        {
            ghostTree = new GhostTree();
            gameTree = ghostTree;

            #region Tree
            tree = new Node<string>
            {
                Value = string.Empty,
                Children = new HashSet<Node<string>>
                {
                    new Node<string>
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
                                                            },
                                                            new Node<string>
                                                            {
                                                                Value = "helic",
                                                                Children = new HashSet<Node<string>>
                                                                {
                                                                     new Node<string>
                                                                     {
                                                                         Value = "helico",
                                                                         Children = new HashSet<Node<string>>
                                                                         {
                                                                              new Node<string>
                                                                              {
                                                                                  Value = "helicop",
                                                                                  Children = new HashSet<Node<string>>{
                                                                                      new Node<string>
                                                                                      {
                                                                                          Value = "helicopt",
                                                                                          Children = new HashSet<Node<string>>
                                                                                          {
                                                                                               new Node<string>
                                                                                               {
                                                                                                   Value = "helicopte",
                                                                                                   Children = new HashSet<Node<string>>
                                                                                                   {
                                                                                                        new Node<string>
                                                                                                        {
                                                                                                            Value = "helicopter",
                                                                                                            Terminal = true
                                                                                                        }
                                                                                                   }
                                                                                               }
                                                                                          }
                                                                                      }
                                                                                  }
                                                                              }
                                                                         }
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
                             }
                }
            };
            #endregion
        }


        [Test]
        public void CreateNullTest()
        {
            Node<string> node = gameTree.Create(null);

            Assert.That(node, Is.Not.Null);
            Assert.That(node.Children.Any(), Is.False);
        }

        [Test]
        public void CreateNullAndEmptiesTest()
        {
            IEnumerable<string> words = new List<string>
            {
                null, "  ", string.Empty
            };

            Node<string> node = gameTree.Create(words);

            Assert.That(node, Is.Not.Null);
            Assert.That(node.Children.Any(), Is.False);
        }


        [Test]
        public void CreateTest()
        {
            IEnumerable<string> words = new List<string>
            {
                "hello", "helium", "helicopter", "ghost", "screen"
            };

            Node<string> root = gameTree.Create(words);

            Node<string> node = null;

            foreach (string word in words)
            {
                node = ghostTree.Search(word, root);

                Assert.That(node, Is.Not.Null, $"Wanted {word} but was null");
                Assert.That(node.Terminal, Is.True, $"Wanted {word} but {node.Value} found. Not a terminal node.");
                Assert.That(node.Value, Is.EqualTo(word), $"Wanted {word} but {node.Value} found. They are not equals.");
            }
        }

        [Test]
        public void AddTest()
        {
            IEnumerable<string> words = new List<string>
            {
                "hello", "helium", "helicopter", "ghost", "screen"
            };

            Node<string> root = new Node<string>
            {
                Value = ""
            };

            ghostTree.Add(words, root);

            Node<string> node = null;

            foreach (string word in words)
            {
                node = ghostTree.Search(word, root);

                Assert.That(node, Is.Not.Null, $"Wanted {word} but was null");
                Assert.That(node.Terminal, Is.True, $"Wanted {word} but {node.Value} found. Not a terminal node.");
                Assert.That(node.Value, Is.EqualTo(word), $"Wanted {word} but {node.Value} found. They are not equals.");
            }
        }

        [Test]
        public void AddNullAndEmptiesTest()
        {
            IEnumerable<string> words = new List<string>
            {
                null, "  ", string.Empty
            };

            Node<string> root = new Node<string>
            {
                Value = ""
            };

            Node<string> node = ghostTree.Add(words, root);

            Assert.That(node, Is.Not.Null);
            Assert.That(node.Children.Any(), Is.False);
        }

        [Test]
        public void CreateNodeWhiteEspaceTest()
        {
            string word = "   ";

            Node<string> root = ghostTree.CreateNodes(word);

            Assert.That(root, Is.Null);
        }

        [Test]
        public void CreateNodeNullTest()
        {
            string word = null;

            Node<string> root = ghostTree.CreateNodes(word);

            Assert.That(root, Is.Null);
        }

        [Test]
        public void CreateNodeEmptyTest()
        {
            string word = "";

            Node<string> root = ghostTree.CreateNodes(word);

            Assert.That(root, Is.Null);
        }

        [Test]
        public void CreateNodeTest()
        {
            string word = "hello";

            Node<string> root = ghostTree.CreateNodes(word);

            Node<string> currentNode = root;

            Assert.That(currentNode.Value.Length, Is.EqualTo(1));

            int count = 1;

            while(currentNode != null && currentNode.Children.Any())
            {
                count++;
                currentNode = currentNode.Children.FirstOrDefault();
            }

            Node<string> lastNode = ghostTree.Search(word, root);

            Assert.That(lastNode.Terminal, Is.True);
            Assert.That(lastNode.Value, Is.EqualTo(word));

            Assert.That(word.Length, Is.EqualTo(count));
        }

        [Test]
        public void CreateNodeWithBaseTest()
        {
            string baseWord = "hel";
            string toCompleteWord = "ium";

            string word = baseWord + toCompleteWord;

            Node<string> root = ghostTree.CreateNodes(toCompleteWord, baseWord);

            Node<string> currentNode = root;
            int count = baseWord.Length + 1;

            // +1 because first node have the first letter of toCompleteWord 
            Assert.That(currentNode.Value.Length, Is.EqualTo(count));


            while (currentNode != null && currentNode.Children.Any())
            {
                count++;
                currentNode = currentNode.Children.FirstOrDefault();
            }

            Node<string> lastNode = ghostTree.Search(word, root);

            Assert.That(lastNode.Terminal, Is.True);
            Assert.That(lastNode.Value, Is.EqualTo(word));

            Assert.That(word.Length, Is.EqualTo(count));
        }

        [Test]
        public void SearchWithChildren()
        {
            string word = "helicopter";

            Node<string> node = ghostTree.Search(word, tree);

            Assert.That(node, Is.Not.Null, $"Wanted {word} but was null");
            Assert.That(node.Terminal, Is.True, $"Wanted {word} but {node.Value} found. Not a terminal node.");
            Assert.That(node.Value, Is.EqualTo(word), $"Wanted {word} but {node.Value} found. They are not equals.");
        }

        [Test]
        public void SearchTerminal()
        {
            string word = "hello";

            Node<string> node = ghostTree.Search(word, tree);

            Assert.That(node, Is.Not.Null);
            Assert.That(node.Value, Is.Not.Null);
            Assert.That(node.Value, Is.EqualTo(word));
        }

        [Test]
        public void SearchNotInTree()
        {
            string word = "a";

            Node<string> node = ghostTree.Search(word, tree);

            Assert.That(node, Is.Not.Null);
            Assert.That(node.Value, Is.Not.Null);
            Assert.That(node.Value, Is.Not.EqualTo(word));
        }

        [Test]
        public void SearchNotComplete()
        {
            string word = "heli";
            Node<string> node = ghostTree.Search(word, tree);

            Assert.That(node, Is.Not.Null);
            Assert.That(node.Value, Is.Not.Null);
            Assert.That(node.Value, Is.EqualTo(word));
        }

        [Test]
        public void SearchNull()
        {
            string word = "heli";
            Node<string> node = ghostTree.Search(word, null);

            Assert.That(node, Is.Null);
        }

        [Test]
        public void SearchWithPartialWordAsRoot()
        {
            string word = "helium";

            Node<string> partialTree = new Node<string>
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
            };

            Node<string> node = ghostTree.Search(word, partialTree);

            Assert.That(node, Is.Not.Null);
            Assert.That(node.Value, Is.Not.Null);
            Assert.That(node.Value, Is.EqualTo(word));
        }

    }
}
