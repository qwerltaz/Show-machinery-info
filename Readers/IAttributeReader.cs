namespace DisplayMachineryAttributes.Readers
{
    public interface IAttributeReader
    {
        string GetDisplayText();
        bool IsValid();
    }
}
