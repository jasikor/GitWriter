using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GitWriter.ViewModels;

public partial class BinderViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<ObservableBinderEntry> _items;

    public BinderViewModel()
    {
        Items = new() {
            new ObservableBinderEntry()  {Title = "Introduction to Microsoft C#"},
            new ObservableBinderEntry() {
                Title = "Chapter 1 - Basics for Beginners",
                Items = new ObservableCollection<ObservableBinderEntry>() {
                    new ObservableBinderEntry(){ Title = "C# Keywords and Reserved Words"},
                    new ObservableBinderEntry(){ Title = "if Statements and Expressions"},
                    new ObservableBinderEntry(){ Title = "Loops, and When to Avoid Them"},
                }
            },
            new ObservableBinderEntry()  {Title = "Chapter 2"},
            new ObservableBinderEntry()  {Title = "Chapter 3"},
            new ObservableBinderEntry()  {Title = "Conclusion"},
            
            
        };
    }

    [RelayCommand]
    private void DoNow()
    {
        Items.Add(new ObservableBinderEntry(){Title = "12gfgg"});
    }

}

public partial class ObservableBinderEntry : ObservableObject
{
    [ObservableProperty] private string _title = string.Empty;

    [ObservableProperty] private ObservableCollection<ObservableBinderEntry> _items = new ObservableCollection<ObservableBinderEntry>();
}