using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    public class ScriptableList<T> : ScriptableObject
    {
        [SerializeField] private T[] _items;
        
        public List<T> Items =>  new (_items);

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _items.Length)
                    throw new IndexOutOfRangeException();
                return _items[index];
            }
        }
    }
}

