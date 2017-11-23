using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace sakwa
{
    public class IDataObjectImpl : IBaseNodeImpl, IDataObject
    {
        IDataPersistence IDataObject.DataPersistence { get { return DataPersistence; } }
        List<IMapping> IDataObject.DecisionModelDataSources { get { return DecisionModelDataSources; } }

        protected override bool Persist(IPersistence persistence, ref ePersistence phase)
        {
            base.Persist(persistence, ref phase);
            switch (phase)
            {
                case ePersistence.Initial:
                    DataPersistence.Persist(persistence);

                    Dictionary<string, List<string>> toJson = new Dictionary<string, List<string>>();
                    foreach (IMapping mapping in DecisionModelDataSources)
                        toJson.Add(mapping.DecisionModelNode.Reference, mapping.ToStringArray());

                    string json = "";
                    if (toJson.Count > 0)
                        json = JsonConvert.SerializeObject(toJson);

                    persistence.UpsertField(Constants.ModelDatasource, json);

                    break;
            }

            return true;

        }
        protected override bool Retrieve(IPersistence persistence, ref ePersistence phase)
        {
            base.Retrieve(persistence, ref phase);
            switch (phase)
            {
                case ePersistence.Initial:
                    DataPersistence.Retrieve(persistence);

                    string json = persistence.GetFieldValue(Constants.ModelDatasource, "");
                    if(json != "")
                    {
                        Dictionary<string, List<string>> fromJson = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(json);
                        foreach(string key in fromJson.Keys)
                        {
                            IMapping mapping = new IMappingImpl();
                            mapping.DecisionModelNode = Tree.GetNodeByReference(key);
                            mapping.FromStringArray(fromJson[key]);

                            DecisionModelDataSources.Add(mapping);

                        }
                    }

                    break;
            }

            return true;

        }

        protected IDataPersistence DataPersistence = new IDataPersistenceImpl();
        protected IDataObject IDataObjectInterface {  get { return this as IDataObject; } }
        protected List<IMapping> DecisionModelDataSources = new List<IMapping>();

    }

    public class IMappingImpl : IMapping
    {
        public IMappingImpl() { }

        IBaseNode IMapping.DecisionModelNode
        {
            get { return DecisionModelNode; }
            set { DecisionModelNode = value; }
        }
        List<IDataDefinitionExport> IMapping.ExportMaps { get { return ExportMaps; } }

        List<string> IMapping.ToStringArray()
        {
            List<string> result = new List<string>();
            foreach (IDataDefinitionExport export in ExportMaps)
                result.Add(export.ToString());

            return result;

        }
        bool IMapping.FromStringArray(List<string> input)
        {
            foreach(string inp in input)
            {
                IDataDefinitionExport export = new IDataDefinitionExportImpl();
                export.FromString(inp);
                ExportMaps.Add(export);

            }

            return true;

        }

        protected IBaseNode DecisionModelNode = null;
        protected List<IDataDefinitionExport> ExportMaps = new List<IDataDefinitionExport>();

    }

}
