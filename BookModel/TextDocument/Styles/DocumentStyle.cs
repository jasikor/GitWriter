using System.ComponentModel;
using BookModel.TextDocument.StyleDefinitions;
using LanguageExt.Pretty;

namespace BookModel.TextDocument.Styles;

public record DocumentStyle
{
    public float AboveSpacing;
    public float BelowSpacing;
    public ParagraphStyle Paragraph;
    public FontStyle Font;
    public ListStyle ListStyle;
}

public static class DocumentStyleExt
{
    public static DocumentStyle ApplyStyleDefinition(this DocumentStyle ds,
        VerticalSpacingStyleDefinition definition) =>
        ds with {
            AboveSpacing = definition.Above.IfNone(ds.AboveSpacing)
        };

    public static DocumentStyle ApplyStyleDefinition(this DocumentStyle ds,
        ParagraphStyleDefinition definition) =>
        ds with {
            Paragraph = ds.Paragraph with {
                LineSpacing = definition.LineSpacing.IfNone(ds.Paragraph.LineSpacing),
            },
            BelowSpacing = definition.Below.IfNone(ds.BelowSpacing),

        };

    public static DocumentStyle ApplyStyleDefinition(this DocumentStyle ds,
        FontStyleDefinition definition) =>
        ds with {
            Font = ds.Font with {
                Family = definition.Family.IfNone(ds.Font.Family),
                Size = definition.Size.IfNone(ds.Font.Size),
            }
        };
    
    public static DocumentStyle ApplyStyleDefinition(this DocumentStyle ds,
        ListStyleDefinition definition) =>
        ds with {
            ListStyle = ds.ListStyle with {
                Indentation= definition.Indentation.IfNone(ds.ListStyle.Indentation),
            },
            BelowSpacing =  definition.SpacingBelowFirstElement.IfNone(ds.BelowSpacing)
        };

}