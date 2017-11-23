using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sakwa
{
    public enum eInitializeMode {  None, SessionStart, SessionStartDefaultValue, CycleStart, CycleStartDefaultValue }
    public enum ePersistMode { Never, SessionComplete, CycleComplete, OnCommit }

    public interface IDataPersistence
    {
        eInitializeMode InitializeMode { get; set; }
        ePersistMode PersistMode { get; set; }
        List<string> DataConnections { get; }
        bool Persist(IPersistence persistence);
        bool Retrieve(IPersistence persistence);
        IDataPersistence Clone();


    }
}
