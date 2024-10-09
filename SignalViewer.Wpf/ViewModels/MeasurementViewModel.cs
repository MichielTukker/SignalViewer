using ReactiveUI;
using SignalViewer.Models;
using SignalViewer.Models.MeasurementData;

namespace SignalViewer.ViewModels;

public class MeasurementViewModel: ReactiveObject
{
    public Measurement CurrentMeasurement => _parent.CurrentMeasurement;
    public DataChartViewModel ChartViewModel { get; set; }
    public DataTableViewModel TableViewModel { get; set; }
    private MainViewModel _parent;
    public MeasurementViewModel(MainViewModel parent)
    {
        _parent = parent;

        ChartViewModel = new DataChartViewModel(_parent);
        TableViewModel = new DataTableViewModel(_parent);
    }
    
}