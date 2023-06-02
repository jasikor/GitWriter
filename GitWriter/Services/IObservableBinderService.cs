using System.Collections.ObjectModel;

namespace GitWriter.ViewModels;

public interface IObservableBinderService
{
    ObservableCollection<ObservableBinderEntry> Get();
}