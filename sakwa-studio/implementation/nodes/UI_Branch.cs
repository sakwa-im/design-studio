using configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;

namespace sakwa
{
    public class UI_Branch : IBranchImpl, ICustomTypeDescriptor
    {
        public UI_Branch() : base()
        {
            CategoriesToShow.AddRange(new string[] { "Global settings", "Expression", "Variable selection", "Value selection" });

            if (ConfigurationRepository.IConfiguration.GetConfigurationValue(UI_Constants.DebugEnabled, false))
                CategoriesToShow.Add("Debugging");

        }

        protected override IBaseNode Clone()
        {
            UI_Branch result = new UI_Branch();
            result.Tree = Tree;
            result.Description = _Description;

            result.Domain = Domain;
            result.Variable = Variable;
            result.Value = Value;

            result.lVal = lVal.Clone();
            result.rVal = rVal.Clone();

            FinalizeClone(result);

            return result;

        }
        protected override string GetName()
        {
            string result = UI_Constants.BranchLabel(this);
            return result != "" ? result : _Name;

        }

        [CategoryAttribute("Global settings")]
        [Editor(typeof(MultiLineTextEditor), typeof(UITypeEditor))]
        public string Description
        {
            get { return _Description; }
            set { IBaseNodeInterface.Description = value; }
        }

        [CategoryAttribute("Global settings")]
        [DisplayName("Evaluation mode")]
        [Description("This value defines how often the branch condition will be tested")]
        public BranchEvaluation EvaluationMode
        {
            get { return _BranchEvaluation; }
            set
            {
                if(value != _BranchEvaluation)
                {
                    _BranchEvaluation = value;

                    OnUpdated();
                }
            }
        }
        #region Expression selection
        [CategoryAttribute("Expression")]
        [EditorAttribute(typeof(ExpressionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        //[DisplayName("Expression")]
        [Description("Expresion that is tested by this branch")]
        public string Expression
        {
            get { return _Expression; }
            set
            {
                IBranchInterface.Expression = value;
            }
        }
        #endregion

        #region Variable selection
        [CategoryAttribute("Variable selection")]
        [TypeConverter(typeof(DefaultValueConverter))]
        [DisplayName("Value")]
        [Description("This is the value that is compared with the variable")]
        public string Value
        {
            get { return lVal.Value; }
            set
            {
                if (lVal.Value != value)
                {
                    lVal.Value = value;

                    OnUpdated();

                }
            }
        }

        [CategoryAttribute("Variable selection")]
        [TypeConverter(typeof(VariableDefConverter))]
        public IVariableDef Variable
        {
            get { return lVal.Variable; }
            set
            {
                if (lVal.Variable != value)
                {
                    lVal.Variable = value;
                    if (lVal.Variable != null)
                        lVal.Value = lVal.Variable.Value;

                    OnUpdated();

                }
            }
        }

        [CategoryAttribute("Variable selection")]
        [TypeConverter(typeof(DomainObjectConverter))]
        [DisplayName("Domain object")]
        public IDomainObject Domain
        {
            get { return lVal.Domain; }
            set
            {
                if (lVal.Domain != value)
                {
                    lVal.Domain = value;

                    lVal.Variable = null;
                    lVal.Value = lVal.Domain != null && lVal.Domain.Methods.Count > 0
                        ? lVal.Domain.Methods[0] : "";

                    OnUpdated();

                }
            }
        }
        #endregion

        #region Value selection
        [CategoryAttribute("Value selection")]
        [TypeConverter(typeof(DefaultValueRConverter))]
        [DisplayName("Value: value")]
        [Description("This is the value that is compared with the variable")]
        public string ValueValue
        {
            get { return rVal.Value; }
            set
            {
                if (rVal.Value != value)
                {
                    rVal.Value = value;

                    OnUpdated();

                }
            }
        }

        [CategoryAttribute("Value selection")]
        [TypeConverter(typeof(VariableDefRConverter))]
        [DisplayName("Value: variable")]
        public IVariableDef ValueVariable
        {
            get { return rVal.Variable; }
            set
            {
                if (rVal.Variable != value)
                {
                    rVal.Variable = value;
                    if (rVal.Variable != null)
                        rVal.Value = rVal.Variable.Value;

                    OnUpdated();

                }
            }
        }

        [CategoryAttribute("Value selection")]
        [TypeConverter(typeof(DomainObjectRConverter))]
        [DisplayName("Value: domain object")]
        public IDomainObject ValueDomain
        {
            get { return rVal.Domain; }
            set
            {
                if (rVal.Domain != value)
                {
                    rVal.Domain = value;

                    rVal.Variable = null;
                    rVal.Value = rVal.Domain != null && lVal.Domain.Methods.Count > 0
                        ? lVal.Domain.Methods[0] : "";

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
