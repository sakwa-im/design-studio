using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace kms
{
    public class IKeyringImpl : IKeyring
    {
        public IKeyringImpl()
        {
            _Id = Guid.NewGuid().ToString();
        }
        public IKeyringImpl(string id)
        {
            _Id = id != "" ? id : Guid.NewGuid().ToString();
        }
        public IKeyringImpl(XmlNode node)
        {
            _Id = node.Attributes["id"] != null ? node.Attributes["id"].InnerText : Guid.NewGuid().ToString();

            foreach (XmlNode n in node)
                switch (n.Name)
                {
                    case "purpose": _Purpose = n.InnerText; break;
                    case "subject": _Subject = n.InnerText; break;
                    case "scope": _Scope = n.InnerText; break;

                    case "name": _Name = n.InnerText; break;
                    case "reference": _KeyReferences.Add(n.InnerText); break;

                } //switch (n.Name)
        } //public IKeyringImpl(XmlNode node)

        void IKeyring.AddToXmlNode(XmlNode node)
        {
            XmlDocument doc = node.OwnerDocument;

            XmlAttribute id = doc.CreateAttribute("id");
            id.InnerText = _Id;
            node.Attributes.Append(id);

            XmlNode purpose = doc.CreateNode(XmlNodeType.Element, "purpose", "");
            purpose.InnerText = _Purpose;

            XmlNode subject = doc.CreateNode(XmlNodeType.Element, "subject", "");
            subject.InnerText = _Subject;

            XmlNode scope = doc.CreateNode(XmlNodeType.Element, "scope", "");
            scope.InnerText = _Scope;

            XmlNode name = doc.CreateNode(XmlNodeType.Element, "name", "");
            name.InnerText = _Name;

            node.AppendChild(purpose);
            node.AppendChild(subject);
            node.AppendChild(scope);
            node.AppendChild(name);

            foreach (string keyReference in _KeyReferences)
            {
                XmlNode keyref = doc.CreateNode(XmlNodeType.Element, "reference", "");
                keyref.InnerText = keyReference;
                node.AppendChild(keyref);

            } //foreach (string keyReference in keyring.KeyReferences)
        } //void IKeyring.AddToXmlNode(XmlNode node)

        string IKeyring.Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        string IKeyring.Purpose
        {
            get { return _Purpose; }
            set { _Purpose = value; }
        }
        string IKeyring.Subject { get { return _Subject; } set { _Subject = value; } }
        string IKeyring.Scope { get { return _Scope; } set { _Scope = value; } }

        string IKeyring.Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        List<string> IKeyring.KeyReferences { get { return _KeyReferences; } }

        bool IKeyring.IsSpecified { get { return false; } }
        bool IKeyring.IsDefined { get { return false; } }

        protected string _Id = "";
        protected string _Purpose = "";
        protected string _Subject = "";
        protected string _Scope = "";
        protected string _Name = "";
        protected List<string> _KeyReferences = new List<string>();

    } //public class IKeyringImpl
}
