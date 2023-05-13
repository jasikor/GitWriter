using System.Text;
using BookModel.TextDocument;
using BookModel.TextDocument.StyleDefinitions;
using BookModel.TextDocument.Styles;
using LanguageExt;

namespace Sandbox;

public static class RendererStringBuilderExt
{
    public static StringBuilder Inspect(this StringBuilder sb, string s) =>
        sb.Append(
            $"<p style=\"font-family:iA Writer Quattro V Regular, Courier New; font-size: 8pt; " +
            $"background-color: #f8f8f8; margin: 1px\">{s}</p>\n");
}

public static class Program
{
    public static void Main(string[] args)
    {
        var doc = CreateDocument("The First Intergalactic Document");
        var defaultStyle = GetDefaultStyle();
        var styleManager = new StylesManager();
        var htmlRenderer = new HTMLRenderer(styleManager);
        
        var html = htmlRenderer.Render(doc, defaultStyle);

        SaveRendered(html);
    }

    private static DocumentStyle GetDefaultStyle() =>
        new StyleBuilder()
            .AboveSpacing(2)
            .BelowSpacing(13)
            .LineSpacing(15f)
            .CharacterStyle(new CharacterStyle() {FontFamily = "Arial", FontSize = 12})
            .ListStyle(new ListStyle() {Indentation = 20f})
            .Build();

    private static void SaveRendered(StringBuilder res)
    {
        Console.WriteLine(res);
        File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\gupie.html",
            res.ToString());
    }


    /*******************************************************************************************/
    private static Document CreateDocument(string title)
    {
        var res = new Document(title);
        for (int i = 0; i < 2; i++)
            res.Items.Add(CreateParagraphSection());

        for (int i = 0; i < 2; i++)
            res.Items.Add(CreateListSection());

        for (int i = 0; i < 2; i++)
            res.Items.Add(CreateParagraphSection());

        return res;
    }

    private static readonly Random random = new Random();

    private static ListSection CreateListSection() =>
        new ListSectionBuilder()
            .ParagraphStyleId(ParagraphStyleDefinitionID.Heading1)
            .ListStyleId(ListStyleDefinitionID.Default)
            .FirstParagraph(new ParagraphSectionBuilder()
                .ParagraphStyle(new() {
                    LineSpacing = random.NextSingle() * 15f + 100f,
                    SpacingAbove = 3.3f,
                    SpacingBelow = 4.4f,
                })
                .AddCharacterSpan(CreateCharacterSpan("pierwszy paragraf listy "))
                .AddCharacterSpan(CreateCharacterSpan("drugi span pierwszego paragrafu listy "))
                .Build())
            .ListStyle(random.NextSingle() < 0.5
                ? new() {Indentation = random.NextSingle() * 30f}
                : new())
            .AddSection(random.NextSingle() < 0.8
                ? CreateParagraphSection()
                : CreateListSection())
            .Build();

    private static ParagraphSection CreateParagraphSection() =>
        new ParagraphSectionBuilder()
            .AddCharacterSpan(CreateCharacterSpan("Span pierwszy"))
            .AddCharacterSpan(CreateCharacterSpan(" i tu drugi"))
            .ParagraphStyle(
                random.NextSingle() < 0.5
                    ? new() {
                        LineSpacing = random.NextSingle() * 15f,
                        SpacingBelow = Option<float>.None, //random.NextSingle() * 10f,
                        SpacingAbove = 3.3f,
                    }
                    : new())
            .ParagraphStyleId(ParagraphStyleDefinitionID.Default)
            .Build();

    private static CharacterSpan CreateCharacterSpan(string s) =>
        new CharacterSpan {
            Characters = s,
            CharacterStyle = random.NextSingle() < 0.5
                ?  new CharacterStyleDefinition() {
                    FontSize = random.NextSingle() * 15 + 50,
                    FontFamily = random.NextSingle() < 0.5f ? "Times New Roman" : "Courier New",
                }
                : new(),
            CharacterStyleDefinitionId = CharacterStyleDefinitionID.Default
        };
}