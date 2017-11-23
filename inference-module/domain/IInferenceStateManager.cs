using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sakwa
{
    public interface IInferenceStateManager
    {
        bool SetValue(string variableReference, string newValue);
        string GetValue(string variableReference);

        bool SetInferencePoint(string reference);

    }
}
