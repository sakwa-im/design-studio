using System.Collections.Generic;
using System.Windows.Forms;

namespace sakwa
{
    public enum eExportType {  Variable, Method, Undefined }
    public interface IDataDefinitionExport
    {
        string Name { get; set; }
        eExportType ExportType { get; set; }
        string Reference { get; set; }

        string ToString();
        void FromString(string input);
        bool Equals(IDataDefinitionExport compareWith);
        IDataDefinitionExport Clone();

    }
    public interface IDataDefinition
    {
        string DataDefinition { get; set; }
        SakwaUserControl Editor { get; }
        List<IDataDefinitionExport> Exports { get; }

    }
}
