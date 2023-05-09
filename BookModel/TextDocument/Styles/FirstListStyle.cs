namespace BookModel.TextDocument.Styles;

public record FirstListStyle
{
    public float SpacingBelow;
}

public static class FirstListStyleExt
{
    public static FirstListStyle ApplyStyleDefinition(this FirstListStyle fs, SpacingBelowStyleDefinition styleDefinition) =>
        fs with {
            SpacingBelow = styleDefinition.Spacing.IfNone(fs.SpacingBelow),
        };
}