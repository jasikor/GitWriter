using BookModel.Binder;

namespace BookModel;

public interface IBinderService
{
    BookBinder Get();
}