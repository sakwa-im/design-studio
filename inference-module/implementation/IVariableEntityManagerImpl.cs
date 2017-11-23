using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sakwa
{
    public class IVariableEntityManagerImpl : IVariableEntityManager
    {
        List<IVariableEntity> IVariableEntityManager.VariableEntities
        {
            get { return VariableEntities; }
        }

        bool IVariableEntityManager.SetVariable(string reference, string Value)
        {
            return false;
        }
        string IVariableEntityManager.GetVariable(string reference)
        {
            return null;
        }

        protected List<IVariableEntity> VariableEntities = new List<IVariableEntity>();

    }
}
