using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace kms
{
    public interface IKeyring
    {
        string Id { get; set; }
        string Purpose { get; set; }
        string Subject { get; set; }
        string Scope { get; set; }

        string Name { get; set; }

        List<string> KeyReferences { get; }

        bool IsSpecified { get; }
        bool IsDefined { get; }

        void AddToXmlNode(XmlNode node);

    }
}
