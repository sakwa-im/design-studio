using System.Collections.Generic;

namespace sakwa
{
    public interface IVariableEntityManager
    {
        List<IVariableEntity> VariableEntities { get; }

        bool SetVariable(string reference, string Value);
        string GetVariable(string reference);
    }
}
