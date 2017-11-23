using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace kms
{
    public interface IKms
    {
        string KeysFileName { get; set; }
        bool New();
        bool Load();
        bool Load(Stream keyStream);
        bool Save();
        bool SaveAs(string newKeysFile);

        IKey GetKeyByName(string name);
        IKey GetKeyById(string id);
        List<IKey> GetKeysByIds(List<string> ids);
        List<IKey> GetKeys(string scope, string subject, string purpose = "", string name = "");

        IKeyring GetKeyringByName(string name);
        IKeyring GetKeyringById(string id);
        List<IKeyring> GetKeyrings(string purpose, string subject, string scope);

        IKey AddKey(IKey key);
        bool RemoveKey(IKey key);
        IKeyring AddKeyring(IKeyring keyring);
        bool RemoveKeyring(IKeyring keyring);

        IKey GetNewKey();
        IKeyring GetNewKeyring();

        List<IKey> Keys { get; }
        List<IKeyring> Keyrings { get; }

    }
}
