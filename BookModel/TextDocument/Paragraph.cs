namespace BookModel.TextDocument;

public record Paragraph : DocumentItem
{
    public string Line { get; init; } = String.Empty;
}