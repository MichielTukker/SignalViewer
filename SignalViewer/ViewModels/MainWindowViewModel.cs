using System.Collections.ObjectModel;
using System.Data;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SignalViewer.Models;

namespace SignalViewer.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    
    public ProjectViewModel ProjectView { get; set; }
    public MeasurementViewModel MeasurementDisplay { get; private set; }
    private readonly Measurement _measurement;
    public FlatTreeDataGridSource<Sample> Source { get; }

    public MainWindowViewModel()
    {
        ProjectView = new ProjectViewModel();
        MeasurementDisplay = new MeasurementViewModel();
        
        _measurement = new Measurement(8,10);

        Source = new FlatTreeDataGridSource<Sample>(_measurement.Samples);
        Source.Columns.Add(new TextColumn<Sample, int>("Index", x => x.Index));
        foreach (var column in _measurement.Columns)
        {
            // Source.Columns.Add(new DataGridTextColumn{Header = column.ColumnName, Binding = new Binding($"Row.ItemArray[{column.Ordinal}]")});
            var t = column.GetType();
            Source.Columns.Add(new TextColumn<Sample, double>($"{column.Name}", x => x.Data[column.Index]));
        }

        var m = new MeasurementSettings(@"C:\2_src\data\shockslugs\ShockSlugs004.set");
    }
    public string Greeting => "Welcome to Avalonia!";
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