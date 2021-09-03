using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HapaxTools
{
    public class Multiset<T> : ICollection<T> where T : IComparable<T>
    {
        private class AATree
        {
            public T Value;
            public int Level;
            public int Count;
            public AATree Left;
            public AATree Right;

            public static readonly AATree Bottom = new AATree();

            private AATree()
            {
                Level = 0;
                Left = this;
                Right = this;
            }

            public AATree(T value)
            {
                Value = value;
                Count = 1;
                Level = 1;
                Left = Bottom;
                Right = Bottom;
            }

            public IEnumerator<T> GetEnumerator()
            {
                if (this == Bottom)
                {
                    yield break;
                }

                var leftEnumerator = Left.GetEnumerator();
                while (leftEnumerator.MoveNext())
                {
                    yield return leftEnumerator.Current;
                }

                for (int i = 0; i < Count; i++)
                {
                    yield return Value;
                }

                var rightEnumerator = Right.GetEnumerator();
                while (rightEnumerator.MoveNext())
                {
                    yield return rightEnumerator.Current;
                }
            }
        }

        public int Count { get; protected set; }
        public bool IsReadOnly { get; protected set; }
        private AATree RootNode;

        public Multiset()
        {
            RootNode = AATree.Bottom;
            IsReadOnly = false;
        }

        /// <summary>
        /// Insert a new value. If the key already exists, overwrites the existing value with the new
        /// one or discards the new value and keeps the existing one, depending on the boolean param.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">inserted value</param>
        /// <param name="overwrite">if true, keeps the new value. If false, keeps the old value</param>
        public void Add(T value)
        {
            Insert(ref RootNode, value);
            Count++;
        }

        public bool Remove(T value)
        {
            AATree deleted = AATree.Bottom;
            var ok = Delete(ref RootNode, value, false, ref deleted);
            if (ok) Count--;
            return ok;
        }

        public void Clear()
        {
            RootNode = AATree.Bottom;
            Count = 0;
        }

        public bool Contains(T value)
        {
            return Contains(ref RootNode, value);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("The array cannot be null.");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("The starting array index cannot be negative.");
            if (Count > array.Length - arrayIndex + 1)
                throw new ArgumentException("The destination array has fewer elements than the collection.");

            var enumerator = GetEnumerator();
            int i = -1;
            while (enumerator.MoveNext())
            {
                i++;
                array[i + arrayIndex] = enumerator.Current;
            }
        }

        public bool Equals(Multiset<T> multiset)
        {
            if (Count != multiset.Count)
                return false;

            var enumerator1 = GetEnumerator();
            var enumerator2 = multiset.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                enumerator2.MoveNext();
                if (enumerator1.Current.CompareTo(enumerator2.Current) != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return RootNode.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return RootNode.GetEnumerator();
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
        
        private void Insert(ref AATree treeNode, T value)
        {
            if (treeNode == AATree.Bottom)
            {
                treeNode = new AATree(value);
                return;
            }

            var comparison = value.CompareTo(treeNode.Value);

            if (comparison < 0)
            {
                Insert(ref treeNode.Left, value);
            }
            else if (comparison > 0)
            {
                Insert(ref treeNode.Right, value);
            }
            else
            {
                treeNode.Count++;
            }

            Skew(ref treeNode);
            Split(ref treeNode);
        }

        private bool Delete(ref AATree treeNode, T value, bool ok, ref AATree deleted)
        {
            if (treeNode == AATree.Bottom)
            {
                return ok;
            }

            var comparison = value.CompareTo(treeNode.Value);

            if (comparison < 0)
            {
                ok = Delete(ref treeNode.Left, value, ok, ref deleted);
            }
            else
            {
                if (comparison == 0)
                {
                    treeNode.Count--;
                    ok = true;
                    if (treeNode.Count == 0)
                    {
                        deleted = treeNode;
                    }
                    else
                    {
                        return ok;
                    }
                }
                ok = Delete(ref treeNode.Right, value, ok, ref deleted);
            }
            
            if (deleted != AATree.Bottom)
            {
                deleted.Value = treeNode.Value;
                deleted.Count = treeNode.Count;
                deleted = AATree.Bottom;
                treeNode = treeNode.Right;
            }
            else if (treeNode.Left.Level < treeNode.Level - 1
                  || treeNode.Right.Level < treeNode.Level - 1)
            {
                treeNode.Level--;
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

            return ok;
        }

        private bool Contains(ref AATree treeNode, T value)
        {
            if (treeNode == AATree.Bottom)
            {
                return false;
            }

            var comparison = value.CompareTo(treeNode.Value);

            if (comparison < 0)
            {
                return Contains(ref treeNode.Left, value);
            }

            if (comparison > 0)
            {
                return Contains(ref treeNode.Right, value);
            }

            return true;
        }
    }
}
