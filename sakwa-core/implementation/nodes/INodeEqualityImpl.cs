using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sakwa
{
    public class INodeEqualityImpl : INodeEquality
    {
        public INodeEqualityImpl(int equality, bool major, bool blocking, string remarks)
        {
            _Equlity = equality;
            _Major = major;
            _Blocking = blocking;
            _Remarks = remarks;

        }
        eNodeEquality INodeEquality.NodeEquality
        {
            get
            {
                return (eNodeEquality)_Equlity;
            }
        }

        eVariableEquality INodeEquality.VariableEquality
        {
            get
            {
                return (eVariableEquality)_Equlity;
            }
        }

        eDomainObjectEquality INodeEquality.DomainObjectEquality
        {
            get
            {
                return (eDomainObjectEquality)_Equlity;
            }
        }

        int INodeEquality.Equality { get { return _Equlity; } }

        bool INodeEquality.Major{ get { return _Major; } }

        bool INodeEquality.Blocking { get { return _Blocking; } }

        string INodeEquality.Remarks { get { return _Remarks; } }

        int _Equlity = 0;
        bool _Major = false;
        bool _Blocking = false;
        string _Remarks = "";

    }

    public class NodeEqualityCollection
    {
        public NodeEqualityCollection()
        {
        }
        public INodeEquality Add(eNodeEquality equality, bool major, bool blocking, string remarks)
        {
            INodeEquality result = new INodeEqualityImpl(Convert.ToInt32(equality), major, blocking, remarks);

            _Major |= major;
            _Blocking |= blocking;

            Items.Add(result);
            return result;

        }
        public INodeEquality Add(eVariableEquality equality, bool major, bool blocking, string remarks)
        {
            INodeEquality result = new INodeEqualityImpl(Convert.ToInt32(equality), major, blocking, remarks);

            _Major |= major;
            _Blocking |= blocking;

            Items.Add(result);
            return result;

        }
        public INodeEquality Add(eDomainObjectEquality equality, bool major, bool blocking, string remarks)
        {
            INodeEquality result = new INodeEqualityImpl(Convert.ToInt32(equality), major, blocking, remarks);

            _Major |= major;
            _Blocking |= blocking;

            Items.Add(result);
            return result;

        }
        public bool HasEqualityType(eNodeEquality equalityType)
        {
            foreach (INodeEquality item in Items)
                if ((item.NodeEquality & equalityType) != 0)
                    return true;

            return false;

        }
        public bool HasEqualityType(eVariableEquality equalityType)
        {
            foreach (INodeEquality item in Items)
                if ((item.VariableEquality & equalityType) != 0)
                    return true;

            return false;

        }
        public bool HasEqualityType(eDomainObjectEquality equalityType)
        {
            foreach (INodeEquality item in Items)
                if ((item.DomainObjectEquality & equalityType) != 0)
                    return true;

            return false;

        }

        public bool Major {  get { return _Major; } }
        public bool Blocking {  get { return _Blocking; } }

        public List<INodeEquality> Items = new List<INodeEquality>();

        protected bool _Blocking = false;
        protected bool _Major = false;
    }
}
