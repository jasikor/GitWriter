using System;
using System.Diagnostics;
using BookModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GitWriter.Services;

namespace GitWriter.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly Book _book;

    [ObservableProperty]
    string _title;

    [ObservableProperty]
    string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Book Of CSharp";

    public MainViewModel(IBookService bookService)
    {
        _book = bookService.GetBook();
        _title = _book.Title;
    }

    public MainViewModel()
    {
        _book = new Book()
        {
            Title = "Design Time Book Title",
            Binder = new BookModel.Binder.BookBinder()
        };
        _title = _book.Title;
    }
    [RelayCommand]
    void Submit()
    {
        Title = Random.Shared.Next(100).ToString();
    }
}