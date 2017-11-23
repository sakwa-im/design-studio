namespace sakwa
{
    public interface IPersistence
    {
        bool Open(string fullFilePath);
        bool Save();
        bool SaveAs(string fullFilePath);
        string Name { get; }
        bool AddRecord();
        bool SelectRecord(string name, string criteria);
        bool NextRecord();
        bool UpsertField(string name, string value);
        bool UpsertFieldArray(string name, string[] values);
        string[] GetFieldValues(string name, string defaultValue = "");
        bool HasField(string name);
        string GetFieldValue(string name, string defaultValue = "");
        int GetFieldValue(string name, int defaultValue = 0);
        decimal GetFieldValue(string name, decimal defaultValue = 0m);
        bool GetFieldValue(string name, bool defaultValue = false);

        string RawContent { get; }

        IPersistence Clone(string fullFilePath = "");
        string GetRelativePath(string fullPath, string basePath = "");
        string GetFullPath(string relativePath);

        string FileVersion { get; set; }

    }
}
