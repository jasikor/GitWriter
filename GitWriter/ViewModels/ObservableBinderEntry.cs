using System;
using System.ComponentModel;
using Accessibility;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GitWriter.ViewModels;

public partial class ObservableBinderEntry : ObservableObject
{
    [ObservableProperty] private string _title = string.Empty;

    [ObservableProperty] private string _id = Guid.NewGuid().ToString();
}