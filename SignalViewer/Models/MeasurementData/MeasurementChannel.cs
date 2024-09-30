using System;
using System.Collections.Generic;

namespace SignalViewer.Models.MeasurementData;

public class MeasurementChannel
{
    public class Channel
    {
        public string? Name { get; set; } = String.Empty;
        public string? Dimension { get; set; } = String.Empty;
        public double Gain { get; set; }
        public double C2 { get; set; }
        public double C1 { get; set; }
        public double C0 { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Zero { get; set; }
    }
}