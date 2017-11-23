using System;
using System.Collections.Generic;

namespace sakwa
{
    public class IVariableEntityImpl : IVariableEntity
    {
        IVariableDef IVariableEntity.IVariableDef
        {
            get { return VariableDef; }
            set { VariableDef = value; }
        }

        string IVariableEntity.GetValue(string reference)
        {
            string result = null;
            if (ValueEntities.ContainsKey(reference))
            {
                List<IValueEntity> list = ValueEntities[reference];
                if (list.Count > 0)
                    result = list[list.Count - 1].Value;

            }

            return result;
        }

        bool IVariableEntity.SetValue(string newValue, string reference)
        {
            if (ValueEntities.ContainsKey(reference))
            {

            }
            else
            {
            }

            return true;

        }

        protected IVariableDef VariableDef = null;
        protected Dictionary<string, List<IValueEntity>> ValueEntities = new Dictionary<string, List<IValueEntity>>();

    }
}
