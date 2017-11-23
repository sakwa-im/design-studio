using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace sakwa
{
    public partial class ucProperties : UserControl
    {
        public static IApplication App = null;
        public ucProperties(IApplication app)
        {
            App = app;
            InitializeComponent();

        }

        public object SelectedObject
        {
            get { return propertyGrid.SelectedObject; }
            set
            {
                propertyGrid.SelectedObject = value;
            }
        }
        public new Font Font { get { return propertyGrid.Font; } set { propertyGrid.Font = value; } }

    }

    internal class ExpressionEditor : UITypeEditor
    {
        public ExpressionEditor() { }
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (svc != null)
            {
                IBaseNode baseNode = context.Instance as IBaseNode;
                if(baseNode != null)
                    using (ExpressionForm frm = new ExpressionForm(baseNode))
                    {
                        if (svc.ShowDialog(frm) == DialogResult.OK)
                        {
                            value = frm.Expression;
                        }
                    }
            }

            return value;

        }
    }

    internal class LinkDataDefinitionsEditor : UITypeEditor
    {
        public LinkDataDefinitionsEditor() { }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (svc != null)
            {
                IBaseNode baseNode = context.Instance as IBaseNode;
                if(baseNode != null)
                using (LinkDataDefinitionsForm frm = new LinkDataDefinitionsForm(baseNode))
                {
                    IDataPersistence dp = getDataPersistence(baseNode);
                    if (dp != null)
                    {
                        IDecisionTree tree = baseNode.Tree;
                        List<LinkDataDefinitionsForm.ListBoxItem> Elements = new List<LinkDataDefinitionsForm.ListBoxItem>();
                        foreach (string reference in dp.DataConnections)
                        {
                            IBaseNode node = tree.GetNodeByReference(reference);
                            if (node != null)
                                Elements.Add(new LinkDataDefinitionsForm.ListBoxItem(node, 0));

                        }
                        frm.Elements = Elements;

                        if (svc.ShowDialog(frm) == DialogResult.OK)
                        {
                            dp.DataConnections.Clear();
                                foreach(LinkDataDefinitionsForm.ListBoxItem lbi in frm.Elements)
                                    dp.DataConnections.Add(lbi.Node.Reference);

                        }
                    }
                }
            }

            return value;

        }

        IDataPersistence getDataPersistence(IBaseNode node)
        {
            if (node is IDataObject)
                return (node as IDataObject).DataPersistence;

            return null;

        }
    }
    internal class LinkNodesEditor : UITypeEditor
    {
        public LinkNodesEditor() { }

        protected eNodeType rootNode = eNodeType.DataObjects;
        protected string title = Constants.DataObjectEditorTitle;

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (svc != null)
            {
                IBaseNode baseNode = context.Instance as IBaseNode;
                if(baseNode != null)
                using (NodeLinkForm frm = new NodeLinkForm(baseNode, rootNode, title))
                {
                    IDataPersistence dp = getDataPersistence(baseNode);
                    if (dp != null)
                    {
                        IDecisionTree tree = baseNode.Tree;
                        List<string> Elements = new List<string>();
                        foreach (string reference in dp.DataConnections)
                        {
                            IBaseNode node = tree.GetNodeByReference(reference);
                            if (node != null)
                                Elements.Add(node.Name);

                        }
                        frm.Elements = Elements;

                        if (svc.ShowDialog(frm) == DialogResult.OK)
                        {
                            List<string> result = new List<string>();
                            IBaseNode nodes = tree.RootNode.GetNode(rootNode);
                            foreach (string name in frm.Elements)
                            {
                                IBaseNode dc = nodes.GetNode(name);
                                if (dc != null)
                                    result.Add(dc.Reference);

                            }

                            value = result.ToArray();

                        }
                    }
                }
            }

            return value;

        }

        IDataPersistence getDataPersistence(IBaseNode node)
        {
            if (node is IDomainObject)
                return (node as IDomainObject).DataPersistence;

            if (node is IVariableDef)
                return (node as IVariableDef).DataPersistence;

            return null;

        }
    }

    internal class DataObjectsEditor : LinkNodesEditor
    {
        public DataObjectsEditor()
        {
            rootNode = eNodeType.DataObjects;
            title = Constants.DataObjectEditorTitle;
        }
    }

    internal class DataSourcesEditor : LinkNodesEditor
    {
        public DataSourcesEditor()
        {
            rootNode = eNodeType.DataSources;
            title = Constants.DataSourceEditorTitle;
        }
    }

    internal class DataConnectionEditor : UITypeEditor
    {
        public DataConnectionEditor() { }
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (svc != null)
            {
                IBaseNode baseNode = context.Instance as IBaseNode;
                if(baseNode != null)
                using (DataConnectionForm frm = new DataConnectionForm(baseNode))
                {
                    IDataPersistence dp = getDataPersistence(baseNode);
                    if(dp != null)
                    {
                        IDecisionTree tree = baseNode.Tree;
                        List<string> Elements = new List<string>();
                        foreach(string reference in dp.DataConnections)
                        {
                            IBaseNode node = tree.GetNodeByReference(reference);
                            if (node != null)
                                Elements.Add(node.Name);

                        }
                        frm.Elements = Elements;

                        if (svc.ShowDialog(frm) == DialogResult.OK)
                        {
                            List<string> result = new List<string>();
                            IBaseNode dataNodes = tree.RootNode.GetNode(eNodeType.DataObjects);
                            foreach (string name in frm.Elements)
                            {
                                IBaseNode dc = dataNodes.GetNode(name);
                                if (dc != null)
                                    result.Add(dc.Reference);

                            }

                            value = result.ToArray();

                        }
                    }
                }
            }

            return value;

        }

        IDataPersistence getDataPersistence(IBaseNode node)
        {
            if (node is IDomainObject)
                return (node as IDomainObject).DataPersistence;

            if (node is IVariableDef)
                return (node as IVariableDef).DataPersistence;

            return null;

        }
    }

    public class DataSourceFactoryConverter : System.ComponentModel.StringConverter
    {
        public DataSourceFactoryConverter() { }

        public override bool GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return true;
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            IBaseNode baseNode = context.Instance as IBaseNode;
            return baseNode != null
                ? GetDataSourceByName(baseNode, value as string)
                : base.ConvertFrom(context, culture, value);
        }
        protected IDataSourceFactory GetDataSourceByName(IBaseNode node, string name)
        {
            IDataSource ds = node as IDataSource;
            if(ds != null && ds.DataSourceManager != null)
                foreach (IDataSourceFactory factory in ds.DataSourceManager.DataSourceFactories)
                    if (factory.Name == name)
                        return factory;

            return null;
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return value is IDataSourceFactory
                ? (value as IDataSourceFactory).Name
                : base.ConvertTo(context, culture, value, destinationType);
        }
        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<string> result = new List<string>();
            result.Add("");

            if (context.Instance != null)
            {
                IDataSource ds = context.Instance as IDataSource;
                if (ds != null && ds.DataSourceManager != null)
                    foreach (IDataSourceFactory factory in ds.DataSourceManager.DataSourceFactories)
                        result.Add(factory.Name);

            }

            return new StandardValuesCollection(result.ToArray());

        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
    }

    internal class DataConnectionPropertiesEditor : UITypeEditor
    {
        public DataConnectionPropertiesEditor() { }
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (svc != null)
            {
                IDataSource ds = context.Instance as IDataSource;
                using (ConnectionPropertiesForm frm = new ConnectionPropertiesForm(ds as IBaseNode))
                {
                    if (svc.ShowDialog(frm) == DialogResult.OK)
                    {
                        value = ds.PropertiesAsJson(false);
                    }
                }
            }

            return value;

        }
    }

    internal class VariableDomainEditor : UITypeEditor
    {
        public VariableDomainEditor() { }
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (svc != null)
            {
                IBaseNode baseNode = context.Instance as IBaseNode;
                using (EnumFromModelForm frm = new EnumFromModelForm(baseNode))
                {
                    List<string> Elements = new List<string>();
                    if (value != null)
                        Elements.AddRange(value as string[]);

                    frm.Elements = Elements;

                    if (svc.ShowDialog(frm) == DialogResult.OK)
                    {
                        value = frm.Elements.ToArray();
                    }
                }
            }

            return value;

        }
    }
    internal class MethodsDomainEditor : UITypeEditor
    {
        public MethodsDomainEditor() { }
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (svc != null)
            {
                IBaseNode baseNode = context.Instance as IBaseNode;
                using (MethodsFromModelForm frm = new MethodsFromModelForm(baseNode))
                {
                    List<string> Elements = new List<string>();
                    if (value != null)
                        Elements.AddRange(value as string[]);

                    frm.Elements = Elements;

                    if (svc.ShowDialog(frm) == DialogResult.OK)
                    {
                        value = frm.Elements.ToArray();
                    }
                }
            }

            return value;

        }
    }

    internal class DomainTemplateEditor : UITypeEditor
    {
        public DomainTemplateEditor() { }
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (svc != null)
            {
                IRootNode rootNode = context.Instance as IRootNode;
                if (rootNode != null)
                {
                    IBaseNode variables = rootNode.Tree.RootNode.GetNode(eNodeType.VariableDefinitions);
                    string domainTemplate = rootNode.DomainTemplate;
                    using (BulkImportForm frm = new BulkImportForm(variables, ucProperties.App, domainTemplate, eImportMode.Template))
                    {
                        if (svc.ShowDialog(frm) == DialogResult.OK)
                        {
                            if (frm.ModelFile != "")
                                rootNode.Tree.ImportVariables(frm.ImportVariables);

                            (rootNode as UI_RootNode).Propegate = false;
                            value = frm.ModelFile;

                        }
                    }
                }
            }

            return value;

        }
    }
    internal class ModelFileEditor : UITypeEditor
    {
        public ModelFileEditor() { }
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (svc != null)
            {
                IDomainObject domainObject = context.Instance as IDomainObject;
                if (domainObject != null)
                {
                    IBaseNode variables = domainObject.Tree.RootNode.GetNode(eNodeType.VariableDefinitions);
                    string modelName = (domainObject as IDomainObjectImpl).FullModelName;
                    using (BulkImportForm frm = new BulkImportForm(variables, ucProperties.App, modelName))
                    {
                        if (svc.ShowDialog(frm) == DialogResult.OK)
                        {
                            domainObject.Nodes.Clear();
                            if (frm.ModelFile != "")
                                foreach (IBaseNode baseNode in frm.ImportVariables)
                                    domainObject.AddNode(baseNode);

                            value = frm.ModelFile;

                        }
                    }
                }
            }

            return value;

        }
    }

    public class DefaultValueConverter : System.ComponentModel.StringConverter
    {
        public DefaultValueConverter() { }
        protected eVariableScope VariableScope = eVariableScope.lVal;

        public override bool GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return true;
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return base.ConvertFrom(context, culture, value);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<string> result = new List<string>();
            IBaseNode baseNode = context.Instance as IBaseNode;

            IVariableDef variable = VariableDef(baseNode);
            if (variable != null)
            {
                switch (variable.VariableType)
                {
                    case eVariableType.enumeration:
                        UI_EnumVariable enumVariable = variable as UI_EnumVariable;
                        result.AddRange(enumVariable.Elements);
                        break;

                    case eVariableType.boolean:
                        result.Add(false.ToString());
                        result.Add(true.ToString());
                        break;

                    case eVariableType.numeric:
                        UI_NumericVariable intVariable = variable as UI_NumericVariable;
                        if (intVariable.Sequence != 0)
                        {
                            if ((intVariable.Maximum - intVariable.Minimum) / intVariable.Sequence < 20)
                                for (decimal i = intVariable.Minimum; i <= intVariable.Maximum; i += intVariable.Sequence)
                                    result.Add(i.ToString());
                        }
                        break;

                    default:
                        result.Add(variable.Value);
                        break;
                }
            }
            else
            {
                IDomainObject domain = DomainObject(baseNode);
                if (domain != null)
                    result.AddRange(domain.Methods.ToArray());
                else
                    return base.GetStandardValues(context);
            }

            return new StandardValuesCollection(result.ToArray());

        }

        private IVariableDef VariableDef(IBaseNode node)
        {
            if (node != null)
            {
                if (node is IExpression)
                    return (node as IExpression).GetVariable(VariableScope).Variable;

                if (node is IBranch)
                    return (node as IBranch).GetVariable(VariableScope).Variable;

                if (node is IVariableDef)
                    return node as IVariableDef;

            }

            return null;

        }
        private IDomainObject DomainObject(IBaseNode node)
        {
            if(node != null)
            {
                if (node is IExpression)
                    return (node as IExpression).GetVariable(VariableScope).Domain;

                if (node is IBranch)
                    return (node as IBranch).GetVariable(VariableScope).Domain;

                if (node is IDomainObject)
                    return node as IDomainObject;

            }

            return null;

        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            bool result = false;
            IBaseNode baseNode = context.Instance as IBaseNode;
            IVariableDef variable = VariableDef(baseNode);
            if (variable != null)
                switch (variable.VariableType)
                {
                    case eVariableType.character:
                    case eVariableType.date:
                    case eVariableType.numeric:
                        result = false;
                        break;

                    default: result = true; break;
                }

            return result;

        }
    }
    public class DefaultValueRConverter : DefaultValueConverter
    {
        public DefaultValueRConverter()
        {
            VariableScope = eVariableScope.rVal;
        }
    }

    public class VariableDefConverter : StringConverter
    {
        public VariableDefConverter() {}

        protected eVariableScope VariableScope = eVariableScope.lVal;

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return true;
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            IBaseNode baseNode = context.Instance as IBaseNode;
            if (baseNode != null)
            {
                List<IBaseNode> nodes = new List<IBaseNode>();
                switch (baseNode.NodeType)
                {
                    case eNodeType.Expression:
                        nodes = (baseNode as IExpressionImpl).GetVarObjs(VariableScope);
                        break;

                    case eNodeType.Branch:
                        nodes = (baseNode as IBranchImpl).GetVarObjs(VariableScope);
                        break;
                }
                return GetNodeByName(nodes, value as string);
            }
            return base.ConvertFrom(context, culture, value);
        }
        protected IBaseNode GetNodeByName(List<IBaseNode> list, string name)
        {
            foreach (IBaseNode node in list)
                if (node is IVariableDef && node.Name == name)
                    return node;

            return null;
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return value is IBaseNode
                ? (value as IBaseNode).Name
                : base.ConvertTo(context, culture, value, destinationType);
        }
        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(System.ComponentModel.ITypeDescriptorContext context)
        {
            List<string> result = new List<string>();
            result.Add("");

            IBaseNode baseNode = context.Instance as IBaseNode;
            if (baseNode != null)
            {
                List<IBaseNode> nodes = new List<IBaseNode>();
                switch (baseNode.NodeType)
                {
                    case eNodeType.Expression:
                        nodes = (baseNode as IExpressionImpl).GetVarObjs(VariableScope);
                        break;
                    case eNodeType.Branch:
                        nodes = (baseNode as IBranchImpl).GetVarObjs(VariableScope);
                        break;
                }

                foreach (IBaseNode node in nodes)
                    if(node is IVariableDef)
                        result.Add(node.Name);

            }

            return new StandardValuesCollection(result.ToArray());

        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
    }

    public class VariableDefRConverter : VariableDefConverter
    {
        public VariableDefRConverter()
        {
            VariableScope = eVariableScope.rVal;
        }
    }
    public class DomainObjectConverter : System.ComponentModel.StringConverter
    {
        public DomainObjectConverter() { }
        protected eVariableScope VariableScope = eVariableScope.lVal;

        public override bool GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return true;
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            IBaseNode baseNode = context.Instance as IBaseNode;
            return baseNode != null
                ? GetNodeByName(baseNode.Tree.GetDomainObjects(), value as string)
                : base.ConvertFrom(context, culture, value);
        }
        protected IBaseNode GetNodeByName(List<IBaseNode> list, string name)
        {
            foreach (IBaseNode node in list)
                if (node is IDomainObject && node.Name == name)
                    return node;

            return null;
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return value is IBaseNode
                ? (value as IBaseNode).Name
                : base.ConvertTo(context, culture, value, destinationType);
        }
        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<string> result = new List<string>();
            result.Add("");

            IBaseNode baseNode = context.Instance as IBaseNode;
            if (baseNode != null)
            {
                List<IBaseNode> variables = baseNode.Tree.GetDomainObjects();
                foreach (IBaseNode node in variables)
                    if(node is IDomainObject)
                        result.Add(node.Name);

            }

            return new StandardValuesCollection(result.ToArray());

        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
    }

    public class DomainObjectRConverter : DomainObjectConverter
    {
        public DomainObjectRConverter()
        {
            VariableScope = eVariableScope.rVal;
        }
    }
    public class MultiLineTextEditor : UITypeEditor
    {
        private IWindowsFormsEditorService _editorService;

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            _editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            TextBox textEditorBox = new TextBox();
            textEditorBox.Multiline = true;
            textEditorBox.ScrollBars = ScrollBars.Vertical;
            textEditorBox.Width = 250;
            textEditorBox.Height = 150;
            textEditorBox.BorderStyle = BorderStyle.None;
            textEditorBox.AcceptsReturn = true;

            textEditorBox.Text = value as string;

            _editorService.DropDownControl(textEditorBox);

            return textEditorBox.Text;

        }

    }

}
