using System;
using System.Collections.Generic;
using WordSuggestion;

namespace WordSuggestion
{
    public class Trie
    {
        private Node<Char> rootNode;
        private List<String> suggestions;
        private const int maxSuggestions = 20;

        public Trie()
        {
            rootNode = new Node<Char>(new Char(), false, 0);
        }

        public void generateFromList(List<String> strings)
        {
            foreach (String s in strings)
            {
                addWord(rootNode, s);
            }
        }

        public void addWord(Node<Char> c, String s)
        {
            Node<Char> nextNode;
            Char value = Char.ToLower(s[0]);
            if (c.containsChildWithValue(value))
            {
                nextNode = c.Children[c.indexOfChildWithValue(value)];
            }
            else
            {
                bool term = (s.Length == 1);
                nextNode = new Node<Char>(value, term, 1);
                c.addChild(nextNode);
            }

            if (s.Length > 1)
            {
                addWord(nextNode, s.Substring(1));
            }
        }

        public void generateSuggestions(String s)
        {
            String lower = s.ToLower();   
            suggestions = new List<String>();
            Node<Char> startingNode = SearchTrie(lower);
            if (startingNode != rootNode)
            {
                if(startingNode.IsTerminal)
                {
                    suggestions.Add(lower);
                }
                recurseSuggestions(startingNode, lower);
            }
        }
    
        private void recurseSuggestions(Node<Char> c, String canidateSuggestion)
        {
            foreach (Node<Char> n in c.Children)
            {
                string toAdd = canidateSuggestion + n.Value;
                if (n.IsTerminal)
                {
                    Suggestions.Add(toAdd);
                }

                if (n.Children.Count > 0 && Suggestions.Count < maxSuggestions)
                {
                    recurseSuggestions(n, toAdd);
                }
            }
        }

        public Node<Char> SearchTrie(String s)
        {
            CharEnumerator c = s.GetEnumerator();
            Node<Char> currentNode = rootNode;

            while(c.MoveNext())
            {
                if (currentNode.containsChildWithValue(c.Current))
                {
                    int index = currentNode.indexOfChildWithValue(c.Current);
                    currentNode = currentNode.Children[index];
                }
                else
                {
                    return rootNode;
                }
            }

            return currentNode;
        }

        public void printTrie(string spacing = "", Node<Char> c = null)
        {
            if(c == null)
            {
                c = rootNode;
            }
            Console.WriteLine(spacing + c.Value);
            foreach (Node<Char> child in c.Children)
            {
                printTrie(spacing + " ", child);
            }
        }

        public Node<Char> RootNode { get => rootNode; set => rootNode = value; }
        public List<string> Suggestions { get => suggestions; set => suggestions = value; }
    }
}
