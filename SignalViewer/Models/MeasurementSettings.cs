using System.Collections.Generic;

namespace SignalViewer.Models;

public class Channel
{
    public string Name { get; set; }
    public string Dimension { get; set; }
    public double Gain { get; set; }
    public double C2 { get; set; }
    public double C1 { get; set; }
    public double C0 { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double Zero { get; set; }

    public Channel(string name)
    {
        Name = name;
        Dimension = "";
    }

    public Channel(string name, string dimension, double gain, double c2, double c1, double c0, double x, double y,
        double z, double zero)
    {
        Name = name;
        Dimension = dimension;
        Gain = gain;
        C2 = c2;
        C1 = c1;
        C0 = c0;
        X = x;
        Y = y;
        Z = z;
        Zero = zero;
    }
    
}
public class MeasurementSettings
{
    private List<Channel> _channels = new List<Channel>();
    private int _numChannels;
    
    public MeasurementSettings(string filename)
    {
        var parser = IniFile.Parse(filename);
        
        
    }
}