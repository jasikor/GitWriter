using System.Collections.Generic;
using BookModel;
using BookModel.Binder;

namespace GitWriter.Services;

public class DummyBinderService : IBinderService
{
    public BookBinder Get()
    {
        return new BookBinder() {
            Items = new List<BinderEntry>() {
                new Folder("Draft") {
                    Items = new List<BinderEntry>() {
                        new DocEntry("Introduction"),
                        new Folder("Chapter 1 - Basics") {
                            Items = new List<BinderEntry>() {
                                new DocEntry("C# Keywords"),
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
                                new DocEntry("Multitasking, Multithreding and Asynchronous"),
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
                                new DocEntry("The C# Bible"),
                                new DocEntry("C# For Geniuses"),
                                new DocEntry("Functional c#"),
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
                }
            }
        };
    }

}