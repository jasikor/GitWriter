using CommunityToolkit.Mvvm.ComponentModel;

namespace GitWriter.ViewModels;

public partial class BinderViewModel : ObservableObject
{
    [ObservableProperty] private string _binder = "This is Binder";
}