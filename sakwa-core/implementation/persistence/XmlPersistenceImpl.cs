using System;
using System.IO;
using System.Xml;

namespace sakwa
{
    public class XmlPersistenceImpl : IPersistence
    {
        public XmlPersistenceImpl() { }
        public XmlPersistenceImpl(string fullFilePath, bool keepClosed = false)
        {
            if (keepClosed)
                FileName = fullFilePath;
            else
                (this as IPersistence).Open(fullFilePath);
        }
        public XmlPersistenceImpl(XmlDocument xml)
        {
            doc = xml;
        }
        string IPersistence.Name { get { return this.FileName; } }

        bool IPersistence.AddRecord()
        {
            if(docOpen)
            {
                record = NewNode();
                doc.DocumentElement.AppendChild(record);
            }

            return record != null;

        }
        bool IPersistence.SelectRecord(string name, string criteria)
        {
            foreach(XmlNode node in doc.DocumentElement)
                if(node[name] != null && node[name].InnerText == criteria)
                {
                    record = node;
                    return true;

                }

            return false;

        }
        bool IPersistence.NextRecord()
        {
            if (record != null)
                record = record.NextSibling;
            else
                record = docOpen ? doc.DocumentElement.FirstChild : null;

            return record != null;

        }
        bool IPersistence.UpsertField(string name, string value)
        {
            if (record[name] != null)
                record[name].InnerText = value;
            else
            {
                XmlNode field = doc.CreateNode(XmlNodeType.Element, name, "");
                field.InnerText = value;
                record.AppendChild(field);

            }

            return true;

        }
        bool IPersistence.UpsertFieldArray(string name, string[] values)
        {
            XmlNode field = null;
            if (record[name] == null)
            {
                field = doc.CreateNode(XmlNodeType.Element, name, "");
                record.AppendChild(field);

            }
            field = record[name];

            field.RemoveAll();
            foreach (string value in values)
            {
                XmlNode subfield = doc.CreateNode(XmlNodeType.Element, "element", "");
                subfield.InnerText = value;
                field.AppendChild(subfield);

            }

            return true;

        }
        string[] IPersistence.GetFieldValues(string name, string defaultValue)
        {
            string[] result = new string[] { defaultValue };

            if(record[name] != null)
            {
                XmlNode field = record[name];
                result = new string[field.ChildNodes.Count];

                int i = 0;
                foreach (XmlNode subfield in field.ChildNodes)
                    result[i++] = subfield.InnerText;

            }
            return result;

        }

        bool IPersistence.HasField(string name)
        {
            return record != null && record[name] != null;

        }
        string IPersistence.GetFieldValue(string name, string defaultValue)
        {
            return record != null && record[name] != null
                ? record[name].InnerText
                : defaultValue;
        }
        int IPersistence.GetFieldValue(string name, int defaultValue)
        {
            return record != null && record[name] != null
                ? int.Parse(record[name].InnerText)
                : defaultValue;
        }
        decimal IPersistence.GetFieldValue(string name, decimal defaultValue)
        {
            return record != null && record[name] != null
                ? decimal.Parse(record[name].InnerText)
                : defaultValue;

        }
        bool IPersistence.GetFieldValue(string name, bool defaultValue)
        {
            return record != null && record[name] != null
                ? bool.Parse(record[name].InnerText)
                : defaultValue;
        }

        bool IPersistence.Open(string fullFilePath)
        {
            if(File.Exists(fullFilePath))
            {
                FileName = fullFilePath;
                doc = new XmlDocument();
                doc.Load(FileName);

                if (doc.DocumentElement.Attributes["version"] != null)
                    FileVersion = doc.DocumentElement.Attributes["version"].InnerText;

                return true;

            }

            return false;

        }

        bool IPersistence.Save()
        {
            return SaveFile(FileName);
        }

        bool IPersistence.SaveAs(string fullFilePath)
        {
            return SaveFile(fullFilePath);
        }

        string IPersistence.RawContent
        {
            get
            {
                return doc != null ? doc.InnerXml : "";
            }
        }

        IPersistence IPersistence.Clone(string fullFilePath)
        {
            if (fullFilePath == "")
            {
                return new XmlPersistenceImpl(FileName);
            }
            else
            {
                return File.Exists(fullFilePath)
                    ? new XmlPersistenceImpl(fullFilePath)
                    : null;
            }
        }

        string IPersistence.GetRelativePath(string fullPath, string basePath)
        {
            if (fullPath == "")
                return "";

            if (basePath == "")
                basePath = Path.GetDirectoryName(FileName) + Path.DirectorySeparatorChar;

            // Require trailing backslash for path
            if (!basePath.EndsWith("\\"))
                basePath += "\\";

            Uri baseUri = new Uri(basePath);
            Uri fullUri = new Uri(fullPath);

            Uri relativeUri = baseUri.MakeRelativeUri(fullUri);

            // Uri's use forward slashes so convert back to backward slashes
            string result = relativeUri.ToString().Replace("/", "\\");
            result = result.Replace("%20", " ");
            return result;
        }
        string IPersistence.GetFullPath(string relativePath)
        {
            if (relativePath == "" || Path.GetPathRoot(relativePath) != "")
                return relativePath;

            return Path.GetDirectoryName(FileName) + Path.DirectorySeparatorChar + relativePath;

        }
        string IPersistence.FileVersion { get { return FileVersion; } set { FileVersion = value; } }


        protected bool SaveFile(string fullFilePath)
        {
            if (doc != null)
            {
                doc.Save(fullFilePath);
                return true;

            }

            return false;

        }

        protected void CreateBasicContent()
        {
            if (doc == null)
                doc = new XmlDocument();

            doc.InnerXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><records></records>";

            XmlAttribute attr = doc.CreateAttribute("version");
            attr.InnerText = FileVersion;
            doc.DocumentElement.Attributes.Append(attr);

        }
        protected string GetAttributeValue(XmlNode node, string name, string defaultValue = "")
        {
            return node.Attributes[name] != null ? node.Attributes[name].InnerText : defaultValue;
        }
        protected bool docOpen
        {
            get
            {
                if (doc == null)
                    CreateBasicContent();

                return doc != null;
            }
        }
        protected XmlNode NewNode()
        {
            return doc.CreateNode(XmlNodeType.Element, "record", "");
        }

        protected string FileName = "";
        protected XmlDocument doc = null;
        protected XmlNode record = null;
        protected string FileVersion = "0.0";
    }
}
