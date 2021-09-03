using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiscUtil;

namespace HapaxTools
{
    public class Pondered<T, NumericType>
    {
        public struct Ponderation
        {
            public T Item { get; private set; }
            public NumericType Value { get; private set; }

            public Ponderation(T item, NumericType value)
            {
                Item = item;
                Value = value;
            }
        }

        public List<Ponderation> Items;

        public Pondered()
        {
            Items = new List<Ponderation>();
        }

        public void Add(T item, NumericType value)
        {
            Items.Add(new Ponderation(item, value));
        }

        public T Fetch(NumericType value)
        {
            NumericType reminder = value;
            int currentItemIndex = 0;
            NumericType currentItemPonderation = Items[0].Value;

            while(Operator<NumericType>.GreaterThanOrEqual(reminder, currentItemPonderation))
            {
                reminder = Operator<NumericType>.Subtract(reminder, currentItemPonderation);
                currentItemIndex = (currentItemIndex + 1) % Items.Count;
                currentItemPonderation = Items[currentItemIndex].Value;
            }

            return Items[currentItemIndex].Item;
        }

        public NumericType Size()
        {
            return Items.Aggregate(Operator<NumericType>.Zero, (acc, ponderedItem) => Operator<NumericType>.Add(acc, ponderedItem.Value));
        }
    }
}