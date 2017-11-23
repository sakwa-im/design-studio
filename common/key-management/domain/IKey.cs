using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace kms
{
    public enum keyType { DESede, AES, PIN }
    public enum KeyChaining { NONE, ECB, CBC }
    public interface IKey
    {
        string Id { get; set; }
        string Purpose { get; set; }
        string Subject { get; set; }
        string Scope { get; set; }

        string Name { get; set; }
        keyType keyType { get; set; }
        KeyChaining KeyChaining { get; set; }
        int Length { get; set; }

        string keyValue { get; set; }
        byte[] keyBytes { get; set; }
        string kcv { get; set; }

        bool OddParity { get; set; }

        bool IsSpecified { get; }
        bool IsDefined { get; }

        IKey DeriveKey(byte[] derivationData, KeyChaining chaining = KeyChaining.CBC);
        bool KeyValueFromGuid(string guid);

        byte[] Encrypt(byte[] plain);
        byte[] Encrypt(byte[] plain, KeyChaining chaining = KeyChaining.CBC);
        byte[] Encrypt(byte[] plain, byte[] icv, KeyChaining chaining = KeyChaining.CBC);
        string Encrypt(string plain);
        byte[] Decrypt(byte[] crypto);
        byte[] Decrypt(byte[] crypto, KeyChaining chaining = KeyChaining.CBC);
        byte[] Decrypt(byte[] crypto, byte[] icv, KeyChaining chaining = KeyChaining.CBC);
        string Decrypt(string crypto);

        byte[] CalculateMac(byte[] input);
        byte[] CalculateMac(byte[] input, byte[] icv);
        byte[] CalculateMac(byte[] input, byte[] icv, int macType = 0);

        void AddToXmlNode(XmlNode node);

        string test_Icv { get; set; }
        string test_Cipher { get; set; }
        string test_plain { get; set; }

        string Notes { get; set; }

    } //public interface IKey
}
