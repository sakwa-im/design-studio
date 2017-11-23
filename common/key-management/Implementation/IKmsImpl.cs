using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Xml;
using System.IO;
using Org.BouncyCastle.Security;

namespace kms
{
    public class IKmsImpl : IKms
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(IKmsImpl));

        public static IKms IKms { get { return _this; } }
        private static IKms _this = new IKmsImpl();
        internal IKmsImpl() { }

        IKey IKms.GetKeyByName(string name)
        {
            foreach (IKey key in _Keys)
                if (key.Name == name)
                    return key;

            return null;
        }
        IKey IKms.GetKeyById(string id)
        {
            foreach (IKey key in _Keys)
                if (key.Id == id)
                    return key;

            return null;
        }
        List<IKey> IKms.GetKeys(string scope, string subject, string purpose, string name)
        {
            List<IKey> scopeKeys = new List<IKey>();
            foreach (IKey key in _Keys)
                if (key.Scope == scope)
                    scopeKeys.Add(key);

            List<IKey> subjectKeys = new List<IKey>();
            foreach (IKey key in scopeKeys)
                if (key.Subject == subject)
                    subjectKeys.Add(key);

            if (purpose != "")
            {
                List<IKey> Keys = new List<IKey>();
                foreach (IKey key in subjectKeys)
                    if (key.Subject == subject)
                        Keys.Add(key);

                if (name != "")
                    foreach (IKey key in Keys)
                        if (key.Name == name)
                        {
                            List<IKey> result = new List<IKey>();
                            result.Add(key);
                            return result;

                        }

                return Keys;

            }

            if (name != "")
            {
                foreach (IKey key in subjectKeys)
                    if (key.Name == name)
                    {
                        List<IKey> result = new List<IKey>();
                        result.Add(key);

                        return result;
                    }

                return new List<IKey>();

            }

            return subjectKeys;

        }

        List<IKey> IKms.GetKeysByIds(List<string> ids)
        {
            List<IKey> keys = new List<IKey>();
            foreach (IKey key in _Keys)
                foreach(string id in ids)
                    if (key.Id == id)
                        keys.Add(key);

            return keys;
        }
        IKeyring IKms.GetKeyringByName(string name)
        {
            foreach (IKeyring kr in _Keyrings)
                if (kr.Name == name)
                    return kr;

            return null;
        }
        IKeyring IKms.GetKeyringById(string id)
        {
            foreach (IKeyring kr in _Keyrings)
                if (kr.Id == id)
                    return kr;

            return null;
        }
        List<IKeyring> IKms.GetKeyrings(string purpose, string subject, string scope)
        {
            List<IKeyring> result = new List<IKeyring>();

            foreach(IKeyring kr in _Keyrings)
            {
                bool add = kr.Purpose == purpose;
                add &= subject != "" ? kr.Subject == subject : true;
                add &= scope != "" ? kr.Scope == scope : true;

                if (add)
                    result.Add(kr);

            } //foreach(IKeyring kr in _Keyrings)

            return result;

        } //List<IKeyring> IKms.GetKeyrings( ...

        string IKms.KeysFileName { get { return _KeysFile; } set { _KeysFile = value; } }
        bool IKms.New()
        {
            _KeysFile = "New keystore";
            _Keys.Clear();
            _Keyrings.Clear();

            return true;

        } //bool IKms.New()
        bool IKms.Load()
        {
            bool result = false;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(_KeysFile);
                result = LoadKeysFile(doc);

            }
            catch (Exception exc)
            {
                log.Debug(exc.ToString());
            }

            return result;

        } //bool IKms.Load()
        bool IKms.Load(Stream keyStream)
        {
            bool result = false;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(keyStream);

                result = LoadKeysFile(doc);

            }
            catch (Exception exc)
            {
                log.Debug(exc.ToString());
            }

            return result;

        } //bool IKms.Load(Stream keyStream)
        bool IKms.Save()
        {
            return SaveKeysFile(_KeysFile);
        }
        bool IKms.SaveAs(string newKeysFile)
        {
            if (SaveKeysFile(newKeysFile))
            {
                _KeysFile = newKeysFile;

                return true;

            } //if (SaveKeysFile(newKeysFile))

            return false;

        } //bool IKms.SaveAs(string newKeysFile)

        IKey IKms.AddKey(IKey key)
        {
            foreach(IKey k in _Keys)
                if (k.Id == key.Id)
                {
                    _Keys.Remove(k);
                    break;

                } //if (k.Name == key.Name)

            _Keys.Add(key);

            return key;

        }
        bool IKms.RemoveKey(IKey key)
        {
            foreach (IKey k in _Keys)
                if (k.Name == key.Name)
                {
                    _Keys.Remove(k);
                    return true;

                } //if (k.Name == key.Name)

            return false;

        }
        IKeyring IKms.AddKeyring(IKeyring keyring)
        {
            foreach(IKeyring kr in _Keyrings)
                if (kr.Name == keyring.Name)
                {
                    _Keyrings.Remove(kr);
                    break;

                } //if (kr.Name == keyring.Name)

            _Keyrings.Add(keyring);

            return keyring;

        }
        bool IKms.RemoveKeyring(IKeyring keyring)
        {
            foreach (IKeyring kr in _Keyrings)
                if (kr.Name == keyring.Name)
                {
                    _Keyrings.Remove(kr);
                    return true;

                } //if (kr.Name == keyring.Name)

            return false;

        }

        IKey IKms.GetNewKey()
        {
            IKey key = new IKeyImpl();
            key.Name = "Key " + _Keys.Count.ToString();
            return key;

        }
        IKeyring IKms.GetNewKeyring()
        {
            IKeyring keyring = new IKeyringImpl();
            keyring.Name = "Keyring " + _Keyrings.Count.ToString();
            return keyring;

        }

        List<IKey> IKms.Keys { get { return _Keys; } }
        List<IKeyring> IKms.Keyrings { get { return _Keyrings; } }

        string _KeysFile = "";
        List<IKey> _Keys = new List<IKey>();
        List<IKeyring> _Keyrings = new List<IKeyring>();

        protected virtual bool LoadKeysFile(XmlDocument doc)
        {
            bool result = false;

            _Keys.Clear();
            _Keyrings.Clear();

            //XmlDocument doc = new XmlDocument();
            try
            {
                //doc.Load(keysFile);

                XmlNode root = doc.DocumentElement;
                XmlNode keys = root["keys"];
                XmlNode keyrings = root["keyrings"];

                if (keys != null)
                    foreach (XmlNode node in keys)
                    {
                        IKey key = new IKeyImpl(node);

                        _Keys.Add(key);

                    } //foreach (XmlNode node in keys)

                if (keyrings != null)
                    foreach (XmlNode node in keyrings)
                    {
                        IKeyring keyring = new IKeyringImpl(node);

                        _Keyrings.Add(keyring);

                    } //foreach (XmlNode node in keyrings)

                result = true;

            }
            catch (Exception exc)
            {
                log.Debug(exc.ToString());
            }

            return result;

        } //protected virtual bool LoadKeysFile(string keysFile)
        protected virtual bool SaveKeysFile(string keysFile)
        {
            bool result = false;

            XmlDocument doc = new XmlDocument();
            try
            {
                string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><kms><keys></keys><keyrings></keyrings></kms>";
                doc.InnerXml = xml;

                XmlNode root = doc.DocumentElement;
                XmlNode keys = root["keys"];
                XmlNode keyrings = root["keyrings"];

                foreach (IKey key in _Keys)
                {
                    XmlNode newNode = doc.CreateNode(XmlNodeType.Element, "key", "");

                    key.AddToXmlNode(newNode);

                    keys.AppendChild(newNode);

                } //foreach (IKey key in _Keys)

                foreach (IKeyring keyring in _Keyrings)
                {
                    XmlNode newNode = doc.CreateNode(XmlNodeType.Element, "keyring", "");

                    keyring.AddToXmlNode(newNode);

                    keyrings.AppendChild(newNode);

                } //foreach (IKey key in _Keys)

                doc.Save(keysFile);

                result = true;

            }
            catch (Exception exc)
            {
                log.Debug(exc.ToString());
            }

            return result;

        } //protected virtual bool SaveKeysFile(string keysFile)
             
    } //public class IKmsImpl
}
