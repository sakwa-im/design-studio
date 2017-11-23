using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace sakwa
{
    public class IDataDefinitionImpl : IBaseNodeImpl, IDataDefinition
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(IBaseNodeImpl));
        string IDataDefinition.DataDefinition
        {
            get
            {
                return _DataDefinition;
            }

            set
            {
                if(value != _DataDefinition)
                {
                    _DataDefinition = value;

                    OnUpdated();

                }
            }
        }
        SakwaUserControl IDataDefinition.Editor
        {
            get { return (Parent as IDataSource).DataSourceFactory.GetEditor(this); }
        }
        List<IDataDefinitionExport> IDataDefinition.Exports { get { return Exports; } }

        protected override bool Persist(IPersistence persistence, ref ePersistence phase)
        {
            base.Persist(persistence, ref phase);
            switch (phase)
            {
                case ePersistence.Initial:
                    List<string> data = new List<string>();
                    foreach(IDataDefinitionExport export in Exports)
                    {
                        string json = JsonConvert.SerializeObject(export);
                        data.Add(json);

                    }
                    persistence.UpsertFieldArray(Constants.DataDefinitionExport, data.ToArray<string>());

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
                    List<string> data = new List<string>();
                    data.AddRange(persistence.GetFieldValues(Constants.DataDefinitionExport, ""));
                    foreach(string json in data)
                        if(json != "")
                        {
                            IDataDefinitionExportImpl export = JsonConvert.DeserializeObject<IDataDefinitionExportImpl>(json);
                            Exports.Add(export);

                        }

                    break;
            }

            return true;

        }

        protected string _DataDefinition = "";
        protected List<IDataDefinitionExport> Exports = new List<IDataDefinitionExport>();
        protected IDataDefinition IDataDefinitionInterface { get { return this as IDataDefinition; } }

    }

    public class IDataDefinitionExportImpl : IDataDefinitionExport
    {
        public IDataDefinitionExportImpl() { }
        public IDataDefinitionExportImpl(eExportType exportType, string name)
        {
            ExportType = exportType;
            Name = name;
            Reference = Guid.NewGuid().ToString();

        }
        IDataDefinitionExport IDataDefinitionExport.Clone()
        {
            IDataDefinitionExport result = new IDataDefinitionExportImpl(ExportType, Name);
            result.Reference = Reference;
            return result;
        }

        bool IDataDefinitionExport.Equals(IDataDefinitionExport compareWith)
        {
            return 
                Reference == compareWith.Reference && 
                ExportType == compareWith.ExportType &&
                Name == compareWith.Name;

        }

        eExportType IDataDefinitionExport.ExportType { get { return ExportType; } set { ExportType = value; } }
        string IDataDefinitionExport.Name { get { return Name; } set { Name = value; } }
        string IDataDefinitionExport.Reference
        {
            get { return Reference; }
            set { Reference = value; }
        }
        string IDataDefinitionExport.ToString()
        {
            return string.Format("{0}\0{1}\0{2}", Reference, ExportType.ToString(), Name);
        }
        void IDataDefinitionExport.FromString(string input)
        {
            string[] elems = input.Split(new char[] { '\0' });

            Reference = elems[0];
            ExportType = (eExportType)Enum.Parse(typeof(eExportType), elems[1]);
            Name = elems[2];

        }

        public string JsonName {  get { return Name; } set { Name = value; } }
        public eExportType JsonExportType { get { return ExportType; } set { ExportType = value; } }
        public string jsonReference { get { return Reference; } set { Reference = value; } }

        protected string Name = "";
        protected eExportType ExportType = eExportType.Variable;
        protected string Reference = "";

    }
}
