using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sakwa
{
    public enum eFloatReason { NotSet, Login, Abort, Other }

    public interface IApplication
    {
        object SelectedObject { get; set; }
        IDecisionTree SelectedTree { get; set; }
        string StatusLine { get; set; }

        ucDecisionTree ucDecisionTree { get; }
        ucDecisionModel ucDecisionModel { get; }

        string GetResourceString(string keyValue);
        Bitmap GetResourceBitmap(string name);
        Stream GetResourceStream(string name);

        void ShowHelp(object sender, string page = "index");

        FloatingForm GetFloatingForm(eFloatReason reason, UserControl userControl);

        void LoadModel(string fullPath);
        string LastUsedFolder { get; set; }
        IDecisionTree NewDescisionTree(string rootName = "", PostNodeInitialize nodeInitialization = null);

        void ShowHideTemplateEditor(bool show, bool removeOnHide = false);

        List<IDataSourceFactory> DataSources { get; }
        IDataSourceManager DataSourceManager { get; }

    }
}
