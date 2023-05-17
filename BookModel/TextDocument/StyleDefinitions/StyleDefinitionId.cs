namespace BookModel.TextDocument.StyleDefinitions;

public abstract class StyleDefinitionId
{
    public string Id = string.Empty;
}

public class ParagraphStyleDefinitionID : StyleDefinitionId
{
    public static readonly ParagraphStyleDefinitionID Default = new() {Id = "Default Paragraph Style"};
    public static readonly ParagraphStyleDefinitionID Heading1 = new() {Id = "Heading 1"};
    public static readonly ParagraphStyleDefinitionID Heading2 = new() {Id = "Heading 2"};
    public static readonly ParagraphStyleDefinitionID Heading3 = new() {Id = "Heading 3"};
    public static readonly ParagraphStyleDefinitionID Title = new() {Id = "Title"};
}

public class ListStyleDefinitionID : StyleDefinitionId
{
    public static readonly ListStyleDefinitionID Default = new() {Id = "Default List Style"};
}

public class CharacterStyleDefinitionID : StyleDefinitionId
{
    public static readonly CharacterStyleDefinitionID Default = new() {Id = "Default Character Style"};
}