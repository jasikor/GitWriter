namespace BookModel.Binder;

public class BinderEntry
{
    public Guid Id { get; } = Guid.NewGuid();

    public BinderEntry(string title) => Title = title;
    public string Title { get; set; } 
    public IList<BinderEntry> Items { get; init; } = new List<BinderEntry>();
    
    protected bool Equals(BinderEntry other) => Title == other.Title && Items.SequenceEqual(other.Items);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((BinderEntry) obj);
    }

    public override int GetHashCode() => HashCode.Combine(Title, Items);
}