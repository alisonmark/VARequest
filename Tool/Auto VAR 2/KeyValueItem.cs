using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auto_VAR
{
    public class KeyValueItem : IComparable<KeyValueItem>
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public KeyValueItem() { }
        public KeyValueItem(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public int CompareTo(KeyValueItem other)
        {
            return Key.CompareTo(other.Key);
        }
    }
}
