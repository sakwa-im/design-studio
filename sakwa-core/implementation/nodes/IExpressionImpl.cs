using System.Collections.Generic;

namespace sakwa
{
    public class IExpressionImpl : IBaseNodeImpl, IExpression
    {
        public IExpressionImpl() : base()
        {
            NodeType = eNodeType.Expression;
        }
        public IExpressionImpl(string name, eNodeType nodeType) : base(name, eNodeType.Expression)
        {
            NodeType = eNodeType.Expression;
        }

        IVariableDef IExpression.Variable
        {
            get { return _Variable; } 
            set { _Variable = value; }
        }
        IDomainObject IExpression.Domain
        {
            get { return _Domain; } 
            set { _Domain = value; }
        }

        IDataSourceFactory IExpression.DataSource
        {
            get { return DataSource; } 
            set { DataSource = value; }
        }

        IVariable IExpression.GetVariable(eVariableScope variableScope)
        {
            return variableScope == eVariableScope.lVal
                ? lVal
                : rVal;
        }
        IVariable IExpression.lVal { get { return lVal; } set { lVal = value; } }
        IVariable IExpression.rVal { get { return rVal; } set { rVal = value; } }

        string IExpression.Expression
        {
            get { return _Expression; }
            set
            {
                if (_Expression != value)
                {
                    _Expression = value;
                    OnUpdated();
                }
            }
        }

        protected override bool Persist(IPersistence persistence, ref ePersistence phase)
        {
            base.Persist(persistence, ref phase);
            switch (phase)
            {
                case ePersistence.Initial:
                    //Persist lVal
                    if (lVal.Domain != null)
                        persistence.UpsertField(Constants.Domain_Reference, lVal.Domain.Reference);

                    if (lVal.Variable != null)
                        persistence.UpsertField(Constants.Variable_Reference, lVal.Variable.Reference);

                    persistence.UpsertField(Constants.Value, lVal.Value);

                    //Persist rVal
                    if (rVal.Domain != null)
                        persistence.UpsertField(Constants.Domain_Reference + "-r", rVal.Domain.Reference);

                    if (rVal.Variable != null)
                        persistence.UpsertField(Constants.Variable_Reference + "-r", rVal.Variable.Reference);

                    persistence.UpsertField(Constants.Value + "-r", rVal.Value);

                    persistence.UpsertField(Constants.Expression, _Expression);

                    break;
            }

            return true;

        }
        protected override bool Retrieve(IPersistence persistence, ref ePersistence phase)
        {
            base.Retrieve(persistence, ref phase);
            switch (phase)
            {
                case ePersistence.Initial:
                    //Retrieve lVal
                    string Reference = persistence.GetFieldValue(Constants.Domain_Reference, "");
                    lVal.Domain = Tree.GetDomainObjectByReference(Reference) as IDomainObject;

                    Reference = persistence.GetFieldValue(Constants.Variable_Reference, "");
                    if(lVal.Domain != null)
                    {
                        foreach (IBaseNode node in lVal.Domain.Nodes)
                            if (node.Reference == Reference)
                                lVal.Variable = node as IVariableDef;
                    }
                    else
                        lVal.Variable = Tree.GetVariableByReference(Reference) as IVariableDef;

                    lVal.Value = persistence.HasField(Constants.Value)
                        ? persistence.GetFieldValue(Constants.Value, "")
                        : _Name;

                    //Retieve rValue
                    Reference = persistence.GetFieldValue(Constants.Domain_Reference + "-r", "");
                    rVal.Domain = Tree.GetDomainObjectByReference(Reference) as IDomainObject;

                    Reference = persistence.GetFieldValue(Constants.Variable_Reference + "-r", "");
                    if(rVal.Domain != null)
                    {
                        foreach (IBaseNode node in rVal.Domain.Nodes)
                            if (node.Reference == Reference)
                                rVal.Variable = node as IVariableDef;
                    }
                    else
                        rVal.Variable = Tree.GetVariableByReference(Reference) as IVariableDef;

                    rVal.Value = persistence.GetFieldValue(Constants.Value + "-r", "");

                    _Expression = persistence.GetFieldValue(Constants.Expression, "");
                    break;

            }

            return true;

        }
        protected override void RemoveNode(IBaseNode nodeRemoved)
        {
            if (lVal.Variable != null && lVal.Variable.Reference == nodeRemoved.Reference)
            {
                lVal.Variable = null;
                OnUpdated();

            }

            base.RemoveNode(nodeRemoved);

        }
        string IBaseNode.Name
        {
            get { return GetName(); }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnUpdated();
                }
            }
        }

        protected override string GetName()
        {
            string result = lVal.Domain != null ? lVal.Domain.Name + "." : "";

            result += lVal.Variable != null
                ? string.Format("{0} := {1}", lVal.Value, lVal.Value)
                : lVal.Value;

            return result;

        }

        protected IDataSourceFactory DataSource = null;
        protected IVariableDef _Variable = null;
        protected IDomainObject _Domain = null;

        protected IVariable lVal = new IVariableImpl();
        protected IVariable rVal = new IVariableImpl();

        protected string _Expression = "";

        protected IExpression IExpressionInterface {  get { return this; } }

        public List<IBaseNode> GetVarObjs(eVariableScope variableScope)
        {
            IVariable iv = variableScope == eVariableScope.lVal ? lVal : rVal;

            return iv.Domain != null
                ? iv.Domain.Nodes
                : Tree.GetVariables();
        }

    }
}
