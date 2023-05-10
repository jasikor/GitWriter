// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Text;
using BookModel.TextDocument;
using BookModel.TextDocument.Styles;

namespace Sandbox;

public static class Program
{
    public static int Main(string[] args)
    {
        var doc = CreateDocument();
        var defaultStyleBuilder = new StyleBuilder();
        var defaultStyle = defaultStyleBuilder
            .VerticalSpacing(new VerticalSpacingStyle() {Above = 2, Below = 11})
            .LineSpacing(new LineSpacingStyle() {Spacing = 15f})
            .Font(new FontStyle(){Family = "Arial", Size = 12})
            .Build();
        ;

        var res = new StringBuilder();
        res.Append(
            $"<!DOCTYPE html><html> <head><title>Page Title {DateTime.Now}</title></head><body  style=\"font-family:iA Writer Quattro V Regular, Courier New\">\n");

        res.Append(Render(doc, defaultStyle));


        res.Append("</body></html>");
        Console.WriteLine(res);
        File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\gupie.html",
            res.ToString());
        return 0;
    }

    private static StringBuilder Render(Document doc, DocumentStyle defaultStyle)
    {
        StringBuilder res = new StringBuilder();
        res.Append(Inspect(defaultStyle.ToString()));
        foreach (var d in doc.Items) {
            res.Append(RenderDocSection(d, defaultStyle));
            res.Append("\n");
        }

        return res;
    }

    private static string Inspect(string s) =>
        $"<p style=\"font-family:iA Writer Quattro V Regular, Courier New; font-size: 8pt; background-color: #eeeeee; margin: 1px\">{s}</p>";

    private static StringBuilder RenderDocSection(DocumentSection documentSection, DocumentStyle style)
    {
        var s = style.ApplyStyleDefinition(documentSection.Style);
        var res = new StringBuilder();
        res.Append(Inspect($"Above: {s.VerticalSpacing.Above}"));

        res.Append($"<div style=\"margin: {style.VerticalSpacing.Above}px 0px {style.VerticalSpacing.Below}px \">");
        res.Append(documentSection switch {
            ParagraphSection par => RenderParagraph(par, s),
            ListSection list => RenderList(list, s),
            _ => new UnreachableException()
        });

        res.Append("</div>");

        return res;
    }

    private static StringBuilder RenderList(ListSection list, DocumentStyle style)
    {
        var s = style.ApplyStyleDefinition(list.Style);

        var res = new StringBuilder();
        res.Append("<div style=\"overflow:auto;\">");

        res.Append("<div style=\"float: left; width: 5%;\">");
        res.Append(RenderBullet());
        res.Append("</div>");

        res.Append("<div style=\"float: right; width: 95%;\">");
        res.Append(RenderIntendedContent(list, s));
        res.Append("</div>");

        res.Append("</div>");
        return res;
    }

    private static StringBuilder RenderIntendedContent(ListSection list, DocumentStyle style)
    {
        var res = new StringBuilder();
        res.Append(RenderParagraph(list.FirstParagraph, style));


        foreach (var section in list.Sections) {
            res.Append(RenderDocSection(section, style));
        }

        return res;
    }

    private static string RenderBullet()
    {
        return "*";
    }

    private static StringBuilder RenderParagraph(ParagraphSection par, DocumentStyle style)
    {
        var st = style.ApplyStyleDefinition(par.LineSpacing);
        var res = new StringBuilder();
        res.Append("<p style=\"margin: 0px\">");
        res.Append(Inspect($"LineSpacing: {st.LineSpacing.Spacing}"));
        foreach (var s in par.Spans)
            res.Append(RenderSpan(s, st));

        res.Append("</p>");
        res.Append(Inspect($"Below: {st.VerticalSpacing.Below}"));

        return res;
    }

    private static StringBuilder RenderSpan(CharacterSpan characterSpan, DocumentStyle style)
    {
        var s = style.ApplyFontDefinition(characterSpan.Style);
        var res = new StringBuilder();
        res.Append(Inspect($"Font:{s.Font}"));
        res.Append($"<span style=\"font-family:{s.Font.Family}\">{characterSpan.Characters}</span>");
        return res;
    }

    private static Document CreateDocument()
    {
        var res = new Document();
        for (int i = 0; i < 2; i++)
            res.Items.Add(CreateParagraphSection());

        for (int i = 0; i < 2; i++)
            res.Items.Add(CreateListSection());

        for (int i = 0; i < 2; i++)
            res.Items.Add(CreateParagraphSection());

        return res;
    }

    private static Random random = new Random();

    private static ListSection CreateListSection()
    {
        var res = new ListSection();
        res.FirstParagraph.Spans.Add(CreateCharacterSpan("pierwszy paragraf listy"));
        res.FirstParagraph.LineSpacing = new() {Spacing = random.NextSingle() * 15f + 100f};

        for (int i = random.Next(0, 9); i > 0; i--)
            res.Sections.Add(CreateParagraphSection());
        return res;
    }

    private static ParagraphSection CreateParagraphSection()
    {
        var res = new ParagraphSection();
        res.Spans.Add(CreateCharacterSpan("Span pierwszy"));
        res.Spans.Add(CreateCharacterSpan(" i tu drugi"));
        res.LineSpacing = new() {
            Spacing = random.NextSingle() * 15f,
        };
        return res;
    }

    private static CharacterSpan CreateCharacterSpan(string s)
    {
        var res = new CharacterSpan();
        res.Characters = s;
        res.Style = new() {
            Size = random.NextSingle() * 15 + 50,
            Family = random.NextSingle() <0.5f ? "Times New Roman" : "Courier New",
        };
        return res;
    }
}