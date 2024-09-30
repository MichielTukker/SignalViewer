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
    private Measurement _measurement = null!;

    public Measurement CurrentMeasurement
    {
        get
        {
            return _measurement;
        } 
        private set
        {
            _measurement = value;
            this.RaiseAndSetIfChanged(ref _measurement, value);
        }
    }
    // public FlatTreeDataGridSource<Sample> Source { get; }

    public MainViewModel()
    {
        ProjectView = new ProjectViewModel();
        CurrentMeasurement = new Measurement(@"c:\2_src\data\shockslugs\ShockSlugs004.set");
        
        Debug.Assert(_measurement!=null);
        MeasurementDisplay = new MeasurementViewModel(this);

       
    }
}

public class Node
{
    public ObservableCollection<Node> Subfolders { get; set; }

    public string strNodeText { get; }
    public string strFullPath { get; }

    public Node(string _strFullPath)
    {
        strFullPath = _strFullPath;
        strNodeText = _strFullPath;
    }
}