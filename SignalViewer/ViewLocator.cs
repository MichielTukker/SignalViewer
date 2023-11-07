using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using SignalViewer.ViewModels;

namespace SignalViewer;

public class ViewLocator : IDataTemplate
{
    public Control Build(object? data)
    {
        var name = data?.GetType().FullName!.Replace("ViewModel", "View");
        Type? type = null;
        if (name != null)
        {
            type = Type.GetType(name);
        }
        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }
        return new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}