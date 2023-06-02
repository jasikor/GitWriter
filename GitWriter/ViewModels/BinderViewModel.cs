using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using BookModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GitWriter.ViewModels;

public partial class BinderViewModel : ObservableObject
{
    private readonly IBinderService _binderService;
    [ObservableProperty] private ObservableCollection<ObservableBinderEntry> _items;

    public BinderViewModel(IBinderService binderService)
    {
        _binderService = binderService;
        Items = GetItems(_binderService);
    }

    private static ObservableCollection<ObservableBinderEntry> GetItems(IBinderService binderService)
    {
        var r = binderService.Get();
        return new() {
            new ObservableFolder() {
                Title = "Draft",
                Items = new() {
                    new ObservableDocument() {
                        Title = "Introduction to Microsoft COBOL"
                    },
                    new ObservableFolder() {
                        Title = "Basics for Beginners",
                        Items = new ObservableCollection<ObservableBinderEntry>() {
                            new ObservableDocument() {Title = "COBOL Keywords and Reserved Words"},
                            new ObservableDocument() {Title = "if Statements and Expressions"},
                            new ObservableFolder() {
                                Title = "Loops",
                                Items = new ObservableCollection<ObservableBinderEntry>() {
                                    new ObservableDocument() {Title = "Loops, and When to Avoid Them"},
                                    new ObservableDocument() {Title = "Advanced Loops"}
                                }
                            }
                        }
                    },
                    new ObservableFolder() {Title = "More than Basics"},
                    new ObservableFolder() {Title = "Advanced"},
                    new ObservableFolder() {Title = "Conclusion"},
                }
            },
            new ObservableFolder() {Title = "Research"},
            new ObservableFolder() {
                Title = "Dictionary",
                Items = new() {
                    new ObservableDocument() {Title = "Keyword"},
                    new ObservableDocument() {Title = "Loop"},
                    new ObservableDocument() {Title = "Private"},
                }
            }
        };
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