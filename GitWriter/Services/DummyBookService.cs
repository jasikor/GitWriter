using System.IO;
using System.Windows.Documents;
using BookModel;
using GitWriter.ViewModels;

namespace GitWriter.Services;

public class DummyBookService : IBookService
{
    private readonly IBinderService _binderService;
    private readonly IBookMetadataService _metadataService;

    public DummyBookService(IBinderService binderService, IBookMetadataService metadataService)
    {
        _binderService = binderService;
        _metadataService = metadataService;
    }
    public Book GetBook() =>
        new() {
            MetaData = _metadataService.Get() ,
            Binder = _binderService.Get(),
        };
}