using System;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GitWriter.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] string _title = "Book of C# and Other Languages";

    [ObservableProperty]
    string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Book Of CSharp";

    [RelayCommand]
    void Submit()
    {
        Title = Random.Shared.Next(100).ToString();
    }
}