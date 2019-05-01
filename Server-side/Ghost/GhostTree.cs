using Game;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GhostTests")]

namespace Ghost
{
    public class GhostTree : ITreeBuilder<string>
    {
        /// <summary>
        /// Creates a tree for the given words
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public Node<string> Create(IEnumerable<string> words)
        {
            // Root is empty
            Node<string> root = new Node<string>
            {
                Value = string.Empty
            };

            if (words == null || !words.Any())
            {
                return root;
            }

            return Add(words, root);
        }

        /// <summary>
        /// Create a tree given a set of strings.
        /// </summary>
        /// <param name="words"></param>
        /// <returns>The root node of the tree</returns>
        internal Node<string> Add(IEnumerable<string> words, Node<string> root)
        {
            foreach (string word in words)
            {
                if (string.IsNullOrWhiteSpace(word))
                {
                    // Do not add it to the tree
                    continue;
                }

                // Search for the closest node that contains the most characters in the tree already.
                Node<string> startNode = Search(word, root);

                // Get the characters that is not currently in the tree to add them.
                // Starting at the length of the current value node to obtain only the character needed to add.
                // The number of character needed is the difference between the length of the word and the current added length.
                string addWord = word.Substring(startNode.Value.Length, word.Length - startNode.Value.Length);

                Node<string> childNode = CreateNodes(addWord, startNode.Value);

                if (childNode != null)
                {
                    startNode.Children.Add(childNode);
                }
            }

            return root;
        }

        /// <summary>
        /// Create the nodes for this word.
        /// </summary>
        /// <param name="word"></param>
        /// <returns>The root of the word</returns>
        internal Node<string> CreateNodes(string word, string baseWord = null)
        {
            if (string.IsNullOrWhiteSpace(word)) {
                return null;
            }

            if (string.IsNullOrWhiteSpace(baseWord))
            {
                baseWord = string.Empty;
            }

            // Empty root
            Node<string> root = new Node<string>
            {
                Value = baseWord
            };

            Node<string> currentNode = root;

            int maxLength = word.Length;

            for (int quantity = 1; quantity <= maxLength; quantity++)
            {
                Node<string> child = new Node<string>
                {
                    Value = baseWord + word.Substring(0, quantity)
                };

                currentNode.Children.Add(child);

                currentNode = child;
            }

            // The last node is terminal
            currentNode.Terminal = true;

            // Remove the empty root
            return root.Children.FirstOrDefault();
        }

        /// <summary>
        /// Search the closest match on the tree.
        /// </summary>
        /// <param name="word"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public Node<string> Search(string word, Node<string> root)
        {
            if (root == null)
            {
                return null;
            }

            Node<string> currentNode = root;
            Node<string> nextNode = null;
            string currentWord = string.Empty;
            
            // Starting at
            int quantity = root.Value.Length + 1;

            bool notInTree = false;

            while (!notInTree && quantity <= word.Length)
            {
                currentWord = word.Substring(0, quantity);

                // Using trygetvalue because children is a hashset.
                if(!currentNode.Children.TryGetValue(new Node<string> { Value = currentWord }, out nextNode))
                {
                    notInTree = true;
                }
                else
                {
                    quantity++;
                    currentNode = nextNode;
                }
            }

            return currentNode;
        }
    }
}
