using System.Collections.Immutable;

namespace BookModel.Document;

public record Document
{
    public ImmutableList<DocumentItem> Items = ImmutableList<DocumentItem>.Empty;
}

public static class DocumentPersistence
{
    public static Document Load(this Document @this, Func<IEnumerable<string>> readLines) =>
        @this with {Items = readLines().ToDocumentItem()};

    private static ImmutableList<DocumentItem> ToDocumentItem(this IEnumerable<string> @this) =>
        ImmutableList<DocumentItem>.Empty
            .AddRange(@this.Select(l => new Paragraph {Line = l}));
}