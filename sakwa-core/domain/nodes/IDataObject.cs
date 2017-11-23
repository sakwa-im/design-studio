using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sakwa
{
    public interface IDataObject : IBaseNode
    {
        IDataPersistence DataPersistence { get; }
        List<IMapping> DecisionModelDataSources { get; }
    }

    public interface IMapping
    {
        IBaseNode DecisionModelNode { get; set; }
        List<IDataDefinitionExport> ExportMaps { get; }
        List<string> ToStringArray();
        bool FromStringArray(List<string> input);

    }
}
