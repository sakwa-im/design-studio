namespace sakwa
{
    public interface IAssignMethod
    {
        string Name { get; }
        string GetValue();
        bool NextValue();

    }
}
