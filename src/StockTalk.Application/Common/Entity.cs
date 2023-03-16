namespace StockTalk.Application.Common;

public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
        CreateAt = DateTime.Now;
    }
    
    public Guid Id { get; init; }
    public DateTime CreateAt { get; init; }
}