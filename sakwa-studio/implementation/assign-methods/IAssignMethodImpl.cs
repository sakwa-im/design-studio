using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sakwa
{
    public class IAssignMethodImpl : IAssignMethod
    {
        string IAssignMethod.Name { get { return "Direct assignment"; } }

        string IAssignMethod.GetValue() { return _Value; }

        bool IAssignMethod.NextValue() { return NextValue(); }

        protected virtual bool NextValue()
        {
            return false;
        }

        [CategoryAttribute("Fixed value")]
        public string Value { get { return _Value; } set { _Value = value; } }

        protected string _Value = "";

    }
}
