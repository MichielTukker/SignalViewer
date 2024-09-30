using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ScottPlot.Avalonia;
using SignalViewer.ViewModels;

namespace SignalViewer.Views;

public partial class DataChartView : ReactiveUserControl<DataChartViewModel>
{
    private AvaPlot? avaPlot1 = null;
    private DataChartViewModel? vm;

    public DataChartView()
    {
        InitializeComponent();
        this.Loaded += OnLoaded;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        // vm = (DataChartViewModel) DataContext;
        vm = DataContext as DataChartViewModel;
        Debug.Assert(vm!=null);
        vm.ChartDataChanged += OnDataChanged;
        update_chart();
    }

    private void OnDataChanged(object? sender, EventArgs e)
    {
        update_chart();
    }

    private void update_chart()
    {
        if(vm==null)
            return;
        if (avaPlot1 == null)
        {
            avaPlot1= this.Find<AvaPlot>("AvaPlot1") ?? throw new InvalidOperationException();    
        }
        avaPlot1.Plot.Clear();
        avaPlot1.Plot.XLabel("Time (s)");
        avaPlot1.Plot.YLabel("Output (units)");
        avaPlot1.Plot.Title(vm.CurrentMeasurement.Name);

        if (!vm.CurrentMeasurement.IsEmpty)
        {
            var datax = vm.CurrentMeasurement.GetTimeSeries();
            var datay = vm.CurrentMeasurement.GetSeries("WHM2");
            avaPlot1.Plot.Add.Scatter(datax,datay);
        }
        avaPlot1.Refresh();
    }
}