namespace BookModel.TextDocument.Styles;

public record FirstListStyle
{
    public float SpacingBelow;
}

public static class FirstListStyleExt
{
    public static FirstListStyle ApplyStyleDefinition(this FirstListStyle fs, FirstListStyleDefinition definition) =>
        fs with {
            SpacingBelow = definition.SpacingBelow.IfNone(fs.SpacingBelow),
        };
}