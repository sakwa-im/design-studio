using System;

namespace configuration
{
    public class ICommandlineItemImpl : ICommandlineItem
    {
        public ICommandlineItemImpl(string name, string cmdSwitch, Constraint<string> constraint = null)
        {
            _Name = name;
            _CmdSwitch = cmdSwitch.ToLower();
            _Constraint = constraint;

        } //public ICommandlineItemImpl(

        string ICommandlineItem.Name { get { return _Name; } }
        string ICommandlineItem.CmdSwitch { get { return _CmdSwitch; } }

        bool ICommandlineItem.HasSwitch(string arg)
        {
            return HasSwitch(arg);

        } //bool ICommandlineItem.HasSwitch(string arg)
        bool ICommandlineItem.Found { get { return _Found; } }
        string ICommandlineItem.Value { get { return _Value; } }

        #region DeveloperPage support
        public string Name { get { return _Name; } }
        public string Switch { get { return _CmdSwitch; } }
        public string Value { get { return _Value; } }
        public bool Found { get { return _Found; } }
        #endregion

        protected virtual bool HasSwitch(string arg)
        {
            if (arg.ToLower().StartsWith(_CmdSwitch))
            {
                _Found = true;

                _Value = "";
                string val = arg.Substring(_CmdSwitch.Length);

                if (_Constraint == null || _Constraint.IsValid(val))
                {
                    _Found = true;
                    _Value = val;

                }

                return _Value != "";

            } //if (arg.ToLower().StartsWith(_CmdSwitch))

            return false;

        } //protected virtual bool HasSwitch(string arg)

        protected string _Name = "";
        protected string _CmdSwitch = "";
        protected Constraint<string> _Constraint = null;

        protected string _Value = "";
        protected bool _Found = false;

    } //public class ICommandlineItemImpl
}
