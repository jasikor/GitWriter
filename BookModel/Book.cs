using BookModel.Binder;

namespace BookModel;

public class Book
{
    public BookBinder Binder { get; set; }
    public string Title;
}