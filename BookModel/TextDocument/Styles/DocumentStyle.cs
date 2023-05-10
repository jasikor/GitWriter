using System.ComponentModel;
using BookModel.TextDocument.StyleDefinitions;
using LanguageExt.Pretty;

namespace BookModel.TextDocument.Styles;

public record DocumentStyle
{
    public VerticalSpacingStyle VerticalSpacing;
    public LineSpacingStyle LineSpacing;
    public FontStyle Font;
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

    public static DocumentStyle ApplyFontDefinition(this DocumentStyle ds,
        FontStyleDefinition definition) =>
        ds with {
            Font = ds.Font with {
                Family = definition.Family.IfNone(ds.Font.Family),
                Size = definition.Size.IfNone(ds.Font.Size),
            }
        };

    private static VerticalSpacingStyle ApplyVerticalSpacing(this DocumentStyle ds,
        VerticalSpacingStyleDefinition definition) =>
        ds.VerticalSpacing with {
            Above = definition.Above.IfNone(ds.VerticalSpacing.Above),
            Below = definition.Below.IfNone(ds.VerticalSpacing.Below),
        };
}