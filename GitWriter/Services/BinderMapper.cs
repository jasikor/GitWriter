using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using BookModel.Binder;

namespace GitWriter.ViewModels;

public static class BinderMapper
{
    public static ObservableBinderEntry ToObservableEntry(BinderEntry entry)
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

    public static ObservableCollection<ObservableBinderEntry> ToObservableCollection(IList<BinderEntry> binderItems) =>
        new( 
            binderItems.Select(ToObservableEntry)
        );

}