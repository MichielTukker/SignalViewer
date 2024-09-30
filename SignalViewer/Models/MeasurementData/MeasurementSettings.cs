using System.Collections.Generic;
using System.Linq;
using static SignalViewer.Models.MeasurementData.MeasurementChannel;

namespace SignalViewer.Models.MeasurementData;
public class MeasurementSettings
{
    public readonly List<Channel> Channels = new List<Channel>();
    public string Version { get; private set; } = string.Empty;
    public int NumberOfChannels { get; private set; } = 0;
    public double SampleFrequency { get; private set; } = 0;
    public int NumberOfRows { get; private set; } = 0;
    public string TimeIndexName { get; private set; } = "Time (s)";

    private IniFile _iniFile = new IniFile();

    public MeasurementSettings()
    {
    }

    public MeasurementSettings(string filename)
    {
        _iniFile = new IniFile(filename);
        Dictionary<string, IniValue> GenSettings;
        if (_iniFile.GetSectionTitles().Contains("Gereral Settings"))
            GenSettings = _iniFile["Gereral Settings"]; // account for typo
        else
            GenSettings = _iniFile["General Settings"];

        if (GenSettings.Keys.Contains("Version"))
            Version = GenSettings["Version"].Value;
        else
            Version = GenSettings["Collect Set version"].Value;
        
        NumberOfChannels = GenSettings["Number of channels"].GetValueAsInt();
        SampleFrequency =  GenSettings["Sample Freq/Time"].GetValueAsDouble();
        NumberOfRows = (int) (GenSettings["Datfile Duration (s)"].GetValueAsDouble() * SampleFrequency); //TODO  fix int overflow...?
        ParseChannels();
    }

    private void ParseChannels()
    {
        foreach (var sectionTitle in _iniFile.GetSectionTitles())
        {
            if (sectionTitle.StartsWith("Channel"))
            {
                var iniSection = _iniFile[sectionTitle];
                var name = iniSection["Name"].GetValueAsString();
                Channels.Add(new Channel(){
                    Name = iniSection["Name"].GetValueAsString(),
                    Dimension = iniSection["Dimension"].GetValueAsString(),
                    Gain = iniSection["Gain"].GetValueAsDouble(),
                    C2 = iniSection["C2"].GetValueAsDouble(),
                    C1 = iniSection["C1"].GetValueAsDouble(),
                    C0 = iniSection["C0"].GetValueAsDouble(),
                    X = iniSection["X (m)"].GetValueAsDouble(),
                    Y = iniSection["Y (m)"].GetValueAsDouble(),
                    Z = iniSection["Z (m)"].GetValueAsDouble(),
                    Zero = iniSection["Zero"].GetValueAsDouble()
                    });
            }
        }
    }
}