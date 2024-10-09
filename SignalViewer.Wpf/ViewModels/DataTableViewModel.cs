using ReactiveUI;

namespace SignalViewer.ViewModels;

public class DataTableViewModel: ReactiveObject
{
    private MainViewModel _parent;
    public DataTableViewModel(MainViewModel parent)
    {
        _parent = parent;
        // Source = new FlatTreeDataGridSource<Sample>(_parent.CurrentMeasurement.Samples);
        // Source.Columns.Add(new TextColumn<Sample, int>("Index", x => x.Index));
        // foreach (var column in _measurement.Columns)
        // {
        //     // Source.Columns.Add(new DataGridTextColumn{Header = column.ColumnName, Binding = new Binding($"Row.ItemArray[{column.Ordinal}]")});
        //     var t = column.GetType();
        //     Source.Columns.Add(new TextColumn<Sample, double>($"{column.Name}", x => x.Data[column.Index]));
        // }
    }

    // public FlatTreeDataGridSource<Sample> Source { get; set; }
}