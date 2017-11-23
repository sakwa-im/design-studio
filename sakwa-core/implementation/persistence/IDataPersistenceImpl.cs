using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sakwa
{
    public class IDataPersistenceImpl : IDataPersistence
    {
        public IDataPersistenceImpl() { }

        eInitializeMode IDataPersistence.InitializeMode
        {
            get 
            {
                return InitializeMode;
            }

            set
            {
                InitializeMode = value;
            }
        }

        ePersistMode IDataPersistence.PersistMode
        {
            get
            {
                return PersistMode;
            }

            set
            {
                PersistMode = value;
            }
        }

        List<string> IDataPersistence.DataConnections
        {
            get
            {
                return DataConnections;
            }
        }

        bool IDataPersistence.Persist(IPersistence persistence)
        {
            persistence.UpsertField(Constants.IDataPesistence_InitializeMode, InitializeMode.ToString());
            persistence.UpsertField(Constants.IDataPesistence_PersistMode, PersistMode.ToString());
            persistence.UpsertFieldArray(Constants.IDataPesistence_DataConnections, DataConnections.ToArray());
            return true;
        }

        bool IDataPersistence.Retrieve(IPersistence persistence)
        {
            InitializeMode = (eInitializeMode)Enum.Parse(
                typeof(eInitializeMode), 
                persistence.GetFieldValue(Constants.IDataPesistence_InitializeMode, 
                    eInitializeMode.None.ToString()));

            PersistMode = (ePersistMode)Enum.Parse(
                typeof(ePersistMode), 
                persistence.GetFieldValue(Constants.IDataPesistence_PersistMode,
                    ePersistMode.Never.ToString()));

            string[] connections = persistence.GetFieldValues(Constants.IDataPesistence_DataConnections, "");
            DataConnections.Clear();
            DataConnections.AddRange(connections);

            return true;
        }
        IDataPersistence IDataPersistence.Clone()
        {
            IDataPersistence result = new IDataPersistenceImpl();
            result.InitializeMode = InitializeMode;
            result.PersistMode = PersistMode;

            List<string> dataConnections = new List<string>();
            foreach (string dc in DataConnections)
                result.DataConnections.Add(dc);

            return result;

        }


        protected eInitializeMode InitializeMode = eInitializeMode.None;
        protected ePersistMode PersistMode = ePersistMode.Never;
        protected List<string> DataConnections = new List<string>();

    }
}
