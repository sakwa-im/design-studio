using configuration;
using System.Drawing;

namespace sakwa
{
    public class UI_Constants : Constants
    {
        public enum ePropertyCategories { None, Debugging = 1, DataInformation = 2, DomainTemplate = 4 }

        public static string ModelName = "decision model";
        public static string TemplateName = "domain template";

        public static string ModelExtension = ".sdm";
        public static string TemplateExtension = ".sdt";

        public static string File_Save = "Save '{0}'";
        public static string File_SaveAs = "Save '{0}' as";

        public static eConfigurationSource NoConfigSource = eConfigurationSource.NonPersistent;

        public static Color TelfortColor = Color.FromArgb(255, 56, 57, 150);
        public static Color KpnColor = Color.FromArgb(255, 0, 146, 1);

        #region Treeview constants
        public static string NodesRemoveNode = "Element(s) from here";
        public static string NodesCantRemove = "Can't remove this element";
        public static string NodesRemoveVariable = "Variable";
        public static string NodesRemoveDomainObject = "DomainObject";
        public static string NodesRemoveAssignment = "Expression";
        public static string NodesRemoveTree = "Decision model";
        public static string NodesReloadTree = "Reload decision model";
        public static string NodesRemoveDataObject = "Data object";
        public static string NodesRemoveDataSource = "Data source";
        public static string NodesRemoveDataDefinition = "Data definition";
        #endregion

        public static string NodesRemoveTemplate = "Domain template";
        public static string NodesReloadTemplate = "Reload domain template";

        #region ConfigurationItems MainForm
        public static string MainFormState = "window-state";
        public static string MainFormSize = "size-main-form";
        public static string MainFormLocation = "location-main-form";
        public static string RecentFiles = "recent-files";
        public static string RecentProjects = "recent-projects";
        public static string RecentNode = "recent-node";
        public static string RecentDomainTemplates = "recent-domain-templates";
        #endregion

        #region ConfigurationItems MainForm
        public static string TemplateFormState = "template-window-state";
        public static string TemplateFormSize = "template-size-main-form";
        public static string TemplateFormLocation = "template-location-main-form";
        public static string TemplateRecentFiles = "template-recent-files";
        public static string TemplateRecentProjects = "template-recent-projects";
        public static string TemplateRecentNode = "template-recent-node";
        #endregion

        #region ConfigurationItems ConfigurationForm
        public static string ConfigurationFormSize = "size-config-form";
        public static string ConfigurationFormLocation = "location-config-form";

        public static string SakwaModelPath = "model-path";
        public static string SakwaTemplatePath = "template-path";
        public static string SakwaModelOnStart = "open-model-on-start";

        public static string ForeColor = "fore-color-";
        public static string ForeColor1 = "fore-color-1";
        public static string ForeColor2 = "fore-color-2";
        public static string ForeColor3 = "fore-color-3";
        public static string ForeColor4 = "fore-color-4";
        public static string ForeColor5 = "fore-color-5";
        public static string ForeColor6 = "fore-color-6";

        public static string BackColor = "back-color-";
        public static string BackColor1 = "back-color-1";
        public static string BackColor2 = "back-color-2";
        public static string BackColor3 = "back-color-3";
        public static string BackColor4 = "back-color-4";
        public static string BackColor5 = "back-color-5";
        public static string BackColor6 = "back-color-6";
        #endregion

        public static Font ModelFont = new Font("Arial", 8F);

        public static string SaveModelStatusLine = "Saved model graphics to: {0}";
        public static string ZoomPanHint = ", Click in the model and use scroll wheel to zoom, click and drag for pan.";

        #region ConfigurationItems ModelToolssForm
        public static string ModelToolsFormLocation = "location-main-form";
        #endregion


        public static int HorizontalSpacing = 200;
        public static int VerticalSpacing = 100;

        public static string ProgramFolder = "program-folder";
        public static string FullHelpPath = "help-folder-file";
        public static int StatusTextDuration = 3;

        public static string HelpIndex = "index.htm";
        public static string HelpChangeLog = "change-log.htm";
        public static string HelpSakwa = "sakwa.htm";

        public static string BulkImportText = "Import variable definitions";
        public static string BulkImportButton = "Import";
        public static string BulkImportFilter = "Descision models (*.sdm)|*.sdm";

        public static string BulkLinkText = "Link sub model";
        public static string BulkLinkButton = "Link";
        public static string BulkLinkFilter = "Descision models (*.sdm)|*.sdm";

        public static string BulkTemplateText = "Link domain template";
        public static string BulkTemplateButton = "Link";
        public static string BulkTemplateFilter = "Domain templates (*.sdt)|*.sdt";

        public static string EnumVariableFormSize = "size-enum-variable-form";
        public static string EnumVariableFormLocation = "location-enum-variable-form";
        public static string EnumVariableFormSplitterLocation = "location-enum-variable-form-splitter";

        public static string DataConnectionFormSize = "size-data-connection-form";
        public static string DataConnectionFormLocation = "location-data-connection-form";
        public static string DataConnectionFormSplitterLocation = "location-data-connection-form-splitter";

        public static string DataDefinitionFormSize = "size-data-definition-form";
        public static string DataDefinitionFormLocation = "location-data-definition-form";
        public static string DataDefinitionFormSplitterLocation = "location-data-definition-form-splitter";

        public static string ConnectionPropertiesFormSize = "size-connection-properties-form";
        public static string ConnectionPropertiesFormLocation = "location-connection-properties-form";

        public static string CommitEnumToolTip = "Apply the definition to the variable domain";
        public static string RevertEnumToolTip = "Close without altering the variable domain";

        public static string AddEnumToolTip = "Add this value(s) to the variable domain";
        public static string RemoveEnumToolTip = "Remove this value(s) from the variable domain";
        public static string NewEnumToolTip = "Add this value to the variable domain";
        public static string MoveUpEnumToolTip = "Move this value up in the variable domain";
        public static string MoveDownEnumToolTip = "Move this value down in the variable domain";

        public static string BulkAssignmentFormSize = "size-enum-variable-form";
        public static string BulkAssignmentFormLocation = "location-enum-variable-form";
        public static string BulkAssignmentFormSplitterLocation = "location-enum-variable-form-splitter";

        public static string BulkBranchFormSize = "size-bulk-branch-form";
        public static string BulkBranchFormLocation = "location-bulk-branch-form";
        public static string BulkBranchFormSplitterLocation = "location-bulk-branch-form-splitter";

        public static string CommitBulkAssignmentEnumToolTip = "Apply the assignment(s) to the model";
        public static string RevertBulkAssignmentToolTip = "Close without altering the model";

        public static string AddAssignmentToolTip = "Add this assignment(s) to the model";
        public static string RemoveAssignmentToolTip = "Remove this assignment(s) from the model";
        public static string MoveUpAssignmentToolTip = "Move this assignment up in the model";
        public static string MoveDownAssignmentToolTip = "Move this assignment down in the model";


        public static string BulkImportFormSize = "size-bulk-import-form";
        public static string BulkImportFormLocation = "location-bulk-import-form";
        public static string BulkImportFormSplitter1Location = "location-bulp-import-form-splitter-1";
        public static string BulkImportFormSplitter2Location = "location-bulp-import-form-splitter-2";

        public static string BulkImportBaseTypeError = "Base types are different";
        public static string BulkImportTypeError = "Variable types are different";
        public static string BulkImportDomainError = "The domains are different";
        public static string BulkImportMinError = "Min constraints are different";
        public static string BulkImportMaxError = "Max constratints are different";
        public static string BulkImportValueError = "The default values are different";

        public static string DebugEnabled = "debug-enabled";

        public static string StockMethods = "stock-methods";
        public static string InferenceConfig = "inference-config";
        public static string GlobalConnectionProperties = "global-connetion-properties";

        public enum eFormat {assign, equals }
        public static string BranchLabel(IBranch node)
        {
            return FormatBranchAssignment(node.lVal, node.rVal, eFormat.equals);
        }
        public static string AssignmentLabel(IExpression node)
        {
            return FormatBranchAssignment(node.lVal, node.rVal, eFormat.assign);
        }
        public static string FormatBranchAssignment(IVariable lVal, IVariable rVal, eFormat format)
        {
            bool hasLVal = !lVal.Empty;
            bool hasRVal = !rVal.Empty;

            if (!hasLVal && !hasRVal)
                return "";

            string result = "";

            result = FormatVariable(lVal, hasRVal);

            switch(format)
            {
                case eFormat.assign:
                    if (hasRVal)
                        result += lVal.Variable != null ? " := " : " (";
                    else
                        result += " := ";
                    break;

                case eFormat.equals:
                    if (!hasRVal)
                        result += " = ";
                    else
                        result += lVal.Variable != null ? " = " : " (";
                    break;
            }

            result += hasRVal ? FormatVariable(rVal) : lVal.Value;

            if (hasRVal && lVal.Variable == null)
                result += ")";

            return result;

        }
        public static string FormatVariable(IVariable variable, bool full = true)
        {
            string result = "";

            if (variable.Domain != null)
                result = variable.Domain.Name;

            if (variable.Variable != null)
                result += result != "" ? "." + variable.Variable.Name : variable.Variable.Name;

            if (full && variable.Value != "")
                result += result != "" ? "." + variable.Value : variable.Value;

            return result;

        }

        public static string DataDefinitionName(IBaseNode node)
        {
            return string.Format("{0} :: {1}", node.Parent.Name, node.Name);
        }

    } //class UI_Constants
}
