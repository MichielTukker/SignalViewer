using System;
using System.Collections.ObjectModel;
using SignalView.Core.Model.MeasurementData;

namespace SignalView.Core.Model.ProjectItems;

public interface IProjectNode
{
    public ObservableCollection<ProjectNode> ProjectSubItems { get; set; }
    public string NodeText { get; }
    public string NodeFullPath { get; }
    public Measurement Measurement { get; }
}