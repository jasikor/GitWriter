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

        var res = new StringBuilder();
        res.Append("<!DOCTYPE html><html> <head><title>Page Title</title></head><body>\n");
        res.Append(Render(doc));
        res.Append("</body></html>");
        Console.WriteLine(res);
        File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\gupie.html", res.ToString());
        return 0;
    }

    private static StringBuilder Render(Document doc)
    {
        StringBuilder res = new StringBuilder();
        foreach (var d in doc.Items) {
            DocumentStyle style = new DocumentStyle().ApplyStyleDefinition(d.Style);
            res.Append(RenderDocSection(d, style));
            res.Append("\n");
        }

        return res;
    }

    private static StringBuilder RenderDocSection(DocumentSection documentSection, DocumentStyle style)
    {
        var res = new StringBuilder();
        res.Append($"<div style=\"margin: {style.SpacingAbove}px 0px {style.SpacingBelow}px \">");
        res.Append(documentSection switch {
            ParagraphSection par => RenderParagraph(par),
            ListSection list => RenderList(list),
            _ => new UnreachableException()
        });
        res.Append("</div>");

        return res;
    }

    private static StringBuilder RenderList(ListSection list)
    {
        var res = new StringBuilder();
        res.Append("<div style=\"overflow:auto;\">");
        
        res.Append("<div style=\"float: left; width: 5%;\">");
        res.Append(RenderBullet());
        res.Append("</div>");
        
        res.Append("<div style=\"float: right; width: 95%;\">");
        res.Append(RenderIntendedContent(list));
        res.Append("</div>");

        res.Append("</div>");
        return res;

    }

    private static StringBuilder RenderIntendedContent(ListSection list)
    {
        var res = new StringBuilder();
        

        res.Append(RenderFirstList(list.FirstParagraph));

        if (list.Sections.Any()) {
            FirstListStyle style = new FirstListStyle().ApplyStyleDefinition(list.FirstParagraph.Style);
            res.Append($"<span>extra space {style.SpacingBelow}</span>");
        }
        foreach (var section in list.Sections) {
            DocumentStyle style = new DocumentStyle().ApplyStyleDefinition(section.Style);
            res.Append(RenderDocSection(section, style));
        }

        return res;
    }

    private static string RenderBullet()
    {
        return "*";
    }

    private static StringBuilder RenderParagraph(ParagraphSection par)
    {
        var res = new StringBuilder();
        res.Append("<p style=\"margin: 0px\"> Paragraf: ");
        foreach (var s in par.Spans)
            res.Append(RenderSpan(s));

        res.Append("</p>");
        return res;
    }
    
    private static StringBuilder RenderFirstList(FirstListElement par)
    {
        var res = new StringBuilder();
        res.Append("<p style=\"margin: 0px\"> FirstLine: ");
        foreach (var s in par.Spans)
            res.Append(RenderSpan(s));

        res.Append("</p>");
        return res;
    }

    private static StringBuilder RenderSpan(CharacterSpan characterSpan)
    {
        var res = new StringBuilder();
        res.Append($"<span>{characterSpan.Characters}</span>");
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

        for (int i = random.Next(0, 9); i > 0; i-- )
           res.Sections.Add( CreateParagraphSection());
        return res;
    }

    private static ParagraphSection CreateParagraphSection()
    {
        var res = new ParagraphSection();
        res.Spans.Add(CreateCharacterSpan("Span pierwszy"));
        res.Spans.Add(CreateCharacterSpan(" i tu drugi"));
        return res;
    }

    private static CharacterSpan CreateCharacterSpan(string s)
    {
        var res = new CharacterSpan();
        res.Characters = s;
        return res;
    }
}