using Game;
using Ghost;
using System;
using System.Collections.Generic;

namespace Business
{
    public class GhostLogic : ILogic<string>
    {
        private const int MIN_LOSS_LENGTH = 3;
        private readonly IEnumerable<string> dictionary;
        private readonly Node<string> root;
        private readonly ITreeBuilder<string> treeBuilder;
        private readonly IDecisionMaker<string, int> decisionMaker;

        public GhostLogic(ITreeBuilder<string> treeBuilder, IDecisionMaker<string, int> decisionMaker, IResource<IEnumerable<string>> resource)
        {
            this.treeBuilder = treeBuilder ?? throw new ArgumentException("It's necessary a tree builder to build a GhostLogic");
            this.decisionMaker = decisionMaker ?? throw new ArgumentException("It's necessary a decision algorithm to build a GhostLogic");

            if (resource == null)
            {
                throw new ArgumentException("It's necessary a resource to build a GhostLogic");
            }

            // Load the dictionary
            dictionary = resource.FecthResource();

            root = this.treeBuilder.Create(dictionary);
        }

        public ResultContainer<string> Play(string word)
        {
            StatusEnum status = StatusEnum.Continue;

            Node<string> node = treeBuilder.Search(word, root);

            // Player give a word that is terminal
            if (IsLoss(node, word))
            {
                status = StatusEnum.MachineWin;
            }
            else
            {
                // Evaluate from the node used by the player, stating playing as a machine.
                IValueContainer<string, int> value = decisionMaker.Evaluate(node, 10, false, word.Length <= MIN_LOSS_LENGTH);

                // If this happend, the player has lost because a terminal word was found but was less than MIN_LOSS_LENGTH character length.
                if (value.MinusInfinity)
                {
                    status = StatusEnum.MachineWin;
                }
                else
                {
                    // This is the word selected by the decision maker.
                    // Get the next letter to play.
                    word = value.Value.Substring(0, word.Length + 1);

                    // Can search from this node because value must be a word that extends the one of the node.
                    node = treeBuilder.Search(word, node);

                    if (IsLoss(node, word))
                    {
                        status = StatusEnum.PlayerWin;
                    }
                }
            }

            return new ResultContainer<string>
            {
                Element = word,
                Status = status
            };
        }

        private bool IsLoss(Node<string> node, string word)
        {
            return string.IsNullOrWhiteSpace(node.Value) || 
                   (node.Value.Length > MIN_LOSS_LENGTH && node.Terminal) ||
                   node.Value != word; // The node cant be distinct of the word returned
        }
    }
}
