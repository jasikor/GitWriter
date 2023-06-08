using GitWriter.ViewModels;

namespace GitWriter;

public class DummyMetadataService:IBookMetadataService
{
    public BookMetadata Get() => new() {Title = "This is The COBOL book"};
}