using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using BookModel;
using BookModel.Binder;

namespace GitWriter.ViewModels;

public class ObservableBinderService : IObservableBinderService
{
    private readonly IBinderService _binderService;

    public ObservableCollection<ObservableBinderEntry> Get() => ToObservableBinder(_binderService.Get());

    private ObservableCollection<ObservableBinderEntry> ToObservableBinder(BookBinder binder) => ToObservableCollection(binder.Items);

    private ObservableCollection<ObservableBinderEntry> ToObservableCollection(IList<BinderEntry> binderItems) =>
        new( 
            binderItems.Select(ToObservableEntry)
        );

    private ObservableBinderEntry ToObservableEntry(BinderEntry entry)
    {
        ObservableBinderEntry res = entry switch {
            Folder f => new ObservableFolder() {
                Items = ToObservableCollection(f.Items),
            },
            DocEntry => new ObservableDocument(),
            _ => throw new UnreachableException(),
        };
        res.Title = entry.Title;
        return res;
    }

    public ObservableBinderService(IBinderService binderService) => _binderService = binderService;
}