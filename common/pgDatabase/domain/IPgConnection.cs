namespace pgDatabase
{
    public interface IPgConnection
    {
        string Name { get; set; }
        string Server { get; set; }
        string Port { get; set; }
        string User { get; set; }
        string Password { get; set; }
        string Database { get; set; }

        bool Persist { get; set; }

        IPgConnection Clone();

        bool Equals(IPgConnection input);

        string ConnectionString { get; }

        string ToString();
    }

}
