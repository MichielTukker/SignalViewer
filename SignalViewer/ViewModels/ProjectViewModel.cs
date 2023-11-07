using System.Collections.ObjectModel;

namespace SignalViewer.ViewModels;

public class ProjectViewModel : ViewModelBase
{
    public ObservableCollection<Node> Items { get; }
    public ObservableCollection<Node> SelectedItems { get; }

    public ProjectViewModel()
    {
        Items = new ObservableCollection<Node>();

        Node rootNode = new Node("Blabla");
        rootNode.Subfolders = new ObservableCollection<Node>(){new Node("item1"), new Node("Item2"), new Node("Item3"), new Node("Item4"), new Node("Item5"), new Node("Item6")};
        Items.Add(rootNode);
        SelectedItems = new ObservableCollection<Node>();
   
    }
}