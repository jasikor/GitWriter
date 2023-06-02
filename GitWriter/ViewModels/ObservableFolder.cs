using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GitWriter.ViewModels;

public partial class ObservableFolder : ObservableBinderEntry
{
    [ObservableProperty]
    private ObservableCollection<ObservableBinderEntry> _items = new ObservableCollection<ObservableBinderEntry>();
}