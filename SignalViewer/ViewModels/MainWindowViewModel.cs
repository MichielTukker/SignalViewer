using System.Collections.ObjectModel;
using System.Data;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Data;
using SignalViewer.Models;

namespace SignalViewer.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    
    public ProjectViewModel ProjectView { get; set; }
    private readonly Measurement _measurement;
    public FlatTreeDataGridSource<Sample> Source { get; }

    public MainWindowViewModel()
    {
        ProjectView = new ProjectViewModel();
        _measurement = new Measurement(8,10);

        Source = new FlatTreeDataGridSource<Sample>(_measurement.Samples);
        Source.Columns.Add(new TextColumn<Sample, int>("Index", x => x.Index));
        foreach (DataColumn column in _measurement.DataSet.Columns)
        {
            // Source.Columns.Add(new DataGridTextColumn{Header = column.ColumnName, Binding = new Binding($"Row.ItemArray[{column.Ordinal}]")});
            Source.Columns.Add(new TextColumn<Sample, int>($"{column.ColumnName}", x => x.Data[column.Ordinal]));
        }

        
        // Source = new FlatTreeDataGridSource<Sample>(_measurement.Samples)
        // {
        //     Columns =
        //     {
        //         new TextColumn<Sample, int>($"Column 0", x => x.Data[0]),
        //         new TextColumn<Sample, int>($"Column 1", x => x.Data[1]),
        //         new TextColumn<Sample, int>($"Column 2", x => x.Data[2]),
        //         new TextColumn<Sample, int>($"Column 3", x => x.Data[3]),
        //         new TextColumn<Sample, int>($"Column 4", x => x.Data[4]),
        //         new TextColumn<Sample, int>($"Column 5", x => x.Data[5]),
        //     }
        // };
        // for (int i = 0; i < _measurement.Columns; i++)
        // {
        //     Source.Columns.Add(new TextColumn<Sample, int>($"Column {i}", x => x.Data[i]));
        // }
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