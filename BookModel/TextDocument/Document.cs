using System.Collections.Immutable;

namespace BookModel.TextDocument;

public record Document
{
    public ImmutableList<DocumentItem> Items = ImmutableList<DocumentItem>.Empty;
}

public static class DocumentPersistence
{
    public static Document Load(this Document @this, Func<IEnumerable<string>> readLines) =>
        @this with {Items = readLines().ToDocumentItem()};

    private static ImmutableList<DocumentItem> ToDocumentItem(this IEnumerable<string> lines) =>
        ImmutableList<DocumentItem>.Empty
            .AddRange(lines.Select(l => new Paragraph {Line = l}));
}