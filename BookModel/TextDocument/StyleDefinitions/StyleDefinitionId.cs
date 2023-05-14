namespace BookModel.TextDocument.StyleDefinitions;

public abstract class StyleDefinitionId
{
    public string Id = string.Empty;
}

public class ParagraphStyleDefinitionID : StyleDefinitionId
{
    public static readonly ParagraphStyleDefinitionID Default = new() {Id = "default-paragraph-style"};
    public static readonly ParagraphStyleDefinitionID Heading1 = new() {Id = "heading1"};
    public static readonly ParagraphStyleDefinitionID Heading2 = new() {Id = "heading2"};
    public static readonly ParagraphStyleDefinitionID Heading3 = new() {Id = "heading3"};
    public static readonly ParagraphStyleDefinitionID Title = new() {Id = "title"};
}

public class ListStyleDefinitionID : StyleDefinitionId
{
    public static readonly ListStyleDefinitionID Default = new() {Id = "Default List Style"};
}

public class CharacterStyleDefinitionID : StyleDefinitionId
{
    public static readonly CharacterStyleDefinitionID Default = new() {Id = "Default Character Style"};
}