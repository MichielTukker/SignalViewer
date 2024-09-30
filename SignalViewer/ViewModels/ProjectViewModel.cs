using System.Collections.ObjectModel;

namespace SignalViewer.ViewModels;

public class ProjectViewModel : ViewModelBase
{
    public ObservableCollection<Node> Items { get; }
    public ObservableCollection<Node> SelectedItems { get; }

    public ProjectViewModel()
    {
        Items = new ObservableCollection<Node>();

        Node rootNode = new Node("Project");
        rootNode.Subfolders = new ObservableCollection<Node>(){new Node("Empty")};
        Items.Add(rootNode);
        SelectedItems = new ObservableCollection<Node>();
   
    }
}