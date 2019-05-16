using System;
using System.Collections.Generic;

namespace WordSuggestion
{
    public class Node<T> where T : IComparable
    {
        private T value;
        private List<Node<T>> children;
        private bool isTerminal;
        private int length;

        public Node(T val, bool t, int l)
        {
            Value = val;
            IsTerminal = t;
            Length = l;
            children = new List<Node<T>>();

        }

        public void addChild(T val, bool term, int length)
        {
            children.Add(new Node<T>(val, term, length));
        }

        public void addChild(Node<T> n)
        {
            children.Add(n);
        }

        public bool containsChildWithValue(T val)
        {
            foreach(Node<T> n in children)
            {
                if(n.Value.CompareTo(val) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public int indexOfChildWithValue(T val)
        {
            if(this.children.Count == 0)
            {
                return -1;
            }

            int index = 0;
            foreach (Node<T> n in children)
            {
                if (n.Value.CompareTo(val) == 0)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public List<Node<T>> Children { get => children; set => children = value; }
        public T Value { get => value; set => this.value = value; }
        public bool IsTerminal { get => isTerminal; set => isTerminal = value; }
        public int Length { get => length; set => length = value; }
    }
}
