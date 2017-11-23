using configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

namespace sakwa
{
    public class UI_DataSource : IDataSourceImpl, ICustomTypeDescriptor
    {
        public UI_DataSource() : base()
        {
            DefineProperties();

            OnBeforeNameChange += BeforeNameChange;
        }
        private void BeforeNameChange(string newName)
        {
            foreach (IBaseNode node in Nodes)
            {
                string oldConfigName = coreSupport.NameNodeConfig(_Name, node);
                string newConfigName = coreSupport.NameNodeConfig(newName, node);

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
            UI_DataSource result = new UI_DataSource();
            result.Tree = Tree;
            result.Name = Name;
            result.Description = _Description;

            //result.DataSource = DataSource;
            //(result as IDataNode).ConnectionProperties = ConnectionProperties;

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
        public IDataSourceFactory DataSourceFactory
        {
            get
            {
                return IDataSourceInterface.DataSourceFactory;
            }
            set
            {
                IDataSourceInterface.DataSourceFactory = value;
            }
        }

        [CategoryAttribute("Data Information")]
        [Editor(typeof(DataConnectionPropertiesEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string Properties
        {
            get
            {
                return IDataSourceInterface.PropertiesAsJson(true);
            }
            set { }
        }
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
