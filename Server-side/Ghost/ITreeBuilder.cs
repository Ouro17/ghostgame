using System;
using System.Collections.Generic;
using Game;

namespace Ghost
{
    public interface ITreeBuilder<T> where T : IComparable<T>
    {
        Node<T> Create(IEnumerable<T> data);
        Node<string> Search(string word, Node<string> root);
    }
}