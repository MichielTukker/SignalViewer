using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using SignalViewer.ViewModels;

namespace SignalViewer.Views;

public partial class DataChartView 
{
    // private DataChartViewModel? vm;

    public DataChartView()
    {
        InitializeComponent();

        this.Loaded += OnLoaded;
    }
    
    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        // vm = (DataChartViewModel) DataContext;
        // vm = DataContext as DataChartViewModel;
        // Debug.Assert(vm!=null);
        // vm.ChartDataChanged += OnDataChanged;
        // update_chart();
        double[] dataX = { 1, 2, 3, 4, 5 };
        double[] dataY = { 1, 4, 9, 16, 25 };
        WpfPlot1.Plot.Add.Scatter(dataX, dataY);
        WpfPlot1.Plot.XLabel("Time (s)");
        WpfPlot1.Plot.YLabel("Output (units)");
        WpfPlot1.Plot.Axes.AutoScale();
        WpfPlot1.Refresh();
    }
    
    
    // private void OnDataChanged(object? sender, EventArgs e)
    // {
    //     update_chart();
    // }
    //
    // private void update_chart()
    // {
    //     WpfPlot1.Plot.Clear();
    //     WpfPlot1.Plot.XLabel("Time (s)");
    //     WpfPlot1.Plot.YLabel("Output (units)");
    //     WpfPlot1.Plot.Title(vm.CurrentMeasurement.Name);
    //
    //     if (!vm.CurrentMeasurement.IsEmpty)
    //     {
    //         var datax = vm.CurrentMeasurement.GetTimeSeries();
    //         var datay = vm.CurrentMeasurement.GetSeries("WHM2");
    //         WpfPlot1.Plot.Add.Scatter(datax,datay);
    //     }
    //     WpfPlot1.Refresh();
    // }
}