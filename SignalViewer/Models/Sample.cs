using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SignalViewer.Models;

public class Sample
{
    public int Index { get; private set; }
    public List<double> Data { get; set; }
    
    public Sample(int index)
    {
        Index = index;
        Data = new();
    }
    public Sample(int index, List<double> data)
    {
        Index = index;
        this.Data = data;
    }
}