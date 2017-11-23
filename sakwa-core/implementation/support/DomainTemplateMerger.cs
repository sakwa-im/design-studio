using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sakwa
{
    public static class DomainTemplateMerger
    {
        public static bool Merge(IBaseNode dest, IBaseNode src)
        {
            Dictionary<IBaseNode, string> destPaths = new Dictionary<IBaseNode, string>();
            Dictionary<IBaseNode, string> srcPaths = new Dictionary<IBaseNode, string>();
            Dictionary<IBaseNode, IBaseNode> replacedList = new Dictionary<IBaseNode, IBaseNode>();
            Dictionary<IBaseNode, string> addList = new Dictionary<IBaseNode, string>();
            Dictionary<string, string> IsReplacedList = new Dictionary<string, string>();

            foreach (IBaseNode node in dest.Nodes)
                recursion(node, destPaths);

            foreach (IBaseNode node in src.Nodes)
                recursion(node, srcPaths);

            IBaseNode tmpParent = dest;

            // Determine which node to add and which to replace
            foreach (KeyValuePair<IBaseNode, string> tmpSrcIbn in srcPaths)
            {
                // Generate a Add and Replace list
                IBaseNode tmpDestIbn =  FindNode(tmpSrcIbn.Key, tmpSrcIbn.Value, destPaths);
                if (tmpDestIbn != null)
                {
                    replacedList.Add(tmpDestIbn, tmpSrcIbn.Key);
                    //NodeEqualityCollection nec = tmpDestIbn.Compare(tmpSrcIbn.Key, eCompareMode.Full);
                    //if (nec.Items.Count > 0)
                    //{
                    //    if (nec.Items[0].Equality != Convert.ToInt16(eNodeEquality.equal))
                    //    {
                    //        replacedList.Add(tmpDestIbn, tmpSrcIbn.Key);
                    //    }
                    //}
                }
                else
                {
                    addList.Add(tmpSrcIbn.Key, ExtractParentPath(tmpSrcIbn.Value));
                }
            }

            // Replace nodes
            foreach (KeyValuePair<IBaseNode, IBaseNode> replacedListItem in replacedList)
            {
                if (replacedListItem.Key.Nodes.Count > 0)
                {
                    foreach (var _tmpNode in replacedListItem.Key.Nodes)
                    {
                        int _tmpIndexOld = replacedListItem.Key.Nodes.IndexOf(_tmpNode);
                        replacedListItem.Value.Nodes.Insert(_tmpIndexOld, _tmpNode);
                        _tmpNode.Parent = replacedListItem.Value;
                    }
                }
                IBaseNode _tmpIbn = FindInDest(dest, replacedListItem.Key);
                IBaseNode _tmpIbnParent = _tmpIbn.Parent;
                int _index = _tmpIbnParent.Nodes.IndexOf(replacedListItem.Key);

                _tmpIbnParent.Nodes.RemoveAt(_index);
                if (_tmpIbnParent.Nodes.Contains(replacedListItem.Value))
                    _tmpIbnParent.Nodes.Remove(replacedListItem.Value);

                _tmpIbnParent.Nodes.Insert(_index, replacedListItem.Value);
                replacedListItem.Value.Parent = _tmpIbnParent;
            }

            // Add missing nodes
            Dictionary<IBaseNode, string> _destPaths = new Dictionary<IBaseNode, string>();

            foreach (IBaseNode node in dest.Nodes)
                recursion(node, _destPaths);

            foreach (KeyValuePair<IBaseNode, string> addListItem in addList)
            {
                if (addListItem.Value == "")
                {
                    // Add to Variables 
                    dest.AddNode(addListItem.Key);
                }
                else
                {
                    // Add to Parent with path
                    IBaseNode _tmpParent = FindNodeWithPath(_destPaths, addListItem.Value);
                    _tmpParent.AddNode(addListItem.Key);
                }
            }

            return true;
        }

        private static string ExtractParentPath(string path)
        {
            string _value = "";
            string[] _delimitedString = path.Split('.');

            if (!(path == null || path == "") || (_delimitedString.Count() > 0))
            {
                // Array count() -2 to ensure the last array object is stripped from the concatination
                int _index = _delimitedString.Count() - 2;

                // Add dot seperator in case of path containing multiple parents
                if (_value != "")
                    _value += ".";

                for (int i = 0; i <= _index; i++)
                {
                    _value += _delimitedString[i];
                }
            }

            return _value;
        }

        private static IBaseNode FindNodeWithPath(Dictionary<IBaseNode, string> nodes, string path)
        {
            IBaseNode _value = null;
            foreach(KeyValuePair<IBaseNode, string> node in nodes)
            {
                if (node.Value == path)
                {
                    _value = node.Key;
                    break;
                }
            }

            return _value;
        }

        private static void recursion(IBaseNode nodes, Dictionary<IBaseNode, string> sourcePaths)
        {
            sourcePaths.Add(nodes, getPath(nodes, eNodeType.VariableDefinitions));
            foreach (IBaseNode node in nodes.Nodes)
                recursion(node, sourcePaths);
        }

        private static IBaseNode FindNode(IBaseNode ibn, string nodename, Dictionary<IBaseNode, string> dict)
        {
            if (dict.ContainsValue(nodename))
            {
                foreach (IBaseNode node in dict.Keys)
                {
                    if (dict[node].Equals(nodename))
                        return node;
                }
            }

            return null;
        }

        private static string getPath(IBaseNode node, eNodeType rootNode)
        {
            string result = node.Name;
            IBaseNode parent = node.Parent;
            while (parent != null && parent.NodeType != rootNode)
            {
                result = parent.Name + "." + result;
                parent = parent.Parent;
            }
            return result;
        }

        private static IBaseNode FindInDest(IBaseNode source, IBaseNode node)
        {
            if (source.Reference == node.Reference)
                return source;

            if (source.Nodes.Count > 0)
            {
                foreach (IBaseNode subnode in source.Nodes)
                {
                    var _found = FindInDest(subnode, node);
                    if (_found != null)
                        return _found;
                }
            }

            return null;
        }
    }
}
