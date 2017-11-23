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
    public class UI_DomainObject : IDomainObjectImpl, ICustomTypeDescriptor
    {
        public UI_DomainObject() : base()
        {
            IConfigurationItem stockMethods = ConfigurationRepository.IConfiguration.GetConfigurationItem(UI_Constants.StockMethods);
            if (stockMethods != null)
                foreach (IConfigurationItem it in stockMethods.ConfigurationItems)
                    _Methods.Add(it.Value);

            DefineProperties();

        }
        public void DefineProperties(UI_Constants.ePropertyCategories categories = UI_Constants.ePropertyCategories.None)
        {
            CategoriesToShow.Clear();
            CategoriesToShow.AddRange(new string[] { "Global settings", "Sub model", "Domain methods" });

            if (ConfigurationRepository.IConfiguration.GetConfigurationValue(UI_Constants.DebugEnabled, false))
                CategoriesToShow.Add("Debugging");

            if ((categories & UI_Constants.ePropertyCategories.DataInformation) == UI_Constants.ePropertyCategories.DataInformation)
                CategoriesToShow.Add("Data Information");
        }
        protected override IBaseNode Clone()
        {
            UI_DomainObject result = new UI_DomainObject();
            result.Tree = Tree;
            result.Name = Name;
            result.Description = _Description;

            result.ShortName = ShortName;
            result.Model = Model;
            result.Methods = new string[] { };
            result.Methods = Methods;

            result.DataPersistence = DataPersistence.Clone();

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
        [CategoryAttribute("Global settings")]
        public string ShortName
        {
            get { return _ShortName; }
            set
            {
                if (_ShortName != value)
                {
                    _ShortName = value;
                    OnUpdated();

                }
            }
        }

        #endregion
        #region Submodel
        [CategoryAttribute("Sub model")]
        [EditorAttribute(typeof(ModelFileEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string Model
        {
            get
            {
                return _Model != "" ? Path.GetFileName(_Model) : "";
            }
            set
            {
                if (_Model != value)
                {
                    _Model = value;
                    OnUpdatedAndRefresh();

                }
            }
        }
        #endregion
        #region Domain methods
        [CategoryAttribute("Domain methods")]
        [EditorAttribute(typeof(MethodsDomainEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Description("Defines the methods of the domain object\nClick on the elipse button to define the elements.")]
        public string[] Methods
        {
            get { return _Methods.ToArray(); }
            set
            {
                if (!SakwaSupport.isEqual(_Methods, value))
                {
                    _Methods.Clear();
                    _Methods.AddRange(value);

                    if (_Methods.Count > 1 && _Methods[_Methods.Count - 1] == "")
                        _Methods.RemoveAt(_Methods.Count - 1);

                    OnUpdated();

                }
            }
        }
        #endregion
        #region Debugging section
        [CategoryAttribute("Debugging")]
        public string ClassType { get { return this.GetType().ToString(); } }
        [CategoryAttribute("Debugging")]
        public string ParentNode { get { return Parent != null ? Parent.NodeType.ToString() : ""; } }
        [CategoryAttribute("Debugging")]
        public string Ref { get { return Reference; } }
        #endregion
        #region Data Information section
        [CategoryAttribute("Data Information")]
        [Description("Defines the initalization mode of the domain object")]
        public eInitializeMode InitializeMode
        {
            get { return DataPersistence.InitializeMode; }
            set
            {
                if (DataPersistence.InitializeMode != value)
                {
                    DataPersistence.InitializeMode = value;

                    OnUpdated();
                }
            }
        }

        [CategoryAttribute("Data Information")]
        [Description("Defines the persistence mode of the domain object")]
        public ePersistMode PersistMode
        {
            get { return DataPersistence.PersistMode; }
            set
            {
                if (DataPersistence.PersistMode != value)
                {
                    DataPersistence.PersistMode = value;

                    OnUpdated();
                }
            }
        }

        [CategoryAttribute("Data Information")]
        [EditorAttribute(typeof(DataObjectsEditor), typeof(UITypeEditor))]
        [Description("Defines the data object(s) of the domain object\nClick on the elipse button to define the elements.")]
        public string[] DataObjects
        {
            get
            {
                List<string> result = new List<string>();
                foreach(string reference in DataPersistence.DataConnections)
                {
                    IBaseNode node = Tree.GetNodeByReference(reference);
                    if (node != null)
                        result.Add(node.Name);
                }

                return result.ToArray();

            }
            set
            {
                if (!SakwaSupport.isEqual(DataPersistence.DataConnections, value))
                {
                    DataPersistence.DataConnections.Clear();
                    DataPersistence.DataConnections.AddRange(value);

                    if (DataPersistence.DataConnections.Count > 1 &&
                        DataPersistence.DataConnections[DataPersistence.DataConnections.Count - 1] == "")
                        DataPersistence.DataConnections.RemoveAt(DataPersistence.DataConnections.Count - 1);

                    OnUpdated();

                }
            }
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
