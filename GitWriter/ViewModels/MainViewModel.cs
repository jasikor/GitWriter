using System;
using System.Diagnostics;
using BookModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GitWriter.Services;

namespace GitWriter.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IBookMetadataService _metadataService;
    private readonly BookMetadata _metadata;

    [ObservableProperty]
    string _title;

    [ObservableProperty]
    string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Book Of CSharp";

    public MainViewModel(IBookMetadataService metadataService)
    {
        _metadataService = metadataService;
        _metadata = metadataService.Get();
        _title = _metadata.Title;
    }

    [RelayCommand]
    void Submit()
    {
        Title = Random.Shared.Next(100).ToString();
    }
}