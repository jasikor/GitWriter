using System.Collections.Generic;
using System.Linq;
using BookModel;
using BookModel.Binder;

namespace GitWriter.Services;

public class DummyBinderService : IBinderService
{
    private BookBinder _binder = new BookBinder() {
        Items = new List<BinderEntry>() {
            new Folder("Draft") {
                Items = new List<BinderEntry>() {
                    new DocEntry("Introduction"),
                    new Folder("Chapter 1 - Basics") {
                        Items = new List<BinderEntry>() {
                            new DocEntry("COBOL Keywords"),
                            new DocEntry("if Statement"),
                            new DocEntry("Loops"),
                        }
                    },
                    new Folder("Chapter 2 - Intermediate") {
                        Items = new List<BinderEntry>() {
                            new DocEntry("Classes vs Records"),
                            new DocEntry("The Best Loops and When To Use Them"),
                            new DocEntry("So What is Assembly, Anyway"),
                            new DocEntry("Public, Private and Assemblies"),
                        }
                    },
                    new Folder("Chapter 2 - Advanced") {
                        Items = new List<BinderEntry>() {
                            new DocEntry("Multitasking, Multithreding, Asynchronous and Other Complicated Concepts"),
                            new DocEntry("WPF vs Maui"),
                            new DocEntry("Generics"),
                            new DocEntry("LanguageExt and Functional Programming"),
                        }
                    },
                    new DocEntry("Conclusion"),
                }
            },
            new Folder("Research") {
                Items = new List<BinderEntry>() {
                    new DocEntry("Books to be read"),
                    new Folder("Read Books Notes") {
                        Items = new List<BinderEntry>() {
                            new DocEntry("The COBOL Bible"),
                            new DocEntry("COBOL For Geniuses"),
                            new DocEntry("Functional COBOL"),
                        }
                    }
                }
            },
            new Folder("Dictionary") {
                Items = new List<BinderEntry>() {
                    new DocEntry("abstract"),
                    new DocEntry("DI"),
                    new DocEntry("Maui"),
                    new DocEntry("LINQ"),
                    new DocEntry("WPF"),
                }
            },
            new Folder("Trash") {
                Items = new List<BinderEntry>() {
                    new DocEntry("abstractions"),
                    new DocEntry("DInjection"),
                    new DocEntry("Manual resource"),
                    new DocEntry("LINQ"),
                    new Folder("Chapter 22 - The Future") {
                        Items = new List<BinderEntry>() {
                            new DocEntry("No Fututre"),
                            new DocEntry("No Past"),
                        }
                    },
                    new DocEntry("WPF"),
                }
            }
        }
    };

    public BookBinder Get() => _binder;


}