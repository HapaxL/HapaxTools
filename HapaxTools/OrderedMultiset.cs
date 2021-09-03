using System;
using System.Collections.Generic;
using System.Text;

namespace HapaxTools
{
    public class OrderedMultiset<SortT, KeyT, ValueT>
        where SortT : IComparable<SortT>
        where KeyT : IComparable<KeyT>
    {
        private class AATree
        {
            public SortT SortKey;
            public KeyValuePair<KeyT, LinkedListNode<ValueT>> Element;
            public int Level;
            public AATree Left;
            public AATree Right;

            public static readonly AATree Bottom = new AATree();

            private AATree()
            {
                Level = 0;
                Left = this;
                Right = this;
            }

            public AATree(SortT sortKey, KeyValuePair<KeyT, LinkedListNode<ValueT>> dictValue)
            {
                SortKey = sortKey;
                Element = dictValue;
                Level = 1;
                Left = Bottom;
                Right = Bottom;
            }
            /*
            private static string NodeToString(AATree node)
            {
                if (node == Bottom)
                {
                    return "";
                }
                else
                {
                    var two = new System.Numerics.BigInteger(2);
                    int nbSpaces;
                    if (node.Level == 1)
                    {
                        nbSpaces = 0;
                    }
                    else
                    {
                        nbSpaces = (int)System.Numerics.BigInteger.Pow(two, node.Level - 2);
                    }

                    var sb = new StringBuilder();
                    sb.Append("(");
                    sb.Append(NodeToString(node.Left));
                    sb.Append(new string(' ', nbSpaces));
                    sb.Append(node.DictValue);
                    sb.Append(new string(' ', nbSpaces));
                    sb.Append(NodeToString(node.Right));
                    sb.Append(")");
                    return sb.ToString();
                }
            }

            public override string ToString()
            {
                var sb = new StringBuilder("AANode [");
                sb.Append(NodeToString(this));
                sb.Append("]");
                return sb.ToString();
            } */
        }

        private AATree RootNode;
        
        public LinkedList<ValueT> Items { get; private set; }
        // public uint Count { get; private set; } /* unnecessary since Items.Count already exists and is O(1) */

        public OrderedMultiset()
        {
            RootNode = AATree.Bottom;
            Items = new LinkedList<ValueT>();
            // Count = 0;
        }

        /// <summary>
        /// Insert a new value. If the key already exists, overwrites the existing value with the new
        /// one or discards the new value and keeps the existing one, depending on the boolean param.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">inserted value</param>
        /// <param name="overwrite">if true, keeps the new value. If false, keeps the old value</param>
        public void Add(SortT sortKey, KeyT key, ValueT value)
        {
            Add(ref RootNode, sortKey, key, value, AATree.Bottom, false);
        }

        public bool Remove(SortT sortKey, KeyT key)
        {
            AATree deleted = AATree.Bottom;
            return Remove(ref RootNode, sortKey, key, ref deleted);
        }

        private static void Skew(ref AATree node)
        {
            if (node.Level == node.Left.Level)
            {
                var left = node.Left;
                node.Left = left.Right;
                left.Right = node;
                node = left;
            }
        }

        private static void Split(ref AATree node)
        {
            if (node.Level == node.Right.Right.Level)
            {
                var right = node.Right;
                node.Right = right.Left;
                right.Left = node;
                node = right;
                node.Level++;
            }
        }
        
        private void Add(ref AATree treeNode, SortT sortKey, KeyT key, ValueT value, AATree from, bool wentRight)
        {
            if (treeNode == AATree.Bottom)
            {
                LinkedListNode<ValueT> listNode;
                if (from == AATree.Bottom)
                {
                    listNode = Items.AddFirst(value);
                    // ++Count;
                }
                else
                {
                    if (wentRight)
                    {
                        listNode = Items.AddAfter(from.Element.Value, value);
                    }
                    else
                    {
                        listNode = Items.AddBefore(from.Element.Value, value);
                    }
                }
                var kvp = new KeyValuePair<KeyT, LinkedListNode<ValueT>>(key, listNode);
                treeNode = new AATree(sortKey, kvp);
                return;
            }

            var sortComparison = sortKey.CompareTo(treeNode.SortKey);

            if (sortComparison < 0)
            {
                Add(ref treeNode.Left, sortKey, key, value, treeNode, false);
            }
            else if (sortComparison > 0)
            {
                Add(ref treeNode.Right, sortKey, key, value, treeNode, true);
            }
            else
            {
                var keyComparison = key.CompareTo(treeNode.Element.Key);

                if (keyComparison < 0)
                {
                    Add(ref treeNode.Left, sortKey, key, value, treeNode, false);
                }
                else if (keyComparison > 0)
                {
                    Add(ref treeNode.Right, sortKey, key, value, treeNode, true);
                }
                else
                {
                    throw new ArgumentException($"An item with the same keys \"{sortKey}\" and \"{key}\" has already been added.");
                }
            }

            Skew(ref treeNode);
            Split(ref treeNode);
        }

        private bool Remove(ref AATree treeNode, SortT sortKey, KeyT key, ref AATree deleted)
        {
            if (treeNode == AATree.Bottom)
            {
                return false;
            }

            bool res;

            var sortComparison = sortKey.CompareTo(treeNode.SortKey);

            if (sortComparison < 0)
            {
                res = Remove(ref treeNode.Left, sortKey, key, ref deleted);
            }
            else if (sortComparison > 0)
            {
                res = Remove(ref treeNode.Right, sortKey, key, ref deleted);
            }
            else
            {
                var keyComparison = key.CompareTo(treeNode.Element.Key);

                if (keyComparison < 0)
                {
                    res = Remove(ref treeNode.Left, sortKey, key, ref deleted);
                }
                else
                {
                    if (keyComparison == 0)
                    {
                        deleted = treeNode;
                        Remove(ref treeNode.Right, sortKey, key, ref deleted);
                        res = true;
                    }
                    else
                    {
                        res = Remove(ref treeNode.Right, sortKey, key, ref deleted);
                    }
                }
            }
            
            if (deleted != AATree.Bottom)
            {
                Items.Remove(deleted.Element.Value);
                // --Count;
                deleted.SortKey = treeNode.SortKey;
                deleted.Element = treeNode.Element;
                deleted = AATree.Bottom;
                treeNode = treeNode.Right;
            }
            else if (treeNode.Left.Level < treeNode.Level - 1
                  || treeNode.Right.Level < treeNode.Level - 1)
            {
                --treeNode.Level;
                if (treeNode.Right.Level > treeNode.Level)
                {
                    treeNode.Right.Level = treeNode.Level;
                }
                Skew(ref treeNode);
                Skew(ref treeNode.Right);
                Skew(ref treeNode.Right.Right);
                Split(ref treeNode);
                Split(ref treeNode.Right);
            }

            return res;
        }

        /*
        private static string NodeToString(AATree node)
        {
            if (node == AATree.Bottom)
            {
                return "";
            }
            else
            {
                var two = new System.Numerics.BigInteger(2);
                int nbSpaces;
                if (node.Level == 1)
                {
                    nbSpaces = 0;
                }
                else
                {
                    nbSpaces = (int) System.Numerics.BigInteger.Pow(two, node.Level - 2);
                }

                var sb = new StringBuilder();
                sb.Append("(");
                sb.Append(NodeToString(node.Left));
                sb.Append(new string(' ', nbSpaces));
                sb.Append(node.Value);
                sb.Append(new string(' ', nbSpaces));
                sb.Append(NodeToString(node.Right));
                sb.Append(")");
                return sb.ToString();
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder("AATree [");
            sb.Append(NodeToString(RootNode));
            sb.Append("]");
            return sb.ToString();
        }
        */
    }
}
