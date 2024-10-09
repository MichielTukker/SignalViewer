using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using ReactiveUI;
using SignalView.Core.Model.MeasurementData;

namespace SignalViewer.ViewModels;

public class MainViewModel : ReactiveObject, IMainViewModel
{
    
    public ProjectViewModel ProjectView { get; set; }
    public MeasurementViewModel MeasurementDisplay { get; private set; }

    public Measurement CurrentMeasurement => ProjectView.SelectedItem.Measurement;

    // public FlatTreeDataGridSource<Sample> Source { get; }

    public MainViewModel(IScreen? screen)
    {
        HostScreen = screen ?? throw new ArgumentNullException(nameof(screen));
        // ProjectView = projectViewModel;
        // MeasurementDisplay = measurementViewModel;
        ProjectView = new ProjectViewModel();
        MeasurementDisplay = new MeasurementViewModel(this);
       
    }

    public string? UrlPathSegment => String.Empty;
    public IScreen HostScreen { get; }
}
