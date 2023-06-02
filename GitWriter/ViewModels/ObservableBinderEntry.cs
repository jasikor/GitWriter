using CommunityToolkit.Mvvm.ComponentModel;

namespace GitWriter.ViewModels;

public partial class ObservableBinderEntry : ObservableObject
{
    [ObservableProperty] private string _title = string.Empty;
}