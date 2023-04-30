﻿using System.Collections.Immutable;

namespace BookModel.Binder;

public record Folder(string Title = "(folder)") : BinderEntry(Title)
{
    public ImmutableList<BinderEntry> Items { get; init; } = ImmutableList<BinderEntry>.Empty;
}