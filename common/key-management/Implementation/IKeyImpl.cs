using System;

namespace kms
{
    using Org.BouncyCastle.Crypto.Engines;
    using Org.BouncyCastle.Crypto.Parameters;
    using Org.BouncyCastle.Utilities.Encoders;
    using Org.BouncyCastle.Crypto.Modes;
    using Org.BouncyCastle.Crypto;
    using System.IO;
    using Org.BouncyCastle.Crypto.Macs;
    using System.Xml;
    using log4net;
    using Org.BouncyCastle.Security;
    using System.Security.Cryptography;

    public class IKeyImpl : IKey
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(IKeyImpl));

        public IKeyImpl()
        {
            _Id = Guid.NewGuid().ToString();
        }
        public IKeyImpl(string id)
        {
            _Id = id != "" ? id : Guid.NewGuid().ToString();
        }
        public IKeyImpl(XmlNode node)
        {
            _Id = node.Attributes["id"] != null ? node.Attributes["id"].InnerText : Guid.NewGuid().ToString();

            foreach (XmlNode n in node)
                switch (n.Name)
                {
                    case "purpose": _Purpose = n.InnerText; break;
                    case "subject": _Subject = n.InnerText; break;
                    case "scope": _Scope = n.InnerText; break;

                    case "name": _Name = n.InnerText; break;
                    case "type": _KeyType = (keyType)Enum.Parse(typeof(keyType), n.InnerText, true); break;
                    case "chaining": _KeyChaining = (KeyChaining)Enum.Parse(typeof(KeyChaining), n.InnerText, true); break;
                    case "length": _Length = Convert.ToInt32(n.InnerText); break;

                    case "value": _KeyValue = n.InnerText; break;
                    case "kcv": _KeyKCV = n.InnerText; break;

                    case "oddparity": _OddParity = bool.Parse(n.InnerText); break;

                    case "test-data":
                        foreach (XmlNode tn in n)
                            switch (tn.Name)
                            {
                                case "test-cipher": test_Cipher = tn.InnerText; break;
                                case "test-plain": test_plain = tn.InnerText; break;
                                case "test-icv": test_Icv = tn.InnerText; break;
                            }
                        break;

                    case "notes": _Notes = n.InnerText; break;

                } //switch (n.Name)
        } //public IKeyImpl(XmlNode node)
        public IKeyImpl(IKey key)
        {
            _Id = Guid.NewGuid().ToString();

            _Purpose = key.Purpose;
            _Subject = key.Subject;
            _Scope = key.Scope;

            _Name = "Clone of " + key.Name;

            _KeyType = key.keyType;
            _KeyChaining = key.KeyChaining;
            _Length = key.Length;

            _OddParity = key.OddParity;

        }
        public IKeyImpl(IKey key, byte[] bin)
        {
            _Id = Guid.NewGuid().ToString();

            _Purpose = key.Purpose;
            _Subject = key.Subject;
            _Scope = key.Scope;

            _Name = "Clone of " + key.Name;

            _KeyType = key.keyType;
            _KeyChaining = key.KeyChaining;
            _Length = key.Length;

            _OddParity = key.OddParity;

            (this as IKey).keyBytes = bin;

        }

        void IKey.AddToXmlNode(XmlNode node)
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

            XmlNode type = doc.CreateNode(XmlNodeType.Element, "type", "");
            type.InnerText = _KeyType.ToString();

            XmlNode chaining = doc.CreateNode(XmlNodeType.Element, "chaining", "");
            chaining.InnerText = _KeyChaining.ToString();

            XmlNode length = doc.CreateNode(XmlNodeType.Element, "length", "");
            length.InnerText = _Length.ToString();

            XmlNode val = doc.CreateNode(XmlNodeType.Element, "value", "");
            val.InnerText = _KeyValue;

            XmlNode kcv = doc.CreateNode(XmlNodeType.Element, "kcv", "");
            kcv.InnerText = _KeyKCV;

            XmlNode oddparity = doc.CreateNode(XmlNodeType.Element, "oddparity", "");
            oddparity.InnerText = _OddParity.ToString();

            node.AppendChild(purpose);
            node.AppendChild(subject);
            node.AppendChild(scope);
            node.AppendChild(name);
            node.AppendChild(type);
            node.AppendChild(chaining);
            node.AppendChild(length);
            node.AppendChild(val);
            node.AppendChild(kcv);
            node.AppendChild(oddparity);

            XmlNode testdata = doc.CreateNode(XmlNodeType.Element, "test-data", "");
            XmlNode testcipher = doc.CreateNode(XmlNodeType.Element, "test-cipher", "");
            testcipher.InnerText = test_Cipher;
            XmlNode testplain = doc.CreateNode(XmlNodeType.Element, "test-plain", "");
            testplain.InnerText = test_plain;
            XmlNode testicv = doc.CreateNode(XmlNodeType.Element, "test-icv", "");
            testicv.InnerText = test_Icv;

            testdata.AppendChild(testcipher);
            testdata.AppendChild(testplain);
            testdata.AppendChild(testicv);

            node.AppendChild(testdata);

            XmlNode notes = doc.CreateNode(XmlNodeType.Element, "notes", "");
            notes.InnerText = _Notes;
            node.AppendChild(notes);

        } //

        string IKey.Id{get { return _Id; }set { _Id = value; }}
        string IKey.Purpose{get { return _Purpose; }set { _Purpose = value; }}
        string IKey.Subject{ get { return _Subject; } set { _Subject = value; } }
        string IKey.Scope { get { return _Scope; } set { _Scope = value; } }

        string IKey.Name{get { return _Name; }set { _Name = value; }}
        keyType IKey.keyType {get { return _KeyType; }set { _KeyType = value; }}
        KeyChaining IKey.KeyChaining { get { return _KeyChaining; } set { _KeyChaining = value; } }

        int IKey.Length{get { return _Length; }set { _Length = value; }}

        string IKey.keyValue
        {
            get
            {
                return _KeyValue; 
            }
            set
            {
                _KeyValue = value.ToUpper();
                theKey = null;
            }}
        byte[] IKey.keyBytes
        {
            get{ return Hex.Decode(_KeyValue); }
            set
            {
                if (value != null && (value.Length == 16 || value.Length == 24))
                {
                    _KeyValue = Hex.ToHexString(value);
                    _KeyKCV = Hex.ToHexString((this as IKey).Encrypt(Hex.Decode("000000"))).ToUpper();
                    _Length = value.Length;

                }
                else
                {
                    _KeyValue = "";
                    _KeyKCV = "";

                }
            }
        }
        bool IKey.KeyValueFromGuid(string guid)
        {
            guid = guid.Replace("-", "");
            if (guid.Length > 32)
                guid = guid.Substring(0, 32);

            (this as IKey).keyBytes = KeyUtils.GetBytes(guid);
            return _KeyValue.Length > 0;
        }


        string IKey.kcv{get{return _KeyKCV;}set { _KeyKCV = value.ToUpper(); }}

        bool IKey.OddParity { get { return _OddParity; }
            set
            {
                _OddParity = value;
                if (_OddParity)
                    _KeyValue = Hex.ToHexString(KeyUtils.ToOddParity(Hex.Decode(_KeyValue)));

            } }

        byte[] IKey.Encrypt(byte[] plain)
        {
            return (this as IKey).Encrypt(plain, _KeyChaining);
        }
        byte[] IKey.Encrypt(byte[] plain, KeyChaining chaining)
        {
            return (this as IKey).Encrypt(plain, null, chaining);

        } //byte[] IKey.Encrypt(byte[] plain)
        byte[] IKey.Encrypt(byte[] plain, byte[] icv, KeyChaining chaining/* = KeyChaining.CBC*/)
        {
            byte[] result = new byte[0];
            if(Initialized)
                switch (_KeyType)
                {
                    case keyType.DESede:
                        result = encryptDesEde(plain, chaining, true, icv);
                        break;

                    case keyType.AES:
                        result = encryptAES(plain, chaining, true, icv);
                        break;

                } //switch (_KeyType)

            return result;

        }
        string IKey.Encrypt(string plain)
        {
            return KeyUtils.GetString(
                           EncryptStringToBytes_Aes(plain, 
                           Hex.Decode(_KeyValue), 
                           new byte[16]));

        }
        byte[] IKey.Decrypt(byte[] crypto)
        {
            return (this as IKey).Decrypt(crypto, _KeyChaining);
        }
        byte[] IKey.Decrypt(byte[] crypto, KeyChaining chaining)
        {
            return (this as IKey).Decrypt(crypto, null, _KeyChaining);

        } //byte[] IKey.Decrypt( ...
        byte[] IKey.Decrypt(byte[] crypto, byte[] icv, KeyChaining chaining/* = KeyChaining.CBC*/)
        {
            byte[] result = new byte[0];
            if (Initialized)
                switch (_KeyType)
                {
                    case keyType.DESede:
                        result = encryptDesEde(crypto, chaining, false, icv);
                        break;

                    case keyType.AES:
                        result = encryptAES(crypto, chaining, false, icv);
                        break;

                } //switch (_KeyType)

            return result;

        }
        string IKey.Decrypt(string crypto)
        {
            return DecryptStringFromBytes_Aes(
                KeyUtils.GetBytes(crypto), 
                Hex.Decode(_KeyValue), 
                new byte[16]);
        }
        byte[] IKey.CalculateMac(byte[] input)
        {
            byte[] result = new byte[0];
            if (Initialized)
                switch (_KeyType)
                {
                    case keyType.DESede:
                        result = CalculateDesMac(input, null, 0);
                        break;

                    case keyType.AES:
                        result = CalculateAESMac(input, null, 0);
                        break;

                } //switch (_KeyType)

            return result;

        } //byte[] IKey.CalculateMac( ...
        byte[] IKey.CalculateMac(byte[] input, byte[] icv)
        {
            byte[] result = new byte[0];
            if (Initialized)
                switch (_KeyType)
                {
                    case keyType.DESede:
                        result = CalculateDesMac(input, icv, 0);
                        break;

                    case keyType.AES:
                        result = CalculateAESMac(input, icv, 0);
                        break;

                } //switch (_KeyType)

            return result;

        } //byte[] IKey.CalculateMac( ...
        byte[] IKey.CalculateMac(byte[] input, byte[] icv = null, int macType = 0)
        {
            byte[] result = new byte[0];
            if (Initialized)
                switch (_KeyType)
                {
                    case keyType.DESede:
                        result = CalculateDesMac(input, icv, macType);
                        break;

                    case keyType.AES:
                        result = CalculateAESMac(input, icv, macType);
                        break;

                } //switch (_KeyType)

            return result;
        }

        bool IKey.IsSpecified { get { return false; } }
        bool IKey.IsDefined
        {
            get
            {
                bool result = _KeyValue != "";

                switch (_KeyType)
                {
                    case keyType.AES:
                    case keyType.DESede:
                        result &=  _KeyValue.Length == _Length * 2;
                        result &= _KeyChaining != KeyChaining.NONE;
                        break;

                    case keyType.PIN:
                        result = false;
                        break;

                } //switch (_KeyType)

                return result;
            }
        }

        IKey IKey.DeriveKey(byte[] derivationData, KeyChaining chaining)
        {
            IKey result = null;
            if (Initialized)
            {
                byte[] derivedKey = null;
                switch (_KeyType)
                {
                    case keyType.DESede:
                        derivedKey = encryptDesEde(derivationData, chaining);
                        break;

                    case keyType.AES:
                        derivedKey = encryptAES(derivationData, chaining);
                        break;

                } //switch(_KeyType)

                if (derivedKey != null && derivedKey.Length == _Length)
                {
                    result = new IKeyImpl();

                    result.Name = "Derived from " + _Name;
                    result.Purpose = _Purpose;
                    result.Subject = _Subject;
                    result.Scope = _Scope;

                    result.keyType = _KeyType;
                    result.Length = _Length;
                    result.KeyChaining = _KeyChaining;

                    result.keyValue = Hex.ToHexString(derivedKey);
                    result.kcv = Hex.ToHexString(result.Encrypt(Hex.Decode("000000")));

                } //if (derivedKey != null && derivedKey.Length == theKey.Length)
            } //if (Initialized)

            return result;

        } //IKey IKey.DeriveKey( ...

        string IKey.test_Icv { get { return test_Icv; } set { test_Icv = value; } }
        string IKey.test_Cipher { get { return test_Cipher; } set { test_Cipher = value; } }
        string IKey.test_plain { get { return test_plain; } set { test_plain = value; } }
        string IKey.Notes { get { return _Notes; } set { _Notes = value; } }


        protected string _Id = "";
        protected string _Purpose = "";
        protected string _Subject = "";
        protected string _Scope = "";
        protected string _Name = "";
        protected keyType _KeyType = keyType.AES;
        protected KeyChaining _KeyChaining = KeyChaining.NONE;

        protected int _Length = 16;

        protected string _KeyValue = "";
        protected string _KeyKCV = "";

        protected bool _OddParity = false;

        protected byte[] theKey = null;

        protected DesEdeParameters _DesEdeParameters = null;
        protected DesEdeEngine _DesEdeEngine = null;

        protected KeyParameter _AesParameters = null;
        protected AesEngine _AesEngine = null;

        protected string test_Icv = "";
        protected string test_plain = "";
        protected string test_Cipher = "";
        protected string _Notes = "";

        protected bool Initialized
        {
            get
            {
                if (theKey == null)
                {
                    switch (_KeyType)
                    {
                        case keyType.DESede:
                            InitializeDESedeKey();
                            break;

                        case keyType.AES:
                            InitializeAESKey();
                            break;

                    } //switch (_KeyType)
                } //if (theKey == null)
            
                return theKey != null;

            }
        } //protected bool Initialized
        protected bool InitializeDESedeKey()
        {
            _DesEdeParameters = new DesEdeParameters(Hex.Decode(_KeyValue));
            _DesEdeEngine = new DesEdeEngine();
            _DesEdeEngine.Init(true, _DesEdeParameters);

            theKey = _DesEdeParameters.GetKey();

            return false;

        }
        protected bool InitializeAESKey()
        {
            _AesParameters = new KeyParameter(Hex.Decode(_KeyValue));
            _AesEngine = new AesEngine();
            _AesEngine.Init(true, _AesParameters);

            theKey = _AesParameters.GetKey();

            return false;

        }
        private string getDesEdeKCV()
        {
            return Hex.ToHexString(encryptDesEde(Hex.Decode("000000"))).ToUpper();

        } //private string getDesEdeKCV(byte[] key)
        private byte[] encryptDesEde(byte[] plain, KeyChaining chaining = KeyChaining.CBC, bool doEncrypt = true, byte[] icv = null)
        {
            BufferedBlockCipher cipher = chaining == KeyChaining.CBC
                ? new BufferedBlockCipher(new CbcBlockCipher(new DesEdeEngine()))   //CBC chaining
                : new BufferedBlockCipher(new DesEdeEngine());                      //ECB chaining

            if (icv != null)
                cipher.Init(doEncrypt, new ParametersWithIV(new KeyParameter(theKey), icv));
            else
                cipher.Init(doEncrypt, new KeyParameter(theKey));

            MemoryStream dst = new MemoryStream();

            byte[] bin = padded(plain, 24);
            byte[] result = new byte[bin.Length];

            int outL = cipher.ProcessBytes(bin, result, 0);
            if (outL > 0)
                dst.Write(result, 0, outL);

            outL = cipher.DoFinal(result, 0);
            if (outL > 0)
                dst.Write(result, 0, outL);

            dst.Position = 0;
            result = dst.ToArray();
            dst.Close();

            if (result.Length > plain.Length)
            {
                byte[] res = new byte[plain.Length];
                System.Array.Copy(result, res, plain.Length);

                return res;

            } //if (result.Length > plain.Length)

            return result;

        } //private byte[] encryptDesEde(byte[] plain)
        private byte[] encryptAES(byte[] plain, KeyChaining chaining = KeyChaining.CBC, bool doEncrypt = true, byte[] icv = null)
        {
            BufferedBlockCipher cipher = chaining == KeyChaining.CBC 
                ? new BufferedBlockCipher(new CbcBlockCipher(new AesEngine()))   //CBC chaining
                : new BufferedBlockCipher(new AesEngine());                      //ECB chaining

            if(icv != null)
                cipher.Init(doEncrypt, new ParametersWithIV(new KeyParameter(theKey), icv));
            else
                cipher.Init(doEncrypt, new KeyParameter(theKey));

            MemoryStream dst = new MemoryStream();

            byte[] bin = padded(plain, 24);
            byte[] result = new byte[bin.Length];

            int outL = cipher.ProcessBytes(bin, result, 0);
            
            if (outL > 0)
                dst.Write(result, 0, outL);

            if (outL < plain.Length)
            {
                outL = cipher.DoFinal(result, 0);
                if (outL > 0)
                    dst.Write(result, 0, outL);

            } //if (outL < plain.Length)

            dst.Position = 0;
            result = dst.ToArray();
            dst.Close();

            if (result.Length > plain.Length)
            {
                byte[] res = new byte[plain.Length];
                System.Array.Copy(result, res, plain.Length);
                return res;

            } //if (result.Length > plain.Length)

            return result;

        } //private byte[] encryptAES(byte[] plain)
        private string getAesKCV()
        {
            return Hex.ToHexString(encryptAES(Hex.Decode("000000"))).ToUpper();

        } //private string getAesKCV(byte[] key)
        private byte[] padded(byte[] input, int length = 16)
        {
            byte[] result = null;

            if (input.Length % length != 0)
            {
                result = new byte[(input.Length / length + 1) * length];
                System.Array.Copy(input, result, input.Length);

            }
            else
                return input;

            return result;

        }

        private byte[] CalculateDesMac(byte[] input, byte[] icv, int macType = 0)
        {
            if (icv == null)
                icv = new byte[8];

            IMac mac = null;
            switch(macType)
            {
                case 0:
                    mac = MacUtilities.GetMac("DESEDEMAC64");
                    //mac = new ISO9797Alg3Mac(new DesEngine());
                    //mac = MacUtilities.GetMac("DESWITHISO9797");
                    break;

                case 1:
                    mac = MacUtilities.GetMac("DESEDEMAC64WITHISO7816-4PADDING");
                    break;

                case 2:
                    //mac = MacUtilities.GetMac("DESMAC");
                    mac = MacUtilities.GetMac("ISO9797ALG3WITHISO7816-4PADDING");
                    break;

                case 3:
                    //mac = MacUtilities.GetMac("DESMAC/CFB8");
                    mac = MacUtilities.GetMac("DESWITHISO9797"); 
                    break;

            } //switch(macType)

            mac.Init(new KeyParameter(theKey));
            
            mac.BlockUpdate(input, 0, input.Length);
            
            byte[] result = new byte[8];
            int outL = mac.DoFinal(result, 0);
            if (outL > 0)
                log.Debug(outL.ToString());


            return result;

        } //private byte[] CalculateDesMac(byte[] input, byte[] icv)
        private byte[] CalculateAESMac(byte[] input, byte[] icv, int macType = 0)
        {
            if (icv == null)
                icv = new byte[16];

            IMac mac = new CbcBlockCipherMac(new AesEngine());
            mac.Init(new ParametersWithIV(new KeyParameter(theKey), icv));

            mac.BlockUpdate(input, 0, input.Length);

            byte[] result = new byte[16];
            int outL = mac.DoFinal(result, 0);
            if (outL > 0)
                log.Debug(outL.ToString());

            return result;

        } //private byte[] CalculateAESMac(byte[] input, byte[] icv)
        public static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;

        }
        public static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

    } //public class IKeyImpl
}
