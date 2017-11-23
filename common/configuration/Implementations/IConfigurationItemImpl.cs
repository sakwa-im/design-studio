using kms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace configuration
{
    public class IConfigurationItemImpl : IConfigurationItem
    {
        public IConfigurationItemImpl() { }
        public IConfigurationItemImpl(string name)
        {
            _Name = name;

        }

        public IConfigurationItemImpl(string name, string newValue, 
            eConfigurationSource target)
        {
            _Name = name;
            InternalValue = newValue;
            _Target = target;

            _Source = eConfigurationSource.Construction;

        }
        public IConfigurationItemImpl(string name, string newValue, 
            eConfigurationSource target, bool isCollection)
        {
            _Name = name;
            InternalValue = newValue;
            _Target = target;
            _Source = eConfigurationSource.Construction;

            _IsCollection = isCollection;

        }
        public IConfigurationItemImpl(string name, string newValue, 
            eConfigurationSource target, 
            eConfigurationSource sourceAllowed = eConfigurationSource.AllAllowed)
        {
            _Name = name;
            InternalValue = newValue;
            _Target = target;
            _Source = eConfigurationSource.Construction;
            _SourceAllowed = sourceAllowed;

        }
        public IConfigurationItemImpl(string name, string newValue, 
            eConfigurationSource target, bool isCollection, 
            eConfigurationSource sourceAllowed = eConfigurationSource.AllAllowed)
        {
            _Name = name;
            InternalValue = newValue;
            _Target = target;
            _Source = eConfigurationSource.Construction;
            _IsCollection = isCollection;
            _SourceAllowed = sourceAllowed;

        }

        public static IConfigurationItem FromXml(XmlNode Node, eConfigurationSource target)
        {

            IConfigurationItem result = new IConfigurationItemImpl("", Node.InnerText, target);
            result.StorageKey = Node.Attributes[tag_key_attribute] != null ? Node.Attributes[tag_key_attribute].InnerText : "";
            result.Type = Node.Attributes[tag_type_attribute] != null ? Node.Attributes[tag_type_attribute].InnerText : "";

            return result;
        }

        #region IConfigurationItem implementation
        string IConfigurationItem.Name { get { return _Name; } set { _Name = value; } }
        string IConfigurationItem.Value { get { return _Value; } set { _Value = value; } }
        eConfigurationSource IConfigurationItem.Source { get { return _Source; } set { _Source = value; } }
        eConfigurationSource IConfigurationItem.SourceAllowed { get{ return _SourceAllowed; }  set { _SourceAllowed = value; } }
        eConfigurationSource IConfigurationItem.Target
        {
            get { return _Target; }
            set
            {
                if ((_Target & eConfigurationSource.CmdLine) == eConfigurationSource.CmdLine)
                    value ^= eConfigurationSource.CmdLine;

                if ((_Target & eConfigurationSource.AppConfig) == eConfigurationSource.AppConfig)
                    value ^= eConfigurationSource.AppConfig;

                _Target = value;

            }
        }
        IConfigurationItem IConfigurationItem.Parent { get { return _Parent; } set { _Parent = value; } }

        protected bool IsSourceAllowed(eConfigurationSource source)
        {
            bool result = false;
            if (_Source == eConfigurationSource.Undefined || Convert.ToInt16(source) <= Convert.ToInt16(_Source))
                result = (_SourceAllowed & source) != eConfigurationSource.Undefined;

            return result;

        }
        bool IConfigurationItem.SetValue(string newValue, eConfigurationSource source)
        {
            if (IsSourceAllowed(source))
            {
                if (source == eConfigurationSource.User && _OrgValue == "" && InternalValue != newValue)
                    _OrgValue = InternalValue;

                InternalValue = newValue;
                _Source = source;

                return true;

            } //if (_Source == eConfigurationSource.Undefined ...

            return false;

        } //bool IConfigurationItem.SetValue(
        bool IConfigurationItem.SetValue(XmlNode newValue, eConfigurationSource source)
        {
            if (newValue != null && IsSourceAllowed(source))
            {
                if (newValue.Attributes[tag_definition_attribute] != null)
                {
                    _ConfigurationItemValueType = (eConfigurationItemValueType)Enum.Parse(typeof(eConfigurationItemValueType), newValue.Attributes[tag_definition_attribute].InnerText, true);

                    bool result = false;

                    switch (_ConfigurationItemValueType)
                    {
                        case eConfigurationItemValueType.plain:
                            if (newValue.ChildNodes.Count >= 1 && newValue.ChildNodes[0].GetType() == typeof(XmlElement))
                            {
                                (this as IConfigurationItem).Clear();
                                foreach (XmlNode node in newValue)
                                {
                                    IConfigurationItem item = new IConfigurationItemImpl(node.Name);
                                    if (node.Attributes[tag_key_attribute] != null)
                                        item.StorageKey = node.Attributes[tag_key_attribute].InnerText;

                                    item.SetValue(node, source);
                                    item.Source = source;
                                    item.Target = _Target;

                                    _Source = source;
                                    _ConfigurationItems.Add(item);

                                } //foreach (XmlNode node in newValue)
                            } //if (newValue.ChildNodes.Count > 1)
                            else
                                _Value = GetNodeValue(newValue);

                            result = true;

                            break;

                        case eConfigurationItemValueType.xml:
                            _Value = GetNodeValue(newValue);

                            result = true;
                            break;

                        case eConfigurationItemValueType.list:
                            (this as IConfigurationItem).Clear();
                            foreach (XmlNode node in newValue)
                            {
                                IConfigurationItem item = new IConfigurationItemImpl("item" + Convert.ToString(_ConfigurationItems.Count + 1));
                                item.SetValue(node, source);
                                item.Source = source;
                                item.Target = _Target;

                                _Source = source;
                                _ConfigurationItems.Add(item);

                            } //foreach (XmlNode node in newValue)

                            result = true;
                            
                            break;

                    } //switch (_ConfigurationItemValueType)

                    return result;

                } //if (newValue.Attributes[tag_definition_attribute] != null)

                switch (newValue.ChildNodes.Count)
                {
                    case 0:
                    case 1:
                        if (_IsCollection)
                        {
                            (this as IConfigurationItem).Clear();
                            foreach (XmlNode node in newValue)
                            {
                                IConfigurationItem item = new IConfigurationItemImpl(node.Name);
                                item.SetValue(node, source);
                                _Source = source;
                                _ConfigurationItems.Add(item);

                            } //foreach (XmlNode node in newValue)
                        } //if (_IsCollection)
                        else
                            InternalValue = GetNodeValue(newValue);

                        break;

                    default:
                        (this as IConfigurationItem).Clear();
                        foreach (XmlNode node in newValue)
                        {
                            IConfigurationItem item = new IConfigurationItemImpl(node.Name);
                            item.SetValue(node, source);
                            _ConfigurationItems.Add(item);

                        } //foreach (XmlNode node in newValue)

                        break;

                } //switch(newValue.ChildNodes.Count)

                _Source = source;

                return true;

            } //if (newValue != null && (_Source ...

            return false;

        } //bool IConfigurationItem.SetValue(
        bool IConfigurationItem.SaveToTarget(eConfigurationSource target)
        {
            return _Source != eConfigurationSource.Undefined || _IsCollection 
                ? (_Target & target) != eConfigurationSource.Undefined 
                : false;
 
        } //bool IConfigurationItem.SaveToTarget( ...
        bool IConfigurationItem.SaveTo(XmlDocument doc, eConfigurationSource target)
        {
            bool result = false;

            if (doc != null && (this as IConfigurationItem).SaveToTarget(target))
            {
                if (doc.InnerXml == "")
                    doc.InnerXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><configuration></configuration>";

                #region ServerSide, CommonAppData and UserAppData
                if ((target &
                    (eConfigurationSource.ServerSide |
                     eConfigurationSource.GroupServerSide |
                     eConfigurationSource.UserServerSide |
                     eConfigurationSource.AllUsersAppData |
                     eConfigurationSource.UserAppData)) != eConfigurationSource.Undefined)
                {
                    XmlNode Node = doc.DocumentElement[_Name];
                    if (Node == null)
                    {
                        Node = GetXmlNode(doc, Node);
                        doc.DocumentElement.AppendChild(Node);

                    } //if (Node == null)
                    else
                    {
                        Node.RemoveAll();
                        Node = GetXmlNode(doc, Node); //Update the definition attribute

                    } //else, if (Node == null)

                    switch (_ConfigurationItemValueType)
                    {
                        case eConfigurationItemValueType.plain:
                            if (_ConfigurationItems.Count > 0)
                            {
                                foreach (IConfigurationItem item in _ConfigurationItems)
                                {
                                    AddSubNode(item, Node);

                                } //foreach(IConfigurationItem item in _ConfigurationItems)
                            } //if(_ConfigurationItems.Count > 0)
                            else
                                Node.InnerText = _Value;

                            break;

                        case eConfigurationItemValueType.xml:
                            Node.InnerText = _Value;
                            break;

                        case eConfigurationItemValueType.list:
                            foreach (IConfigurationItem item in _ConfigurationItems)
                            {
                                AddSubNode(item, Node);
                                //XmlNode childNode = item.GetXmlNode(doc);
                                //Node.AppendChild(childNode);
                                //childNode.InnerText = item.Value;

                            } //foreach(IConfigurationItem item in _ConfigurationItems)
                            break;

                    }

                    //if (_ConfigurationItems.Count > 0)
                    //{
                    //    foreach (IConfigurationItem item in _ConfigurationItems)
                    //    {
                    //        XmlNode childNode = item.GetXmlNode(doc);
                    //        Node.AppendChild(childNode);
                    //        childNode.InnerText = item.Value;

                    //    } //foreach(IConfigurationItem item in _ConfigurationItems)
                    //} //if(_ConfigurationItems.Count > 0)
                    //else
                    //    Node.InnerText = InternalValue;

                    result = true;

                } //if((target & ...
                #endregion
                else
                #region app.config
                    if ((target & eConfigurationSource.AppConfig) != eConfigurationSource.Undefined)
                    {
                        XmlNode root = doc.DocumentElement[tag_appSettings];
                        if (root == null)
                        {
                            root = doc.CreateNode(XmlNodeType.Element, tag_appSettings, "");
                            doc.DocumentElement.AppendChild(root);

                        }
                        XmlNode Node = null;
                        foreach (XmlNode node in root)
                            if (node.Attributes[tag_key] != null && node.Attributes[tag_key].InnerText == _Name)
                            {
                                Node = node;
                                break;

                            } //if (node.Attributes["key"] != null && node.Attributes["key"].InnerText = _Name)

                        if (Node == null)
                        {
                            Node = doc.CreateNode(XmlNodeType.Element, tag_addNode, "");
                            Node.Attributes.Append(doc.CreateAttribute(tag_key));
                            Node.Attributes.Append(doc.CreateAttribute(tag_value));
                            Node.Attributes[tag_key].InnerText = _Name;

                            root.AppendChild(Node);

                        } //if (Node == null)

                        Node.Attributes[tag_value].InnerText = _Value;

                    } //if ((target & eConfigurationSource.AppConfig) != eConfigurationSource.Undefined)
                    #endregion
            
            } //if (doc != null && (this as IConfigurationItem).SaveToTarget(target))

            return result;

        } //bool IConfigurationItem.SaveTo( ...
        bool IConfigurationItem.SaveTo(IConfigurationSource source)
        {
            return source != null ? source.UpdateConfigurationItem(this) : false;

        } //bool IConfigurationItem.SaveTo( ...
        XmlNode IConfigurationItem.GetXmlNode(XmlDocument doc, XmlNode node) { return GetXmlNode(doc, node); }
        protected XmlNode GetXmlNode(XmlDocument doc, XmlNode node = null)
        {
            XmlNode result = node == null ? doc.CreateNode(XmlNodeType.Element, _Name, "") : node;

            XmlAttribute definition = null;
            if (result.Attributes[tag_definition_attribute] == null)
            {
                definition = doc.CreateAttribute(tag_definition_attribute);
                result.Attributes.Append(definition);
            }
            else
                definition = result.Attributes[tag_definition_attribute];

            definition.InnerText = _ConfigurationItemValueType.ToString();

            definition = null;
            if (result.Attributes[tag_key_attribute] == null)
            {
                definition = doc.CreateAttribute(tag_key_attribute);
                result.Attributes.Append(definition);
            }
            else
                definition = result.Attributes[tag_key_attribute];

            definition.InnerText = _StorageKey;

            definition = null;
            if (result.Attributes[tag_type_attribute] == null)
            {
                definition = doc.CreateAttribute(tag_type_attribute);
                result.Attributes.Append(definition);
            }
            else
                definition = result.Attributes[tag_type_attribute];

            definition.InnerText = _Type;

            return result;

        } //protected XmlNode GetXmlNode(XmlDocument doc)
        protected XmlNode AddSubNode(IConfigurationItem item, XmlNode node)
        {
            XmlNode result = node.OwnerDocument.CreateNode(XmlNodeType.Element, item.Name, "");

            XmlAttribute definition = null;
            if (result.Attributes[tag_definition_attribute] == null)
            {
                definition =  node.OwnerDocument.CreateAttribute(tag_definition_attribute);
                result.Attributes.Append(definition);
            }
            else
                definition = result.Attributes[tag_definition_attribute];

            definition.InnerText = item.ConfigurationItemValueType.ToString();

            definition = null;
            if (result.Attributes[tag_key_attribute] == null)
            {
                definition = node.OwnerDocument.CreateAttribute(tag_key_attribute);
                result.Attributes.Append(definition);
            }
            else
                definition = result.Attributes[tag_key_attribute];

            definition.InnerText = item.StorageKey;

            definition = null;
            if (result.Attributes[tag_type_attribute] == null)
            {
                definition = node.OwnerDocument.CreateAttribute(tag_type_attribute);
                result.Attributes.Append(definition);
            }
            else
                definition = result.Attributes[tag_type_attribute];

            definition.InnerText = item.Type;

            node.AppendChild(result);

            if (item.ConfigurationItems.Count > 0)
            {
                foreach (IConfigurationItem it in item.ConfigurationItems)
                    AddSubNode(it, result);
            }
            else
                result.InnerText = item.Value;

            return result;

        } //protected XmlNode AddSubNode(IConfigurationItem item, XmlNode node)
        eConfigurationItemValueType IConfigurationItem.ConfigurationItemValueType { get { return _ConfigurationItemValueType; } set { _ConfigurationItemValueType = value; } }

        private string GetNodeValue(XmlNode node)
        {
            if(node != null)
            {
                if (node.Name == tag_addNode && node.Attributes[tag_value] != null)
                    return node.Attributes[tag_value].InnerText;

                return node.InnerText;

            } //if(node != null)

            return "";

        } //private string GetNodeValue(XmlNode node)

        public static readonly string tag_appSettings = "appSettings";
        public static readonly string tag_addNode = "add";
        public static readonly string tag_key = "key";
        public static readonly string tag_value = "value";
        public static readonly string tag_definition_attribute = "definition";
        public static readonly string tag_key_attribute = "key";
        public static readonly string tag_type_attribute = "type";

        bool IConfigurationItem.AddConfigurationItem(string path, IConfigurationItem item)
        {
            IConfigurationItem parent = ConfigurationItemFromPath(path);
            if (parent != null)
            {
                item.Parent = parent;
                item.Configuration = _Configuration;
                parent.ConfigurationItems.Add(item);

                return true;

            } //if (parent != null)

            return false;

        } //bool IConfigurationItem.AddConfigurationItem( ...
        bool IConfigurationItem.AddConfigurationItem(IConfigurationItem item)
        {
            if (item != null)
            {
                if (CanAdd(item))
                {
                    item.Configuration = _Configuration;
                    item.Parent = this;
                    _ConfigurationItems.Add(item);

                    _Source = eConfigurationSource.User;

                } //if (CanAdd(item))

                return true;

            } //if (item != null)

            return false;

        } //bool IConfigurationItem.AddConfigurationItem( ...
        protected bool CanAdd(IConfigurationItem item)
        {
            foreach (IConfigurationItem it in _ConfigurationItems)
                if (item.Name == it.Name)
                    return false;

            return true;

        }
        bool IConfigurationItem.RemoveConfigurationItem(IConfigurationItem item)
        {
            if (item != null && _ConfigurationItems.Contains(item))
            {
                item.Clear();
                _ConfigurationItems.Remove(item);

                if (_ConfigurationItems.Count == 0)
                    _Source = eConfigurationSource.Undefined;

                return true;

            } //if (_ConfigurationItems.Contains(item))

            return false;

        } //bool IConfigurationItem.RemoveConfigurationItem(
        bool IConfigurationItem.RemoveConfigurationItem(string path)
        {
            IConfigurationItem item = ConfigurationItemFromPath(path);
            if (item != null)
            {
                item.Clear();
                item.Parent.ConfigurationItems.Remove(item);

                return true;

            } //if (item != null)

            return false;

        } //bool IConfigurationItem.RemoveConfigurationItem(
        IConfigurationItem IConfigurationItem.GetConfigurationItem(string path)
        {
            return ConfigurationItemFromPath(path);
        }
        List<IConfigurationItem> IConfigurationItem.ConfigurationItems { get { return _ConfigurationItems; } }
        void IConfigurationItem.Clear()
        {
            while (_ConfigurationItems.Count > 0)
            {
                IConfigurationItem item = _ConfigurationItems[0];
                _ConfigurationItems.RemoveAt(0);

                item.Clear();
                item = null;

            } //while (_ConfigurationItems.Count > 0)

            //_Source = eConfigurationSource.Undefined;

        } //void IConfigurationItem.Clear()
        bool IConfigurationItem.IsDirty
        {
            get
            {
                if (_IsCollection)
                {
                    foreach (IConfigurationItem item in _ConfigurationItems)
                        if(item.IsDirty)
                            return true;

                    return false;

                } //if (_IsCollection)

                return _Source == eConfigurationSource.User && _OrgValue != "" && _OrgValue != InternalValue;

            }
        }
        void IConfigurationItem.Reset()
        {
            _OrgValue = "";
            foreach (IConfigurationItem item in _ConfigurationItems)
                item.Reset();

        }

        protected IConfiguration _Configuration = null;
        protected string _Name = "";
        protected string _Value = "";
        protected string _OrgValue = "";
        protected eConfigurationSource _Source = eConfigurationSource.Undefined;
        protected eConfigurationSource _SourceAllowed = eConfigurationSource.AllAllowed;
        protected eConfigurationSource _Target = eConfigurationSource.Undefined;
        protected IConfigurationItem _Parent = null;
        protected bool _IsCollection = false;
        protected eConfigurationItemValueType _ConfigurationItemValueType = eConfigurationItemValueType.plain;
        protected string _StorageKey = "";
        protected string _Type = "";
        protected List<IConfigurationItem> _ConfigurationItems = new List<IConfigurationItem>();
        protected IConfigurationItem ConfigurationItemFromPath(string path)
        {
            IConfigurationItem result = this;
            if (path != "")
            {
                result = ConfigurationItemRoot;
                string[] elems = path.Split(new char[] { '.' });
                foreach (string name in elems)
                {
                    result = GetConfigurationItem(result, name);
                    if (result == null)
                        break;

                }
            }

            return result;

        }
        protected IConfigurationItem ConfigurationItemRoot
        {
            get
            {
                IConfigurationItem result = this;

                while (result.Parent != null)
                    result = result.Parent;

                return result;

            }
        } //protected IConfigurationItem ConfigurationItemRoot
        protected IConfigurationItem GetConfigurationItem(IConfigurationItem item, string name)
        {
            foreach (IConfigurationItem ci in item.ConfigurationItems)
                if (ci.Name == name)
                    return ci;

            return null;

        } //protected IConfigurationItem GetConfigurationItem

        #region Value Getter/Setters
        string IConfigurationItem.GetValue(string defaultValue)
        {
            return _Source != eConfigurationSource.Undefined ? InternalValue : defaultValue;
        }
        void IConfigurationItem.SetValue(string newValue)
        {
            (this as IConfigurationItem).SetValue(newValue, eConfigurationSource.User);

        }
        int IConfigurationItem.GetValue(int defaultValue)
        {
            int result = defaultValue;
            try
            {
                if (_Source != eConfigurationSource.Undefined)
					if (!String.IsNullOrEmpty(InternalValue))
						result = int.Parse(InternalValue);
            }
            catch (Exception)
            {
                result = defaultValue;
            }

            return result;

        }
        void IConfigurationItem.SetValue(int newValue)
        {
            (this as IConfigurationItem).SetValue(newValue.ToString(), eConfigurationSource.User);

        }
        float IConfigurationItem.GetValue(float defaultValue)
        {
            float result = 0.0F;
            try
            {
                if (_Source != eConfigurationSource.Undefined)
                    if (!String.IsNullOrEmpty(InternalValue))
                        result = float.Parse(InternalValue, System.Globalization.NumberStyles.AllowDecimalPoint);
            }
            catch (Exception)
            {
                result = defaultValue;
            }

            return result;

        }
        void IConfigurationItem.SetValue(float newValue)
        {
            (this as IConfigurationItem).SetValue(newValue.ToString(), eConfigurationSource.User);

        }
        bool IConfigurationItem.GetValue(bool defaultValue)
		{
			bool result = defaultValue;
			try
			{
				if (_Source != eConfigurationSource.Undefined)
					if (!String.IsNullOrEmpty(InternalValue))
						result = bool.Parse(InternalValue);

			}
			catch (Exception)
			{
				result = defaultValue;
			}
			return result;
		}
        void IConfigurationItem.SetValue(bool newValue)
        {
            (this as IConfigurationItem).SetValue(newValue.ToString(), eConfigurationSource.User);

        }
        IConfiguration IConfigurationItem.Configuration
        {
            get { return _Configuration; }
            set { _Configuration = value; }
        }


        protected string InternalValue
        {
            get
            {
                string result = _Value;
                if(_StorageKey != "")
                {
                    IKey key = _Configuration != null ? _Configuration.GetKeyById(_StorageKey) : null;
                    if (key != null)
                    {
                        result = key.Decrypt(_Value);
                    }
                }
                return result;
            }
            set
            {
                if (_StorageKey == "")
                    _Value = value;
                else
                {
                    IKey key = _Configuration != null ? _Configuration.GetKeyById(_StorageKey) : null;
                    if (key != null)
                    {
                        _Value = key.Encrypt(value);
                    }
                }
            }
        }
        string IConfigurationItem.StorageKey
        {
            set { _StorageKey = value; }
            get { return _StorageKey; }
        }
        string IConfigurationItem.Type { get { return _Type; } set { _Type = value; } }

        #endregion

        string IConfigurationItem.ToString()
        {
            string result = "";
            result += "IConfigurationItem {" + "Name: " + _Name;
            result += "; Value: " + InternalValue;
            result += "; Source: " + eCSToString(_Source);
            result += "; SourceAllowed: " + eCSToString(_SourceAllowed);
            result += "; Target: " + eCSToString(_Target);
            if (_ConfigurationItems.Count > 0)
            {
                result += Environment.NewLine + "ConfigurationItems {" + Environment.NewLine;
                foreach (IConfigurationItem item in _ConfigurationItems)
                    result += item.ToString();

                result += "}" + Environment.NewLine;

            }

            result += "}" + Environment.NewLine;
            return result;

        } //string IConfigurationItem.ToString()
        string IConfigurationItem.ToString(eConfigurationSource SourecOrTarget)
        {
            return eCSToString(SourecOrTarget);

        } //string IConfigurationItem.ToString( ...

        /*
        public enum eConfigurationSource { 
        Undefined, User = 1, 
        CmdLine = 2, 
        UserAppData = 4, 
        AllUsersAppData = 8, 
        UserServerSide = 16,
        GroupServerSide = 32,
        ServerSide = 64, 
        AppConfig = 128, 
        NonPersistent = 256,
        AllAllowed = 1 + 2 + 4 + 8 + 16 + 32 + 64 + 128 + 256,
        Construction = 512}

         * */
        private string eCSToString(eConfigurationSource source)
        {
            string result = "";
            //if ((source & eConfigurationSource.AllAllowed) == eConfigurationSource.AllAllowed)
            //    return "AllAllowed";

            if (source == eConfigurationSource.Undefined)
                return "Undefined";

            if ((source & eConfigurationSource.User) == eConfigurationSource.User)
                result += result != "" ? ", User" : "User";

            if ((source & eConfigurationSource.CmdLine) == eConfigurationSource.CmdLine)
                result += result != "" ? ", CmdLine" : "CmdLine";

            if ((source & eConfigurationSource.AllUsersAppData) == eConfigurationSource.AllUsersAppData)
                result += result != "" ? ", AllUsersAppData" : "AllUsersAppData";

            if ((source & eConfigurationSource.UserAppData) == eConfigurationSource.UserAppData)
                result += result != "" ? ", UserAppData" : "UserAppData";

            if ((source & eConfigurationSource.ServerSide) == eConfigurationSource.ServerSide)
                result += result != "" ? ", ServerSide" : "ServerSide";

            if ((source & eConfigurationSource.UserServerSide) == eConfigurationSource.UserServerSide)
                result += result != "" ? ", UserServerSide" : "UserServerSide";

            if ((source & eConfigurationSource.GroupServerSide) == eConfigurationSource.GroupServerSide)
                result += result != "" ? ", GroupServerSide" : "GroupServerSide";

            if ((source & eConfigurationSource.AppConfig) == eConfigurationSource.AppConfig)
                result += result != "" ? ", AppConfig" : "AppConfig";

            if ((source & eConfigurationSource.NonPersistent) == eConfigurationSource.NonPersistent)
                result += result != "" ? ", NonPersistent" : "NonPersistent";

            if ((source & eConfigurationSource.Construction) == eConfigurationSource.Construction)
                result += result != "" ? ", Construction" : "Construction";

            return result;

        } //private string eCSToString(eConfigurationSource source)
        #endregion

    } //public class IConfigurationItemImpl

}
