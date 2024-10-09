using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reactive;
using System.Windows.Input;
using ReactiveUI;
using SignalViewer.Models.ProjectItems;

namespace SignalViewer.ViewModels;

public class ProjectViewModel : ReactiveObject
{
    public Interaction<string, List<string>> OpenFileInteraction { get; }
    public readonly ReactiveCommand<Unit, List<string>> _openFileCommand;
    public ICommand OpenFileCommand => _openFileCommand;

    // public ReactiveCommand<Unit,Unit> OpenFolderCommand { get; }

    public ProjectViewModel()
    {
        Items = new ObservableCollection<IProjectNode>();
        ProjectRootNode projectNode = new ProjectRootNode();
        projectNode.ProjectSubItems.Add(new ProjectNode());
        Items.Add(projectNode
        );

        OpenFileInteraction = new Interaction<string, List<string>>();
        _openFileCommand = ReactiveCommand.CreateFromObservable(
            () => OpenFileInteraction.Handle("Open measurement files") );
        _openFileCommand.Subscribe(list => OpenFiles(list));
        
        _openFileCommand.ThrownExceptions.Subscribe(exception => 
        {
            Debug.WriteLine(exception);
        });
        // OpenFolderCommand = openDirectoryCommand;
    }

    private void OpenFiles(List<string> files)
    {
        if (files.Count == 0)
        {
            return;
        }
        foreach (var file in files)
        {
            Debug.WriteLine($"Opening file: {file}"); 
        }
    }

    public ObservableCollection<IProjectNode> Items { get; }
    public ObservableCollection<IProjectNode> SelectedItems { get; } = new ObservableCollection<IProjectNode>();
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
}