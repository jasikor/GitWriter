using System.Collections.Immutable;

namespace BookModel;

public record Document
{
    public ImmutableList<Paragraph> Paragraphs = ImmutableList<Paragraph>.Empty;
}

public static class DocumentPersistance
{
    public static Document Load(this Document @this, Func<IEnumerable<string>> readLines) =>
        @this with {Paragraphs = readLines().ToParagraphs()};

    private static ImmutableList<Paragraph> ToParagraphs(this IEnumerable<string> @this) =>
        ImmutableList<Paragraph>.Empty
            .AddRange(@this.Select(l => new Paragraph {Line = l}));
}