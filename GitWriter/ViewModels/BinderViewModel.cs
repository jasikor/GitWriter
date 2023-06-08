using System;
using System.Collections.ObjectModel;
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
        Items = BinderMapper.ToObservableCollection( _binderService.Get().Items);
    }

}                