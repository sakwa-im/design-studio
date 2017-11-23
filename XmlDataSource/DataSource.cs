using System;
using sakwa;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using System.Windows.Forms;

namespace XmlDataSource
{
    [Export(typeof(IDataSourceFactory))]
    public class XmlDataSource : IDataSourceFactory
    {
        public XmlDataSource() { }

        eDataSourceType IDataSourceFactory.DataSourceType
        {
            get { return eDataSourceType.XmlFile; }
        }
        List<IProperty> IDataSourceFactory.ConnectionProperties
        {
            get
            {
                List<IProperty> result = new List<IProperty>();

                IProperty pSettingsMode = new IPropertyStringImpl(
                    Constants.PropertySettingCaption, Constants.PropertySettingName, "pgDataSource",
                    new Constraint<string>(new string[]
                        { Constants.PropertySettingUseGlobal,
                          Constants.PropertySettingUseThese}),
                    eAttributeTarget.DataConnection,
                    eAttributeRequirement.User | eAttributeRequirement.Optional,
                    Constants.PropertySettingUseThese, "Settings");

                IPropertyUriFileOrPathImpl pfilePath = 
                    new IPropertyUriFileOrPathImpl(
                        "Xml File Path", "Filepath", "XmlDataSource", 
                        new Constraint<string>(ConstraintTest.Any),
                        eAttributeTarget.DataConnection,
                        eAttributeRequirement.User | eAttributeRequirement.Optional,
                        eAttributeDomainType.FileOrPath,
                        "", "File settings");

                pfilePath.Extension = ".xml";
                pfilePath.Filter = "Xml files (*.xml)|*.xml";

                IProperty pfileMode = new IPropertyStringImpl(
                    "Mode", "Filemode", "XmlDataSource",
                    new Constraint<string>(new string[] { "Read only", "Read write", "Write only"}), 
                    eAttributeTarget.DataConnection, 
                    eAttributeRequirement.User | eAttributeRequirement.Optional,
                    "Read only", "File settings");

                result.Add(pfilePath);
                result.Add(pfileMode);
                result.Add(pSettingsMode);

                return result;

            }
        }
        IDataSource IDataSourceFactory.GetDataSource(List<IProperty> properties)
        {
            return new XmlDataConnection(properties);
        }
        string IDataSourceFactory.Name
        {
            get { return "Xml Files"; }
        }
        SakwaUserControl IDataSourceFactory.GetEditor(IBaseNode node)
        {
            return new ucDataConnectionEditor(node);
        }

        private Dictionary<string, string> Parameters = new Dictionary<string, string>();

    }

    public class XmlDataConnection : IDataSource
    {
        IDataSourceFactory IDataSource.DataSourceFactory
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        List<IProperty> IDataSource.ConnectionProperties
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        string IDataSource.GetPropertyValue(string property)
        {
            return null;
        }

        string IDataSource.PropertiesAsJson(bool safeString)
        {
            throw new NotImplementedException();
        }

        IDataSourceManager IDataSource.DataSourceManager
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public XmlDataConnection(List<IProperty> properties)
        {
        }

        bool IDataSource.Open()
        {
            return false;
        }
        bool IDataSource.Close()
        {
            return false;
        }

        bool IDataSource.Persist()
        {
            return false;
        }

    }
}
