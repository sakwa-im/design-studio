using configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sakwa
{
    public class NodeSupport
    {
        public static IBaseNode TreeNodeAsBaseNode(TreeNode node)
        {
            return node.Tag != null ? node.Tag as IBaseNode : null;
        }
        public static TreeNode FindNode(TreeNodeCollection treeNodes, IBaseNode baseNode)
        {
            TreeNode result = null;
            foreach (TreeNode node in treeNodes)
            {
                if (node.Tag == baseNode)
                    return node;

                if (node.Nodes.Count > 0)
                    result = FindNode(node.Nodes, baseNode);

                if (result != null)
                    return result;

            }

            return null;

        }

    }

    public class SakwaSupport
    {
        public enum eInitialFolder { Model, Template}
        public static bool isEqual(List<string> list, string[] input)
        {
            if (input.Length != list.Count)
                return false;

            for (int i = 0; i < input.Length; i++)
                if (input[i] != list[i])
                    return false;

            return true;

        }

        public static string InitialFolder(IDecisionTree tree, eInitialFolder initialFolder = eInitialFolder.Model, string LastUsedFolder = "")
        {
            IConfiguration conf = ConfigurationRepository.IConfiguration;
            if (conf.GetConfigurationItem(UI_Constants.SakwaModelPath) == null)
                ConfigurationForm.DefineConfigurationItems();

            string ciKey = initialFolder == eInitialFolder.Model
                ? UI_Constants.SakwaModelPath
                : UI_Constants.SakwaTemplatePath;

            string result = LastUsedFolder == ""
                ? conf.GetConfigurationValue(ciKey, "")
                : Path.GetDirectoryName(LastUsedFolder);

            if (result == "")
                result = conf.GetConfigurationValue("UserAppDataPath", "");

            if (tree != null && tree.FullPath != "")
                result = Path.GetDirectoryName(tree.FullPath);

            return result;

        }
    }
}
