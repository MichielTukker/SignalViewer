using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using SignalViewer.Models.ProjectItems;

namespace SignalViewer.ViewModels;

public class ProjectViewModel : ViewModelBase
{
    public ObservableCollection<IProjectNode> Items { get; }
    public ObservableCollection<IProjectNode> SelectedItems { get; } = new ObservableCollection<IProjectNode>();
    public ReactiveCommand<Unit,Unit> OpenFileCommand { get; }
    public ReactiveCommand<Unit,Unit> OpenFolderCommand { get; }

    private IProjectNode _selectedItem = new ProjectNode();
    public IProjectNode SelectedItem
    {
        get
        {
            return _selectedItem;
        }
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItem, value);
        }
    }

    public ProjectViewModel()
    {
        Items = new ObservableCollection<IProjectNode>();
        
        // CurrentMeasurement = new Measurement(@"c:\2_src\data\shockslugs\ShockSlugs004.set");

        ProjectRootNode projectNode = new ProjectRootNode();
        projectNode.ProjectSubItems.Add(new ProjectNode());
        Items.Add(projectNode
        
        );
        OpenFileCommand = ReactiveCommand.Create(() => { });
        OpenFolderCommand = ReactiveCommand.Create(() => { });
    }
}