using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GitWriter.ViewModels;

public partial class BinderViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<ObservableBinderEntry> _items;

    public BinderViewModel()
    {
        Items = new() {
            new ObservableDocument() {Title = "Introduction to Microsoft C#"},
            new ObservableFolder() {
                Title = "Chapter 1 - Basics for Beginners",
                Items = new ObservableCollection<ObservableBinderEntry>() {
                    new ObservableDocument() {Title = "C# Keywords and Reserved Words"},
                    new ObservableDocument() {Title = "if Statements and Expressions"},
                    new ObservableFolder() {
                        Title ="Loops",
                        Items = new ObservableCollection<ObservableBinderEntry>() {
                            
                            new ObservableDocument() {Title = "Loops, and When to Avoid Them"},
                            new ObservableDocument(){Title = "Advanced Loops"}
                        }
                    }
                }
            },
            new ObservableFolder() {Title = "Chapter 2"},
            new ObservableFolder() {Title = "Chapter 3"},
            new ObservableFolder() {Title = "Conclusion"},
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

public partial class ObservableDocument : ObservableBinderEntry
{
    
}