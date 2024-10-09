using SignalViewer.Models.MeasurementData;

namespace SignalViewer.Models.ProjectItems;

public class ProjectRootNode : ProjectNode
{
    public override string NodeText => "Project Root";
    public override Measurement Measurement { get; set; } = new();
}