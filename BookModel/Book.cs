using BookModel.Binder;
using GitWriter.ViewModels;

namespace BookModel;

public class Book
{
    public BookBinder Binder { get; set; }
    public BookMetadata MetaData;
}