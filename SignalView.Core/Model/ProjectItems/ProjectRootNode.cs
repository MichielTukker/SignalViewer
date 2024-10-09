using SignalView.Core.Model.MeasurementData;

namespace SignalView.Core.Model.ProjectItems;

public class ProjectRootNode : ProjectNode
{
    public override string NodeText => "Project Root";
    public override Measurement Measurement { get; set; } = new();
}