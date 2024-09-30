using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;
using SignalViewer.Models;
using SignalViewer.Models.MeasurementData;

namespace SignalViewer.ViewModels;

public class MainViewModel : ViewModelBase
{
    
    public ProjectViewModel ProjectView { get; set; }
    public MeasurementViewModel MeasurementDisplay { get; private set; }

    public Measurement CurrentMeasurement => ProjectView.SelectedItem.Measurement;

    // public FlatTreeDataGridSource<Sample> Source { get; }

    public MainViewModel()
    {
        ProjectView = new ProjectViewModel();
        
        MeasurementDisplay = new MeasurementViewModel(this);

       
    }
}
