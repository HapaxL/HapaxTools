using System;
using System.Collections.Generic;
using System.Text;

namespace HapaxTools
{
    public class OrderedDictMultiset<SortT, KeyT, ValueT> where SortT : IComparable<SortT>
    {
        private class AATree
        {
            public SortT SortKey;
            public LinkedListNode<Dictionary<KeyT, ValueT>> DictValue;
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

            public AATree(SortT sortKey, LinkedListNode<Dictionary<KeyT, ValueT>> dictValue)
            {
                SortKey = sortKey;
                DictValue = dictValue;
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
        
        public LinkedList<Dictionary<KeyT, ValueT>> Items { get; private set; }
        public uint Count { get; private set; }

        public OrderedDictMultiset()
        {
            RootNode = AATree.Bottom;
            Items = new LinkedList<Dictionary<KeyT, ValueT>>();
            Count = 0;
        }

        /// <summary>
        /// Insert a new value. If the key already exists, overwrites the existing value with the new
        /// one or discards the new value and keeps the existing one, depending on the boolean param.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">inserted value</param>
        /// <param name="overwrite">if true, keeps the new value. If false, keeps the old value</param>
        public void Insert(SortT sortKey, KeyT key, ValueT value)
        {
            Insert(ref RootNode, sortKey, key, value, AATree.Bottom, false);
        }

        public void Remove(SortT sortKey, KeyT key)
        {
            AATree deleted = AATree.Bottom;
            Remove(ref RootNode, sortKey, key, ref deleted);
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
        
        private void Insert(ref AATree treeNode, SortT sortKey, KeyT key, ValueT value, AATree from, bool wentRight)
        {
            if (treeNode == AATree.Bottom)
            {
                var dict = new Dictionary<KeyT, ValueT>();
                dict.Add(key, value);
                LinkedListNode<Dictionary<KeyT, ValueT>> listNode;
                if (from == AATree.Bottom)
                {
                    listNode = Items.AddFirst(dict);
                }
                else
                {
                    if (wentRight)
                    {
                        listNode = Items.AddAfter(from.DictValue, dict);
                    }
                    else
                    {
                        listNode = Items.AddBefore(from.DictValue, dict);
                    }
                }
                treeNode = new AATree(sortKey, listNode);
                return;
            }

            var comparison = sortKey.CompareTo(treeNode.SortKey);

            if (comparison < 0)
            {
                Insert(ref treeNode.Left, sortKey, key, value, treeNode, false);
            }
            else if (comparison > 0)
            {
                Insert(ref treeNode.Right, sortKey, key, value, treeNode, true);
            }
            else
            {
                treeNode.DictValue.Value.Add(key, value);
                ++Count;
            }

            Skew(ref treeNode);
            Split(ref treeNode);
        }

        private void Remove(ref AATree treeNode, SortT sortKey, KeyT key, ref AATree deleted)
        {
            if (treeNode == AATree.Bottom)
            {
                return;
            }

            var comparison = sortKey.CompareTo(treeNode.SortKey);

            if (comparison < 0)
            {
                Remove(ref treeNode.Left, sortKey, key, ref deleted);
            }
            else
            {
                if (comparison == 0)
                {
                    treeNode.DictValue.Value.Remove(key);
                    --Count;
                    if (treeNode.DictValue.Value.Count == 0)
                    {
                        deleted = treeNode;
                    }
                    else
                    {
                        return;
                    }
                }
                Remove(ref treeNode.Right, sortKey, key, ref deleted);
            }
            
            if (deleted != AATree.Bottom)
            {
                Items.Remove(deleted.DictValue);
                deleted.SortKey = treeNode.SortKey;
                deleted.DictValue = treeNode.DictValue;
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
