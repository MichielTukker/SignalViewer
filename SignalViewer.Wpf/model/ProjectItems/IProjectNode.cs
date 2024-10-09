using System;
using System.Collections.ObjectModel;
using SignalViewer.Models.MeasurementData;

namespace SignalViewer.Models.ProjectItems;

public interface IProjectNode
{
    public ObservableCollection<ProjectNode> ProjectSubItems { get; set; }
    public string NodeText { get; }
    public string NodeFullPath { get; }
    public Measurement Measurement { get; }
}