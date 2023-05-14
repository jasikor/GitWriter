using System.Runtime.CompilerServices;
using BookModel.Renderers;
using BookModel.TextDocument;
using BookModel.TextDocument.StyleDefinitions;
using FluentAssertions;
using LanguageExt;
using LanguageExt.UnitTesting;

namespace Tests.Renderers;

public class CharacterSpanRendererTests
{
    [Theory]
    [InlineData("")]
    [InlineData("DefaultStyle")]
    public static void RenderSpan_Renders_Class_WhenStyleID_IsPresent(string style)
    {
        var characterSpan = new CharacterSpan() {
            CharacterStyleDefinitionId = new CharacterStyleDefinitionID() {Id = style}
        };
        var actual = DocRenderer
            .RenderSpan(characterSpan)
            .Class as string;
        actual.Should().Be(style);
    }

    [Fact]
    public static void RenderSpan_Renders_Class_WhenStyleID_IsAbsent()
    {
        // Arrange
        var characterSpan = new CharacterSpan() { };
        // Act
        var actual = DocRenderer
            .RenderSpan(characterSpan)
            .Class as string;
        // Assert        
        actual.Should().Be(string.Empty);
    }

    [Theory]
    [InlineData("")]
    [InlineData("asdf")]
    public static void RenderSpan_Renders_Characters_Correctly(string expected)
    {
        var characterSpan = new CharacterSpan() {
            Characters = expected,
        };
        var actual = DocRenderer
            .RenderSpan(characterSpan)
            .Characters as string;
        actual.Should().Be(expected);
    }

    [Fact]
    public static void RenderSpan_Renders_EmptyStyle_AsEmpty()
    {
        var characterSpan = new CharacterSpan() { };
        var actual = DocRenderer
            .RenderSpan(characterSpan)
            .Style as string;
        actual.Should().Be(string.Empty);
    }

    [Fact]
    public static void RenderSpan_Renders_Style_With_NotEmpty_Fields()
    {
        var expectedFontFamily = "FontFamilyName";
        var expectedFontSize = 12;
        var characterSpan = new CharacterSpan() {
            CharacterStyle = new CharacterStyleDefinition() {
                FontFamily = expectedFontFamily,
                FontSize = expectedFontSize,
            }
        };
        var actual = DocRenderer
            .RenderSpan(characterSpan)
            .Style;
        (actual.FontFamily as string).Should().Be(expectedFontFamily);
        (actual.FontSize as string).Should().Be(expectedFontSize.ToString());
    }
    [Fact]
    public static void RenderSpan_Renders_Style_With_Empty_FontSize()
    {
        var expectedFontFamily = "FontFamilyName";
        var characterSpan = new CharacterSpan() {
            CharacterStyle = new CharacterStyleDefinition() {
                FontFamily = expectedFontFamily,
            }
        };
        var actual = DocRenderer
            .RenderSpan(characterSpan)
            .Style;
        (actual.FontFamily as string).Should().Be(expectedFontFamily);
        (actual.FontSize as string).Should().Be(string.Empty);
    } 
    [Fact]
    public static void RenderSpan_Renders_Style_With_Empty_FontFamily()
    {
        var expectedFontSize = 12;
        var characterSpan = new CharacterSpan() {
            CharacterStyle = new CharacterStyleDefinition() {
                FontSize = expectedFontSize,
            }
        };
        var actual = DocRenderer
            .RenderSpan(characterSpan)
            .Style;
        (actual.FontFamily as string).Should().Be(string.Empty);
        (actual.FontSize as string).Should().Be(expectedFontSize.ToString());
    }
}