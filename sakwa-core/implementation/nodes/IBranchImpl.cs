using System;
using System.Collections.Generic;

namespace sakwa
{
    public class IBranchImpl : IBaseNodeImpl, IBranch
    {
        public IBranchImpl()
        {
            NodeType = eNodeType.Branch;
        }
        public IBranchImpl(string name, eNodeType nodeType) : base(name, eNodeType.Branch)
        {
            NodeType = eNodeType.Branch;
        }

        IVariableDef IBranch.Variable
        {
            get { return _Variable; }
            set { _Variable = value; }
        }
        IDomainObject IBranch.Domain
        {
            get { return _Domain; }
            set { _Domain = value; }
        }
        IVariable IBranch.GetVariable(eVariableScope variableScope)
        {
            return variableScope == eVariableScope.lVal
                ? lVal : rVal;
        }

        IVariable IBranch.lVal { get { return lVal; } set { lVal = value; } }
        IVariable IBranch.rVal { get { return rVal; } set { rVal = value; } }
        string IBranch.Expression
        {
            get { return _Expression; }
            set
            {
                if(_Expression != value)
                {
                    _Expression = value;
                    OnUpdated();
                }
            }
        }
        BranchEvaluation IBranch.BranchEvaluation { get { return _BranchEvaluation; } set { _BranchEvaluation = value; } }

        protected override bool Persist(IPersistence persistence, ref ePersistence phase)
        {
            base.Persist(persistence, ref phase);
            switch (phase)
            {
                case ePersistence.Initial:
                    //Persist lVal
                    if (lVal.Domain != null)
                    {
                        persistence.UpsertField(Constants.Domain_Reference, lVal.Domain.Reference);
                    }

                    if (lVal.Variable != null)
                    {
                        persistence.UpsertField(Constants.Variable_Reference, lVal.Variable.Reference);
                    }

                    persistence.UpsertField(Constants.Value, lVal.Value);

                    //Persist rVal
                    if (rVal.Domain != null)
                    {
                        persistence.UpsertField(Constants.Domain_Reference + "-r", rVal.Domain.Reference);
                    }

                    if (rVal.Variable != null)
                    {
                        persistence.UpsertField(Constants.Variable_Reference + "-r", rVal.Variable.Reference);
                    }

                    persistence.UpsertField(Constants.Value + "-r", rVal.Value);

                    persistence.UpsertField(Constants.Branch_Evaluation, _BranchEvaluation.ToString());
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
                    if (lVal.Domain != null)
                    {
                        foreach (IBaseNode node in lVal.Domain.Nodes)
                            if (node.Reference == Reference)
                                lVal.Variable = node as IVariableDef;
                    }
                    else
                        lVal.Variable = Tree.GetVariableByReference(Reference) as IVariableDef;

                    lVal.Value = persistence.GetFieldValue(Constants.Value, "");
                    
                    //Retrieve rVal
                    Reference = persistence.GetFieldValue(Constants.Domain_Reference + "-r", "");
                    rVal.Domain = Tree.GetDomainObjectByReference(Reference) as IDomainObject;

                    Reference = persistence.GetFieldValue(Constants.Variable_Reference + "-r", "");
                    if (rVal.Domain != null)
                    {
                        foreach (IBaseNode node in rVal.Domain.Nodes)
                            if (node.Reference == Reference)
                                rVal.Variable = node as IVariableDef;
                    }
                    else
                        rVal.Variable = Tree.GetVariableByReference(Reference) as IVariableDef;

                    rVal.Value = persistence.GetFieldValue(Constants.Value + "-r", "");

                    _BranchEvaluation = (BranchEvaluation)Enum.Parse(typeof(BranchEvaluation), 
                        persistence.GetFieldValue(Constants.Branch_Evaluation, BranchEvaluation.Once.ToString()));

                    _Expression = persistence.GetFieldValue(Constants.Expression, "");
                    break;
            }

            return true;

        }
        public List<IBaseNode> GetVarObjs(eVariableScope variableScope)
        {
            IVariable iv = variableScope == eVariableScope.lVal ? lVal : rVal;

            return iv.Domain != null
                ? iv.Domain.Nodes
                : Tree.GetVariables();
        }
        protected override string GetName()
        {
            string result = _Domain != null ? _Domain.Name + "." : "";

            result += _Variable != null
                ? string.Format("{0} = {1}", _Variable.Name, _Name)
                : _Name;

            return result;

        }

        protected IVariableDef _Variable = null;
        protected IDomainObject _Domain = null;

        protected IVariable lVal = new IVariableImpl();
        protected IVariable rVal = new IVariableImpl();

        protected string _Expression = "";

        protected BranchEvaluation _BranchEvaluation = BranchEvaluation.Once;
        protected IBranch IBranchInterface {  get { return this; } }

    }
}
