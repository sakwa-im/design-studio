using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sakwa
{
    public class IValueEntityImpl : IValueEntity
    {
        public IValueEntityImpl() { }
        public IValueEntityImpl(List<string> newValue, string newReference, int sequence)
        {
            Value = newValue;
            Reference = newReference;
            Sequence = sequence;
        }

        string IValueEntity.Value
        {
            get { return ""; }// Value; }
            set { }
        }
        string IValueEntity.Reference
        {
            get { return Reference; }
            set { Reference = value; }
        }
        int IValueEntity.Sequence
        {
            get { return Sequence; }
            set { Sequence = value; }
        }

        IValueEntity IValueEntity.Clone()
        {
            return new IValueEntityImpl(Value, Reference, Sequence);
        }

        protected List<string> Value = new List<string>();
        protected string Reference = "";
        protected int Sequence = 0;

    }
}
