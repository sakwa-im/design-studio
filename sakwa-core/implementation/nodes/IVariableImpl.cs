using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sakwa
{
    public class IVariableImpl : IVariable
    {
        public IVariableImpl() { }

        eVariableScope IVariable.VariableScope
        {
            get { return VariableScope; }
            set { VariableScope = value; }
        }

        IDomainObject IVariable.Domain
        {
            get { return Domain; }
            set { Domain = value; }
        }

        IVariableDef IVariable.Variable
        {
            get { return Variable; }
            set { Variable = value; }
        }

        string IVariable.Value
        {
            get { return Value; }
            set { Value = value; }
        }

        IVariable IVariable.Clone()
        {
            IVariable result = new IVariableImpl();

            result.VariableScope = VariableScope;
            result.Domain = Domain;
            result.Variable = Variable;
            result.Value = Value;

            return result;
        }
        bool IVariable.Empty
        {
            get
            {
                return Domain == null && Variable == null && Value == "";
            }
        }

        protected eVariableScope VariableScope = eVariableScope.lVal;
        protected IDomainObject Domain = null;
        protected IVariableDef Variable = null;
        protected string Value = "";

    }
}
