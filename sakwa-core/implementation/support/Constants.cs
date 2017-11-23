using configuration;

namespace sakwa
{
    public class Constants
    {
        public static eConfigurationSource ConfigurationSource = eConfigurationSource.UserAppData;
        
        #region IBaseNode persistence constants
        public static string Base_ClassType = typeof(IBaseNodeImpl).ToString();

        public static string BaseNode_ClassType = "class-type";
        public static string BaseNode_Type = "node-type";
        public static string BaseNode_Reference = "reference";
        public static string BaseNode_NodeCount = "count";
        public static string BaseNode_Name = "name";
        public static string BaseNode_Description = "description";

        public static string Expression = "expression";
        public static string Variable_Reference = "variable-reference";
        public static string Variable_Type = "variable-type";
        public static string Value = "value";

        public static string Root_Template = "domain-template";

        public static string Domain_Reference = "domain-reference";
        public static string Domain_Short_Name = "domain-short-name";
        public static string Domain_Sub_Model = "domain-sub-model";
        public static string Domain_Methods = "domain-methods";

        public static string Branch_Logic = "branch-logic";
        public static string Branch_Evaluation = "branch-evaluation";

        public static string CharVariable_Value = "char-variable-value";

        public static string IntVariable_Value = "int-variable-value";
        public static string IntVariable_MinValue = "int-variable-minvalue";
        public static string IntVariable_MaxValue = "int-variable-maxvalue";
        public static string IntVariable_StepValue = "int-variable-stepvalue";

        public static string EnumVariable_Value = "enum-variable";
        public static string EnumVariable_Choices = "enum-variable-branches";

        public static string DateVariable_Value = "date-variable";

        public static string DataNode_DataSourceFactory = "data-source-factory";
        public static string DataNode_DataSourceProperties = "data-source-properties";

        #endregion

        #region IDataPersistence constants
        public static string IDataPesistence_InitializeMode = "initialize-mode"; 
        public static string IDataPesistence_PersistMode = "persist-mode"; 
        public static string IDataPesistence_DataConnections = "data-connections"; 
        #endregion
        public static string VariableTreeName = "Variable definitions";
        public static string DataNodesTreeName = "Data objects";
        public static string DataSourcesTreeName = "Data sources";

        public static string PropertySettingName = "SettingsMode";
        public static string PropertySettingCaption = "Scope";
        public static string PropertySettingUseGlobal = "Use global";
        public static string PropertySettingUseThese = "Use these";

        public static string PropertyUseGlobal = "{$global}";
        public static string PropertyPromptUser = "{$prompt}";

        public static string DataDefinitionExport = "export";

        public static string ModelDatasource = "model-datasource";
        public static string ModelDatasourceMapping = "Mapping";

        public static string DataObjectEditorTitle = "Selected data object(s)";
        public static string DataSourceEditorTitle = "Selected data source(s)";
        public static string DataDefinitionEditorTitle = "Selected data definition(s)";


    }
}
