using System.Globalization;

namespace SignalViewer.Models.MeasurementData;

public class IniValue
{
    public string Value { get; private set; }
    public string Key { get; private set; }
    
    private CultureInfo _culture = CultureInfo.InvariantCulture;
    public IniValue(string key, string value)
    {
        Key = key;
        Value = value;
    }

    public double GetValueAsDouble()
    {
        return double.Parse(Value, NumberStyles.Number, _culture);
    }

    public int GetValueAsInt()
    {
        return int.Parse(Value, NumberStyles.Number, _culture);
    }

    public string GetValueAsString()
    {
        return Value;
    }

    public override string ToString()
    {
        return $"{Key} = {Value}";
    }
}