using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace configuration
{
    public interface IConfigurationItemObject<T>
    {
        T GetValue(T defaultValue);
        void SetValue(T newValue);

        void Attach(IConfigurationItem item);

    }
}
