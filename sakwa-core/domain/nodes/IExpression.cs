namespace sakwa
{
    public interface IExpression : IBaseNode
    {
        IDomainObject Domain { get; set; }
        IVariableDef Variable { get; set; }
        IDataSourceFactory DataSource { get; set; }

        IVariable GetVariable(eVariableScope variableScope);
        IVariable lVal { get; set; }
        IVariable rVal { get; set; }

        string Expression { get; set; }

    }
}
