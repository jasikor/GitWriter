using System;

namespace GitWriter;

public class MainWindowViewModel
{
    public string BookTitle { get; set; } = "Book of C# and Other Languages";
    public string Path { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Book Of CSharp";
}