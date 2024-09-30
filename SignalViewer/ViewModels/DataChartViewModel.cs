using System;
using SignalViewer.Models;
using SignalViewer.Models.MeasurementData;

namespace SignalViewer.ViewModels;

public class DataChartViewModel: ViewModelBase
{
    public Measurement CurrentMeasurement => _parent.CurrentMeasurement;
    public event EventHandler? ChartDataChanged;

    private readonly MainViewModel _parent;

    public DataChartViewModel(MainViewModel parent)
    {
        _parent = parent;
        
        parent.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(MainViewModel.CurrentMeasurement))
            {
                OnChartDataChanged();
            }
        };
        OnChartDataChanged();
    }

    protected virtual void OnChartDataChanged()
    {
        ChartDataChanged?.Invoke(this, EventArgs.Empty);
    }
}