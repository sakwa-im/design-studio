using System;

namespace configuration
{
    public interface ICommandlineItem
    {
        string Name { get; }
        string CmdSwitch { get; }

        bool HasSwitch(string arg);
        bool Found { get; }
        string Value { get; }

    } //public interface ICommandlineItem
}
