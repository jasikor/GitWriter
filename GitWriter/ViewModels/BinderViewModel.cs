using System.Collections.ObjectModel;
using BookModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GitWriter.ViewModels;

public partial class BinderViewModel : ObservableObject
{
    private readonly IObservableBinderService _binderService;
    [ObservableProperty] private ObservableCollection<ObservableBinderEntry> _items;

    public BinderViewModel(IObservableBinderService binderService)
    {
        _binderService = binderService;
        Items = _binderService.Get();
    }

    [RelayCommand]
    private void DoNow()
    {
        Items.Add(new ObservableDocument() {Title = "12gfgg"});
    }
}

public partial class ObservableBinderEntry : ObservableObject
{
    [ObservableProperty] private string _title = string.Empty;
}

public partial class ObservableFolder : ObservableBinderEntry
{
    [ObservableProperty]
    private ObservableCollection<ObservableBinderEntry> _items = new ObservableCollection<ObservableBinderEntry>();
}

public partial class ObservableDocument : ObservableBinderEntry { }