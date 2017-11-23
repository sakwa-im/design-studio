using configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;

namespace sakwa
{
    public class UI_DataDefinition : IDataDefinitionImpl, ICustomTypeDescriptor
    {
        public UI_DataDefinition() : base()
        {
            DefineProperties();

            OnBeforeNameChange += BeforeNameChange;
        }

        private void BeforeNameChange(string newName)
        {
            if (newName != _Name)
            {
                string oldConfigName = coreSupport.NameNodeConfig(this);
                string newConfigName = coreSupport.NameNodeConfig(this, newName);

                coreSupport.RenameNodeConfig(oldConfigName, newConfigName);

            }
        }

        public void DefineProperties(UI_Constants.ePropertyCategories categories = UI_Constants.ePropertyCategories.None)
        {
            CategoriesToShow.Clear();

            CategoriesToShow.AddRange(new string[] { "Global settings" });

            if (ConfigurationRepository.IConfiguration.GetConfigurationValue(UI_Constants.DebugEnabled, false))
                CategoriesToShow.Add("Debugging");

            if ((categories & UI_Constants.ePropertyCategories.DataInformation) == UI_Constants.ePropertyCategories.DataInformation)
                CategoriesToShow.Add("Data Information");
        }
        protected override IBaseNode Clone()
        {
            UI_DataDefinition result = new UI_DataDefinition();
            result.Tree = Tree;
            result.Name = Name;
            result.Description = _Description;

            (result as IDataDefinition).DataDefinition = _DataDefinition;

            foreach (IDataDefinitionExport export in Exports)
                result.Exports.Add(export.Clone());

            FinalizeClone(result);

            return result;

        }

        #region Global settings
        [CategoryAttribute("Global settings")]
        public string Name
        {
            get { return _Name; }
            set { IBaseNodeInterface.Name = value; }
        }
        [CategoryAttribute("Global settings")]
        [Editor(typeof(MultiLineTextEditor), typeof(UITypeEditor))]
        public string Description
        {
            get { return _Description; }
            set { IBaseNodeInterface.Description = value; }
        }
        #endregion
        #region Debug section
        [CategoryAttribute("Debugging")]
        public string ClassType { get { return this.GetType().ToString(); } }
        [CategoryAttribute("Debugging")]
        public string ParentNode { get { return Parent != null ? Parent.NodeType.ToString() : ""; } }
        [CategoryAttribute("Debugging")]
        public string Ref { get { return Reference; } }
        #endregion
        #region Data Information
        [CategoryAttribute("Data Information")]
        [TypeConverter(typeof(DataSourceFactoryConverter))]
        public string DataDefinition
        {
            get
            {
                return DataDefinition;
            }
            set
            {
                DataDefinition = value;
            }
        }

        [CategoryAttribute("Data Information")]
        [Editor(typeof(DataConnectionPropertiesEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string Properties
        {
            get
            {
                json = ""; // PropertiesAsJson;
                return json;
            }
            set
            {
                string newValue = "";// PropertiesAsJson;
                if (newValue != json)
                {
                    json = newValue;
                    OnUpdated();

                }
            }
        }
        private string json = "";
        #endregion

        #region ICustomTypeDescriptor implementation
        private List<string> CategoriesToShow = new List<string>();
        //Does the property filtering...
        private PropertyDescriptorCollection FilterProperties(PropertyDescriptorCollection pdc)
        {
            ArrayList toShow = new ArrayList();
            foreach (string s in CategoriesToShow)
                toShow.Add(s);

            PropertyDescriptorCollection adjustedProps =
            new PropertyDescriptorCollection(new PropertyDescriptor[] { });

            foreach (PropertyDescriptor pd in pdc)

                if (toShow.Contains(pd.Category))
                    adjustedProps.Add(pd);

            return adjustedProps;

        }

        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(this, true);

            return FilterProperties(pdc);

        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(this, attributes, true);

            return FilterProperties(pdc);
        }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }
        #endregion
    }
}
