using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SignalViewer.Models;

public class Sample
{
    
    public int Index { get; private set; }
    public List<int> Data = new();
    public Sample(int index)
    {
        Index = index;
     }
    public Sample(int index, List<int> data)
    {
        Index = index;
        this.Data = data;
    }
}