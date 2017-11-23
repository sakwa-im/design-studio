using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace configuration
{
    public class ConfigurationItemObject<T> : IConfigurationItemImpl, IConfigurationItemObject<T>
        {
            public ConfigurationItemObject(){}
            public ConfigurationItemObject(string name)
                : base(name)
            {
                _Type = typeof(T).Name;
            }

            public ConfigurationItemObject(string name, T newValue, eConfigurationSource target)
                : base(name, StreamPersist(newValue), target)
            {
                _Type = typeof(T).Name;
            }
            public ConfigurationItemObject(string name, T newValue, eConfigurationSource target, 
                eConfigurationSource sourceAllowed = eConfigurationSource.AllAllowed)
                : base(name, StreamPersist(newValue), target, sourceAllowed)
            {
                _Type = typeof(T).Name;
            }

            public static T StreamRetrieve(string input)
            {
                if (typeof(T) == typeof(Color))
                {
                    return (T)Convert.ChangeType(ColorTranslator.FromHtml(input), typeof(T));
                }

                //var knowTypes = new Type[] { typeof(T), typeof(T).BaseType };
                //var serializer = new XmlSerializer(typeof(T), knowTypes);
                var serializer = new XmlSerializer(typeof(T));
                using (StringReader sr = new StringReader(input))
                    return (T) serializer.Deserialize(sr);

            }
            public static string StreamPersist(T input)
            {
                if (input is Color)
                {
                    Color c = ((Color)(Convert.ChangeType(input, typeof(Color))));
                    return ColorTranslator.ToHtml(c);

                }

                var serializer = new XmlSerializer(typeof(T));
                MemoryStream s = new MemoryStream();
                serializer.Serialize(s, input);
                s.Position = 0;
                string result = Encoding.ASCII.GetString(s.ToArray());
                return result;

            }

            public T GetValue(T defaultValue)
            {
                return _Value != "" ? StreamRetrieve(_Value) : defaultValue;
            }
            void SetValue(T newValue)
            {
                _Value = StreamPersist(newValue);
            }

            T IConfigurationItemObject<T>.GetValue(T defaultValue)
            {
                return _Value != "" ? StreamRetrieve(_Value) : defaultValue;
            }
            void IConfigurationItemObject<T>.SetValue(T newValue)
            {
                _Value = StreamPersist(newValue);
            }
            void IConfigurationItemObject<T>.Attach(IConfigurationItem item)
            {
                string storageKey = _StorageKey;
                _StorageKey = "";

                _Value = item.Value;
                _Source = item.Source;
                _Target = item.Target;
                _StorageKey = storageKey;

                item.StorageKey = storageKey;

            }


        }
}
