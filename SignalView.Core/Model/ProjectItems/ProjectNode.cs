using System;
using System.Collections.ObjectModel;
using SignalView.Core.Model.MeasurementData;

namespace SignalView.Core.Model.ProjectItems;

public class ProjectNode: IProjectNode
{
    public ObservableCollection<ProjectNode> ProjectSubItems { get; set; }=new ObservableCollection<ProjectNode>();

    public virtual string NodeText => Measurement.Name;
    public string NodeFullPath { get; } = String.Empty;
    
    public virtual Measurement Measurement { get; set; } = new();

    public ProjectNode()
    {
    }
    
    public ProjectNode(string nodeFullPath)
    {
        NodeFullPath = nodeFullPath;
        Measurement = new Measurement(nodeFullPath);
    }
}