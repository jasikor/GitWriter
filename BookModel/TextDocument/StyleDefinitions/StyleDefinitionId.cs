namespace BookModel.TextDocument.StyleDefinitions;

public abstract class StyleDefinitionId
{
    public string Id;
}

public class ParagraphStyleDefinitionID : StyleDefinitionId { }

public class ListStyleDefinitionID : StyleDefinitionId { }

public class CharacterStyleDefinitionID : StyleDefinition { }