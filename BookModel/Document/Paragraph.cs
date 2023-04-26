namespace BookModel.Document;

public record Paragraph : DocumentItem
{
    public string Line { get; init; } = String.Empty;
}