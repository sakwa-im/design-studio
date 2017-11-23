using System;
using System.Collections.Generic;

namespace SourceMeta
{
    public class MetaObject
    {
        public MetaObject(IDataSourceMetadata source)
        {
            this.source = source; 
        }

        public string type { get
            {
                return _type;
            }
        }

        public string value { get
            {
                return _value;
            }
            set
            {
                this._value = value;
            }
        }

        public virtual Type getChildType()
        {
            return null;
        }

        public MetaObject parent { get; set; }

        public bool mappable = true;

        public List<MetaObject> children
        {
            get
            {
                if (this._children.Count == 0)
                {
                    this.getChildren();
                }
                return this._children;
            }
            set
            {
                this._children = value;
            }
        }

        public virtual List<MetaInfoViewRow> MetaInfo
        {
            get
            {
                List<MetaInfoViewRow> list = new List<MetaInfoViewRow>();
                foreach (MetaObject obj in this.children)
                {
                    list.AddRange(obj.MetaInfo);
                }
                return list;
            }
        }



        protected IDataSourceMetadata source { get; set; }

        protected string _type = "";

        protected string _value = "";

        protected List<MetaObject> _children = new List<MetaObject>();

        protected virtual void getChildren() { }

        
    }
}
