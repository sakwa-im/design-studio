using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sakwa
{
    public class IInferenceStateManagerImpl : IInferenceStateManager
    {
        public IInferenceStateManagerImpl()
        {

        }

        string IInferenceStateManager.GetValue(string variableReference)
        {
            throw new NotImplementedException();
        }

        bool IInferenceStateManager.SetValue(string variableReference, string newValue)
        {
            throw new NotImplementedException();
        }

        bool IInferenceStateManager.SetInferencePoint(string reference)
        {
            throw new NotImplementedException();
        }

    }
}
