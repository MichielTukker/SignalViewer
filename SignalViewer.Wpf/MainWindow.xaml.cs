using ReactiveUI;
using SignalViewer.ViewModels;
using Splat;

namespace SignalViewer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : IActivatableView
{
    public MainWindow()
    {
        InitializeComponent();
        AppBootstrapper = new AppBootstrapper();
        DataContext = AppBootstrapper;
    }

    public AppBootstrapper AppBootstrapper { get; private set; }
}