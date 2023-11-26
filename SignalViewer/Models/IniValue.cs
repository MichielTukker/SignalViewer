namespace SignalViewer.Services.Models;

public class IniValue
{
    public string Value { get; private set; }
    public string Key { get; private set; }
    
    public IniValue(string key, string value)
    {
        Key = key;
        Value = value;
    }

    public double GetValueAsDouble()
    {
        return double.Parse(Value);
    }

    public int GetValueAsInt()
    {
        return int.Parse(Value);
    }

    public override string ToString()
    {
        return $"{Key} = {Value}";
    }
}