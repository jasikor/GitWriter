using System.ComponentModel;
using LanguageExt.Pretty;

namespace BookModel.TextDocument.Styles;

public record DocumentStyle
{
    public VerticalSpacingStyle VerticalSpacing;
    public LineSpacingStyle LineSpacing;
}

public static class DocumentStyleExt
{
    public static DocumentStyle ApplyStyleDefinition(this DocumentStyle ds,
        VerticalSpacingStyleDefinition definition) =>
        ds with {
            VerticalSpacing = ds.ApplyVerticalSpacing(definition)
        };
public static DocumentStyle ApplyStyleDefinition(this DocumentStyle ds,
        LineSpacingStyleDefinition definition) =>
        ds with {
            LineSpacing = ds.LineSpacing with {
                Spacing = definition.Spacing.IfNone(ds.LineSpacing.Spacing),
            }
        };

    private static VerticalSpacingStyle ApplyVerticalSpacing(this DocumentStyle ds,
        VerticalSpacingStyleDefinition definition) =>
        ds.VerticalSpacing with {
            Above = definition.Above.IfNone(ds.VerticalSpacing.Above),
            Below = definition.Below.IfNone(ds.VerticalSpacing.Below),
        };

}