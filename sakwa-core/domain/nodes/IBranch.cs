namespace sakwa
{
    public enum BranchEvaluation { Once, Always }

    public interface IBranch : IBaseNode
    {
        IDomainObject Domain { get; set; }
        IVariableDef Variable { get; set; }

        IVariable GetVariable(eVariableScope variableScope);
        IVariable lVal { get; set; }
        IVariable rVal { get; set; }
        string Expression { get; set; }

        BranchEvaluation BranchEvaluation { get; set; }

    }
}
